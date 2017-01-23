using System;
using System.Collections.Generic;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.example.tc.TriangleCount;
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

namespace br.ufc.mdcc.hpcshelf.gust.example.tc.TriangleCountImpl {
	public class ITriangleCountImpl : BaseITriangleCountImpl, ITriangleCount {

		private IUndirectedGraphInstance<IVertexBasic, IEdgeBasic<IVertexBasic>, int, IEdgeInstance<IVertexBasic, int>> g = null;
		private int[] partition = null;
		private int partid = 0;
		private int partition_size = 0;

		public override void main() {}
		public override void after_initialize() { }

		public void graph_creator(){ //Chamado uma vez pelo processo Redutor
			IKVPairInstance<IInteger,IIterator<IInputFormat>> input_gifs_instance = (IKVPairInstance<IInteger,IIterator<IInputFormat>>) Graph_values.Instance;
			IIteratorInstance<IInputFormat> vgifs = (IIteratorInstance<IInputFormat>)input_gifs_instance.Value;

			object o;
			if (vgifs.fetch_next (out o)) {
				IInputFormatInstance gif = (IInputFormatInstance)o;
				// grafa-se informações que poderão auxiliar nas decisões de emissão de KVPairs
				partition = gif.PartitionTABLE; 
				partid = gif.PARTID; 
				partition_size = gif.PARTITION_SIZE; 
				g = Graph.newInstance (gif.VSIZE); // pega-se instancia do graph, com previsão de tamanho para VSIZE
				g.DataContainer.AllowingLoops = false; // não serão premitidos laços
				g.DataContainer.AllowingMultipleEdges = false; // não serão permitidas múltiplas arestas
				push_data_graph (gif); // inserem-se dados no grafo
			}
			while (vgifs.fetch_next (out o)) {
				push_data_graph ((IInputFormatInstance)o);
			}
		}
		private void push_data_graph(IInputFormatInstance gif){ //funcao privada auxiliar de graph_creator()
			for (int i = 0; i < gif.ESIZE;) {
				if (gif.Target [i] != 0) { // Será usada a forma canonica: i->j, onde i<j
					int s = gif.Source [i] < gif.Target [i] ? gif.Source [i] : gif.Target [i]; 
					int t = gif.Source [i] > gif.Target [i] ? gif.Source [i] : gif.Target [i];
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
			output_gifs_instance.put (item); // emite-se gif novamente: para a funcão de particionamento do conector setar o PartitionTABLE.
		}

		public void startup_processing(){ // Chamado uma vez pelo processo Redutor, após graph_creator() ser completado. Aqui começa o algoritmo
			IIteratorInstance<IKVPair<IInteger,IDataTriangle>> output_value_instance = (IIteratorInstance<IKVPair<IInteger,IDataTriangle>>)Output.Instance;
			int v, ordered, i, j;
			IEnumerator<int> V = g.vertexSet ().GetEnumerator ();
			while (V.MoveNext ()) { // itera em todo vertice da particao partid
				v = V.Current;
				if (this.partition [v - 1] == this.partid) {
					IEnumerator<int> vneighbors = g.iteratorNeighborsOf (v);
					while (vneighbors.MoveNext ()) {
						int maior = vneighbors.Current;
						if (v < maior) { //busca nos vizinhos os vérices maiores
							IKVPairInstance<IInteger,IDataTriangle> item = (IKVPairInstance<IInteger,IDataTriangle>)Output.createItem ();
							IIntegerInstance ok = (IIntegerInstance)item.Key;
							IDataTriangleInstance ov = (IDataTriangleInstance)item.Value;
							ok.Value = maior;
							ov.V = v;
							output_value_instance.put (item);
						}
					}
				}
			}
		}

		public void gust0(){// Uma vez que o startup_processing() efetuou a etapa 1, sendo a primeira iteração, gust0 é a etapa 2 do algoritmo, de um total de 3 iterações
			IKVPairInstance<IInteger,IIterator<IDataTriangle>> input_values_instance = (IKVPairInstance<IInteger,IIterator<IDataTriangle>>)Input_values.Instance;
			IIteratorInstance<IKVPair<IInteger,IDataTriangle>> output_value_instance = (IIteratorInstance<IKVPair<IInteger,IDataTriangle>>)Output.Instance;

			IIntegerInstance ikey = (IIntegerInstance)input_values_instance.Key;
			IIteratorInstance<IDataTriangle> ivalues = (IIteratorInstance<IDataTriangle>)input_values_instance.Value;

			IList<int> l = new List<int> (); int w = ikey.Value;
			object o;
			while (ivalues.fetch_next (out o)) { // guarda em lista os vizinhos v's menores emitidos na fase anterior
				l.Add(((IDataTriangleInstance)o).V);
			}

			IEnumerator<int> wneighbors = g.iteratorNeighborsOf (w);
			while (wneighbors.MoveNext ()) { 
				int z = wneighbors.Current;
				if (w < z) { // busca-se nos vizinhos os vértices maiores. Se z for vizinho de v na fase seguinte, haverá um triangulo
					foreach (int v in l) {
						IKVPairInstance<IInteger,IDataTriangle> item = (IKVPairInstance<IInteger,IDataTriangle>)Output.createItem ();
						IIntegerInstance ok = (IIntegerInstance)item.Key;
						IDataTriangleInstance ov = (IDataTriangleInstance)item.Value;
						ok.Value = v;
						ov.V = z;
						ov.W = w;
						output_value_instance.put (item);
					}
				}
			}
		}

		public void gust1(){ //Etapa final, ou iteração 3
			IKVPairInstance<IInteger,IIterator<IDataTriangle>> input_values_instance = (IKVPairInstance<IInteger,IIterator<IDataTriangle>>)Input_values.Instance;
			IIteratorInstance<IKVPair<IInteger,IDataTriangle>> output_value_instance = (IIteratorInstance<IKVPair<IInteger,IDataTriangle>>)Output.Instance;

			IIntegerInstance ikey = (IIntegerInstance)input_values_instance.Key;
			IIteratorInstance<IDataTriangle> ivalues = (IIteratorInstance<IDataTriangle>)input_values_instance.Value;

			int v = ikey.Value;
			ICollection<int> vneighbors = g.neighborsOf (v);
			object o;
			while (ivalues.fetch_next (out o)) {
				IDataTriangleInstance dt = ((IDataTriangleInstance)o);
				int z = dt.V;
				int w = dt.W;
				if (vneighbors.Contains (z)) { //Se z é vizinho de v, forma-se um triangulo
					IKVPairInstance<IInteger,IDataTriangle> item = (IKVPairInstance<IInteger,IDataTriangle>)Output.createItem ();
					((IIntegerInstance)item.Key).Value = v;
					item.Value = dt;
					output_value_instance.put (item);
				}
			}
		}
		public void output_filter(){} // Em cada iteração, o redutor chama uma única vez, permitindo ao desenvolvedor avaliar dados a serem emitidos. Por exemplo: evitando redundância, ou descartando valores.
	}
}
