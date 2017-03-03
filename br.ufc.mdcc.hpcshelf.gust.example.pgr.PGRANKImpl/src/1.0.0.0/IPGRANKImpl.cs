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
using br.ufc.mdcc.hpcshelf.gust.example.pgr.DataPGRANK;
using br.ufc.mdcc.hpcshelf.gust.example.pgr.PGRANK;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using br.ufc.mdcc.hpcshelf.gust.graph.DirectedGraph;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerKV;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeWeighted;

namespace br.ufc.mdcc.hpcshelf.gust.example.pgr.PGRANKImpl {
	public class IPGRANKImpl : BaseIPGRANKImpl, IPGRANK {

		private IDirectedGraphInstance<IVertex, IEdgeWeighted<IVertex>, int, IEdgeInstance<IVertex, int>> g = null;
		private int[] partition = null;
		private bool[]  partition_own = null;
		private int partition_size = 0;
		private float nothing_outgoing = 0;
		private float sum_nothings = 0.0f;
		private int partid = 0;
		private int iteration_sum = 0;
		private int num_iteration = 30;
		IDictionary<int, float> before = null;
		private IDictionary<int, float>[] messages = null;

		public override void main() {} 
		public override void after_initialize() { }
		public bool isGhost(int v){ return !partition_own[this.partition [v - 1]]; }

		#region Create Directed Graph Weight
		public void graph_creator(){
			IKVPairInstance<IInteger,IIterator<IInputFormat>> input_gifs_instance = (IKVPairInstance<IInteger,IIterator<IInputFormat>>)Graph_values.Instance;
			IIteratorInstance<IInputFormat> vgifs = (IIteratorInstance<IInputFormat>)input_gifs_instance.Value;

			object o;
			if (partition_own==null){
				if (vgifs.fetch_next (out o)) {
					IInputFormatInstance gif = (IInputFormatInstance)o;
					partition = gif.PartitionTABLE; 
					partition_size = gif.PARTITION_SIZE; 
					g = Graph.newInstance (gif.VSIZE);
					g.DataContainer.AllowingLoops = false;
					g.DataContainer.AllowingMultipleEdges = false;
					graph_creator_aux (gif);
					partition_own = new bool[partition_size];
					partition_own [gif.PARTID] = true;
					this.partid = gif.PARTID;
				}
			}
			while (vgifs.fetch_next (out o)) {
				graph_creator_aux ((IInputFormatInstance)o);
				partition_own [((IInputFormatInstance)o).PARTID] = true;
			}
		}
		private void graph_creator_aux(IInputFormatInstance gif){
			for (int i = 0; i < gif.ESIZE;i++) {
				int s = gif.Source [i]; 
				int t = gif.Target [i];
				g.addVertex (s);
				g.addVertex (t);
				g.addEdge (s, t);
				if (s == 0 || t==0) { throw new ArgumentNullException ("WARNING: Vertex id is 0! "); }
			}
			IIteratorInstance<IKVPair<IInteger,IInputFormat>> output_gifs_instance = (IIteratorInstance<IKVPair<IInteger,IInputFormat>>)Output_gif.Instance;
			IKVPairInstance<IInteger,IInputFormat> item = (IKVPairInstance<IInteger,IInputFormat>)Output_gif.createItem ();
			((IIntegerInstance)item.Key).Value = gif.PARTID;
			item.Value = gif;
			output_gifs_instance.put (item);
		}
		#endregion

