using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.example.tc3.TC2;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using br.ufc.mdcc.hpcshelf.gust.graph.UndirectedGraph;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerV;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeBasic;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.gust.example.tc.DataTriangle;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;

namespace br.ufc.mdcc.hpcshelf.gust.example.tc3.TC2Impl {
	public class ITC2Impl : BaseITC2Impl, ITC2 {

		private IUndirectedGraphInstance<IVertexBasic, IEdgeBasic<IVertexBasic>, int, IEdgeInstance<IVertexBasic, int>> g = null;
		private int[] partition = null;
		private bool[]  partition_own = null;
		private int partid = 0;
		private int partition_size = 0;
		private int count = 0;
		private IDictionary<int, IList<KeyValuePair<int,int>>> triangles = new Dictionary<int, IList<KeyValuePair<int,int>>>();

		public override void main() {}
		public override void after_initialize() { }

		public bool isGhost(int v){ return !partition_own[this.partition [v - 1]]; }

		#region Create Undirected Graph
		public void graph_creator(){ 
			IKVPairInstance<IInteger,IIterator<IInputFormat>> input_gifs_instance = (IKVPairInstance<IInteger,IIterator<IInputFormat>>)Graph_values.Instance;
			IIteratorInstance<IInputFormat> vgifs = (IIteratorInstance<IInputFormat>)input_gifs_instance.Value;

			object o;
			if (partition_own==null){
				if (vgifs.fetch_next (out o)) {
					IInputFormatInstance gif = (IInputFormatInstance)o;
					partition = gif.PartitionTABLE; 
					partid = gif.PARTID; 
					partition_size = gif.PARTITION_SIZE; 
					g = Graph.newInstance (gif.VSIZE); // pega-se uma instancia do graph, com vertices do tipo inteiro, com tamanho previsto VSIZE
					g.DataContainer.AllowingLoops = false; // não serão premitidos laços
					g.DataContainer.AllowingMultipleEdges = false; // não serão permitidas múltiplas arestas
					graph_creator_aux (gif); // inserem-se dados no grafo
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
				if (gif.Target [i] != 0) { // Será usada a forma canonica: i->j, onde i<j, i>0 j>0
					int s = gif.Source [i] < gif.Target [i] ? gif.Source [i] : gif.Target [i]; 
					int t = gif.Target [i] > gif.Source [i] ? gif.Target [i] : gif.Source [i];
					g.addVertex (s);
					g.addVertex (t);
					g.noSafeAdd (s, t);
					i++;
				}
			}
			IIteratorInstance<IKVPair<IInteger,IInputFormat>> output_gifs_instance = (IIteratorInstance<IKVPair<IInteger,IInputFormat>>)Output_gif.Instance;
			IKVPairInstance<IInteger,IInputFormat> item = (IKVPairInstance<IInteger,IInputFormat>)Output_gif.createItem ();
			((IIntegerInstance)item.Key).Value = gif.PARTID;
			item.Value = gif;
			output_gifs_instance.put (item); // Emite-se gif novamente para que a funcão de particionamento do conector receba a instancia PartitionTABLE.
		}                                    // Isso é necessário no caso de IKey ser do tipo IVertex.
		#endregion

		#region Algorithm implementation
		public void startup_push(){ }

		public void pull(){
			IKVPairInstance<IVertex,IIterator<IDataTriangle>> input_values_instance = (IKVPairInstance<IVertex,IIterator<IDataTriangle>>)Input_values.Instance;
			IVertexInstance ikey = (IVertexInstance)input_values_instance.Key;
			IIteratorInstance<IDataTriangle> ivalues = (IIteratorInstance<IDataTriangle>)input_values_instance.Value;
			IIteratorInstance<IKVPair<IVertex,IDataTriangle>> output_value = (IIteratorInstance<IKVPair<IVertex,IDataTriangle>>)Output.Instance;
			object o; int w = ikey.Id; 

			while (ivalues.fetch_next (out o)) {
				IEnumerator<int> wneighbors = g.iteratorNeighborsOf (w);
				while (wneighbors.MoveNext ()) { 
					int z = wneighbors.Current;
					if (w < z) { 
						IKVPairInstance<IVertex,IDataTriangle> item = (IKVPairInstance<IVertex,IDataTriangle>)Output.createItem ();
						((IVertexInstance)item.Key).Id = ((IDataTriangleInstance)o).V;
						((IDataTriangleInstance)item.Value).V = w;
						((IDataTriangleInstance)item.Value).W = z;
						output_value.put (item);
					}
				}
			}
		}
		public void gust0(){ }
		#endregion
	}
}
