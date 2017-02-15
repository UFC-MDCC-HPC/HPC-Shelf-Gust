using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.hpcshelf.gust.example.sssp.DataSSSP;
using br.ufc.mdcc.hpcshelf.gust.example.sssp.SSSP;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using br.ufc.mdcc.hpcshelf.gust.graph.DirectedGraph;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerKV;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeWeighted;

namespace br.ufc.mdcc.hpcshelf.gust.example.sssp.SSSPImpl {
	public class ISSSPImpl : BaseISSSPImpl, ISSSP {

		private IDirectedGraphInstance<IVertex, IEdgeWeighted<IVertex>, int, IEdgeInstance<IVertex, int>> g = null;
		private int[] partition = null;
		private bool[]  partition_own = null;
		private int partition_size = 0;
		private bool done = true;
		private bool[] activates;
		private IDictionary<int, float>[] messages = null;

		public override void main() {} 
		public override void after_initialize() { }
		public bool isGhost(int v){ return !partition_own[this.partition [v - 1]]; }

		public void graph_creator(){ // START: Bloco de alimentacao do componente DirectedGraph com peso nas arestas
			IKVPairInstance<IInteger,IIterator<IInputFormat>> input_gifs_instance = (IKVPairInstance<IInteger,IIterator<IInputFormat>>)Graph_values.Instance;
			IIteratorInstance<IInputFormat> vgifs = (IIteratorInstance<IInputFormat>)input_gifs_instance.Value;

			object o;
			if (partition_own==null){
				if (vgifs.fetch_next (out o)) {
					IInputFormatInstance gif = (IInputFormatInstance)o;
					partition = gif.PartitionTABLE; 
					int partid = gif.PARTID;
					partition_size = gif.PARTITION_SIZE; 
					g = Graph.newInstance (gif.VSIZE);
					g.DataContainer.AllowingLoops = false;
					g.DataContainer.AllowingMultipleEdges = false;
					graph_creator_aux (gif);
					partition_own = new bool[partition_size];
					partition_own [partid] = true;
				}
			}
			while (vgifs.fetch_next (out o)) {
				graph_creator_aux ((IInputFormatInstance)o);
				partition_own [((IInputFormatInstance)o).PARTID] = true;
			}
		}
		private void graph_creator_aux(IInputFormatInstance gif){
			for (int i = 0; i < gif.ESIZE;) {
				if (gif.Target [i] != 0) {
					int s = gif.Source [i]; 
					int t = gif.Target [i];
					float f = gif.Weight [i];
					g.addVertex (s);
					g.addVertex (t);
					g.noSafeAdd (s, t, f);
					i++;
				}
			}
			IIteratorInstance<IKVPair<IInteger,IInputFormat>> output_gifs_instance = (IIteratorInstance<IKVPair<IInteger,IInputFormat>>)Output_gif.Instance;
			IKVPairInstance<IInteger,IInputFormat> item = (IKVPairInstance<IInteger,IInputFormat>)Output_gif.createItem ();
			((IIntegerInstance)item.Key).Value = gif.PARTID;
			item.Value = gif;
			output_gifs_instance.put (item);
		} // END: Bloco de alimentacao do componente DirectedGraph com peso nas arestas

		public void startup_push() { // Inicio do Algoritmo 
			messages = new Dictionary<int, float>[partition_size]; //Preparar buffer de mensagens
			ICollection<int> V = g.vertexSet ();
			for (int i = 0; i < partition_size; i++)
				messages[i] = new Dictionary<int, float> ();

			bool is_source = g.containsVertex (1);
			activates = new bool[partition_size];
			if (is_source) { // Busca em profundidade
				int v = 1; float tmp;
				messages[partition [v - 1]][v]=0f;
				activates [partition [v - 1]] = true;
				Queue<int> queue = new Queue<int> (); queue.Enqueue (v);
				while (queue.Count > 0) {
					v = queue.Dequeue ();
					float vw = messages[partition [v - 1]][v];
					IEnumerator<KeyValuePair<int, float>> vneighbors = g.iteratorOutgoingVertexWeightOf (v);
					while (vneighbors.MoveNext ()) {
						int n = vneighbors.Current.Key;
						float nw = vneighbors.Current.Value+vw; 
						if (!messages [partition [n - 1]].TryGetValue(n,out tmp) || tmp > nw) {
							messages [partition [n - 1]] [n] = nw;
							queue.Enqueue (n);
							activates [partition [n - 1]] = true;
						}
					}
				}
			}
			gust0 ();
		}
		public void pull() { //pull de mensagens das particoes distribuidas
			IKVPairInstance<IInteger,IIterator<IDataSSSP>> input_values_instance = (IKVPairInstance<IInteger,IIterator<IDataSSSP>>)Input_values.Instance;
			IIntegerInstance ikey = (IIntegerInstance)input_values_instance.Key;
			IIteratorInstance<IDataSSSP> ivalues = (IIteratorInstance<IDataSSSP>)input_values_instance.Value;

			object o; float tmp;
			while (ivalues.fetch_next (out o)) {
				IDataSSSPInstance VALUE = (IDataSSSPInstance)o;
				if (!VALUE.Activated) {
					foreach (KeyValuePair<int, float> kv in VALUE.Path_size) {
						int v = kv.Key;
						Queue<int> queue = new Queue<int> ();
						queue.Enqueue (v);
						while (queue.Count > 0) {
							v = queue.Dequeue ();
							float vw = messages [partition [v - 1]] [v];
							if (!messages [partition [v - 1]].TryGetValue (v, out tmp) || tmp > vw) {
								done = false;
								IEnumerator<KeyValuePair<int, float>> vneighbors = g.iteratorOutgoingVertexWeightOf (v);
								while (vneighbors.MoveNext ()) {
									int n = vneighbors.Current.Key;
									float nw = vneighbors.Current.Value + vw; 
									if (!messages [partition [n - 1]].TryGetValue (n, out tmp) || tmp > nw) {
										messages [partition [n - 1]] [n] = nw;
										queue.Enqueue (n);
										activates [partition [n - 1]] = true;
									}
								}
							}
						}
					}
				}
			}
		}

		public void gust0(){ //emissor de saída de dados
			IIteratorInstance<IKVPair<IInteger,IDataSSSP>> output_value_instance = (IIteratorInstance<IKVPair<IInteger,IDataSSSP>>)Output.Instance;

			bool any_activated = false;
			foreach (bool any in activates) 
				any_activated = any_activated || any;
			if (!any_activated) 
				output_value_instance.finish (); // Caso todos estejam Activated=false, TerminatedFunctionSSSP é avisado com um finish(), preparando-se para a emissão definitiva de saída

			for (int i = 0; i<partition_size;i++) {
				IKVPairInstance<IInteger,IDataSSSP> ITEM = (IKVPairInstance<IInteger,IDataSSSP>)Output.createItem ();
				IIntegerInstance KEY = (IIntegerInstance)ITEM.Key;
				IDataSSSPInstance VALUE = (IDataSSSPInstance)ITEM.Value;
				KEY.Value = i;

				if (partition_own [i] || !activates[i])
					VALUE.Path_size = new Dictionary<int, float> ();
				else
					VALUE.Path_size = messages [i];

				VALUE.Activated = activates[i];
				output_value_instance.put (ITEM);
			}
			activates = new bool[partition_size];
		}
	}
}