		#region Algorithm implementation
		public void startup_push() {
			before = new Dictionary<int, float>(g.countV()+1);
			messages = new Dictionary<int, float>[partition_size];
			for (int i = 0; i < partition_size; i++) messages[i] = new Dictionary<int, float> ();

			ICollection<int> vertices = g.vertexSet (); 
			foreach (int v in vertices) {
				if (!messages [partition [v - 1]].ContainsKey (v)) messages [partition [v - 1]] [v] = 0.0f;
				if (!isGhost (v)) { 
					bool any = false;
					before [v] = 0.0f;
					IEnumerator<int> n = g.iteratorOutgoingVertexOf (v); 
					while (n.MoveNext ()) { 
						any = true;
						if (!messages [partition [n.Current - 1]].ContainsKey (n.Current)) messages [partition [n.Current - 1]] [n.Current] = 0.0f;
						messages [partition [n.Current - 1]] [n.Current] += 1.0f / g.outDegreeOf (v);
					}
					if (!any) nothing_outgoing+=1.0f;
				}
			}
			emite ();
		}
		public void gust0(){
			IIteratorInstance<IKVPair<IInteger,IDataPGRANK>> output_value_instance = (IIteratorInstance<IKVPair<IInteger,IDataPGRANK>>)Output.Instance;

			ICollection<int> vertices = g.vertexSet ();
			foreach (int v in vertices)
				if (!isGhost (v)) {
					before [v] = messages [partition [v - 1]] [v] + sum_nothings - before [v];
					messages[partition [v - 1]] [v] = before [v];
				}
			if(iteration_sum<num_iteration)
				foreach (int v in vertices) {
					if (!isGhost (v)) {
						bool any = false;
						IEnumerator<int> n = g.iteratorOutgoingVertexOf (v);
						while (n.MoveNext ()) {
							any = true;
							if (!messages [partition [n.Current - 1]].ContainsKey (n.Current)) messages [partition [n.Current - 1]] [n.Current] = 0.0f;
							messages [partition [n.Current - 1]] [n.Current] += before [v] / g.outDegreeOf (v);
						}
						if (!any) nothing_outgoing += before [v];
					}
				}
			emite ();
		}

		public void pull() {
			IKVPairInstance<IInteger,IIterator<IDataPGRANK>> input_values_instance = (IKVPairInstance<IInteger,IIterator<IDataPGRANK>>)Input_values.Instance;
			IIntegerInstance ikey = (IIntegerInstance)input_values_instance.Key;
			IIteratorInstance<IDataPGRANK> ivalues = (IIteratorInstance<IDataPGRANK>)input_values_instance.Value;

			object o;
			while (ivalues.fetch_next (out o)) {
				IDataPGRANKInstance VALUE = (IDataPGRANKInstance)o;
				foreach (KeyValuePair<int, float> kv in VALUE.Ranks) 
					messages [partition [kv.Key - 1]] [kv.Key] += kv.Value;
				sum_nothings += VALUE.Slice;
			}
		}
		private void emite(){
			IIteratorInstance<IKVPair<IInteger,IDataPGRANK>> output_value_instance = (IIteratorInstance<IKVPair<IInteger,IDataPGRANK>>)Output.Instance;
			if (iteration_sum == num_iteration) {
				output_value_instance.finish ();

				IKVPairInstance<IInteger,IDataPGRANK> ITEM = (IKVPairInstance<IInteger,IDataPGRANK>)Output.createItem ();
				((IIntegerInstance)ITEM.Key).Value = this.partid;
				((IDataPGRANKInstance)ITEM.Value).Ranks = messages [((IIntegerInstance)ITEM.Key).Value];
				output_value_instance.put (ITEM);

			} else {
				for (int i = 0; i < partition_size; i++) {
					IKVPairInstance<IInteger,IDataPGRANK> ITEM = (IKVPairInstance<IInteger,IDataPGRANK>)Output.createItem ();
					((IIntegerInstance)ITEM.Key).Value = i;

					if (partition_own [i])
						((IDataPGRANKInstance)ITEM.Value).Ranks = new Dictionary<int, float> ();
					else {
						((IDataPGRANKInstance)ITEM.Value).Ranks = messages [i];
						messages [i] = new Dictionary<int, float> ();
					}
					((IDataPGRANKInstance)ITEM.Value).Slice = nothing_outgoing / ((float)partition.Length);

					output_value_instance.put (ITEM);
				}
			}
			iteration_sum++;
			nothing_outgoing = 0.0f;
			sum_nothings = 0.0f;
		}
		#endregion
	}
}

