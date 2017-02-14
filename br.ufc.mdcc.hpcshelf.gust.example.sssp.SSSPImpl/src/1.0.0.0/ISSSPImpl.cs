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
		private ConcurrentDictionary<int, float>[] messages = null;

		public override void main() {}
		public override void after_initialize() { }

		public bool isGhost(int v){ return !partition_own[this.partition [v - 1]]; }

		// START: Bloco de criacao do grafo direcionado com peso nas arestas
		public void graph_creator(){
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
					int t = gif.Source [i];
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
		}
		// END: Bloco de criacao do grafo direcionado com peso nas arestas

		public void startup_push(){
			IIteratorInstance<IKVPair<IInteger,IDataSSSP>> output_value_instance = (IIteratorInstance<IKVPair<IInteger,IDataSSSP>>)Output.Instance;
			IKVPairInstance<IInteger,IDataSSSP> item = (IKVPairInstance<IInteger,IDataSSSP>)Output.createItem ();
			IIntegerInstance ok = (IIntegerInstance)item.Key;
			IDataSSSPInstance ov = (IDataSSSPInstance)item.Value;

			//ok.Id = 1;
			//ov.Value = 0.0;
			//output_value_instance.put (item);
			int v;
			IEnumerator<KeyValuePair<int, float>> vneighbors = g.iteratorOutgoingVertexWeightOf (1);//.outgoingEdgesOf(1).GetEnumerator();
			while (vneighbors.MoveNext ()) {
				item = (IKVPairInstance<IVertex,IDataSSSP>)Output_kv.createItem ();
				ok = (IVertexInstance)item.Key;
				ov = (IDataSSSPInstance)item.Value;
				ok.Id = vneighbors.Current.target;
				ov.Value = vneighbors.Current.Weight;
				output_value_instance.put (item);
			}
		}

		public void pull(){
		}

		public void gust0(){
		}

	}
}

