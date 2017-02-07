using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
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
		private bool[]  partition_own = null;
		private int partid = 0;
		private int partition_size = 0;
		private ConcurrentDictionary<int, IList<KeyValuePair<int,int>>> messages = new ConcurrentDictionary<int, IList<KeyValuePair<int,int>>>();

		public override void main() {}
		public override void after_initialize() { }

		public bool isGhost(int v){ return !partition_own[this.partition [v - 1]]; }//return this.partition [v - 1] != this.partid; }
		public void graph_creator(){ //Chamado uma vez pelo processo Redutor
			IKVPairInstance<IInteger,IIterator<IInputFormat>> input_gifs_instance = (IKVPairInstance<IInteger,IIterator<IInputFormat>>)Graph_values.Instance;
			IIteratorInstance<IInputFormat> vgifs = (IIteratorInstance<IInputFormat>)input_gifs_instance.Value;

			object o;
			if (partition_own==null){
				if (vgifs.fetch_next (out o)) {
					IInputFormatInstance gif = (IInputFormatInstance)o;
					// grava-se informações que poderão auxiliar nas decisões de emissão de KVPairs
					partition = gif.PartitionTABLE; 
					partid = gif.PARTID; 
					partition_size = gif.PARTITION_SIZE; 
					g = Graph.newInstance (gif.VSIZE); // pega-se instancia do graph, com previsão de tamanho para VSIZE
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
		private void graph_creator_aux(IInputFormatInstance gif){ //funcao privada auxiliar de graph_creator()
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

		public void pull(){
			IKVPairInstance<IVertex,IIterator<IDataTriangle>> input_values_instance = (IKVPairInstance<IVertex,IIterator<IDataTriangle>>)Input_values.Instance;
			IVertexInstance ikey = (IVertexInstance)input_values_instance.Key;
			IIteratorInstance<IDataTriangle> ivalues = (IIteratorInstance<IDataTriangle>)input_values_instance.Value;

			object o;
			while (ivalues.fetch_next (out o)) {
				IList<KeyValuePair<int,int>> l;
				if(!messages.TryGetValue(ikey.Id, out l)){
					l = new List<KeyValuePair<int,int>> ();
					messages[ikey.Id] = l;
				}
				l.Add(new KeyValuePair<int,int>( ((IDataTriangleInstance) o).V, ((IDataTriangleInstance) o).W ) );
			}
		}

		public void startup_push(){ // Chamado uma vez pelo processo Redutor, após graph_creator() ser completado. Aqui começa o algoritmo
			IIteratorInstance<IKVPair<IVertex,IDataTriangle>> output_value_instance = (IIteratorInstance<IKVPair<IVertex,IDataTriangle>>)Output.Instance;
			int v, ordered, i, j;
			IEnumerator<int> V = g.vertexSet ().GetEnumerator ();
			while (V.MoveNext ()) { // itera em todo vertice da particao partid
				v = V.Current;
				if (!isGhost(v)) {
					IEnumerator<int> vneighbors = g.iteratorNeighborsOf (v);
					while (vneighbors.MoveNext ()) {
						int bigger = vneighbors.Current;
						if (v < bigger) { //busca nos vizinhos os vérices maiores
							if (isGhost(bigger)) { // fantasma: true se bigger estiver fora da particao local
								IKVPairInstance<IVertex,IDataTriangle> item = (IKVPairInstance<IVertex,IDataTriangle>)Output.createItem ();
								IVertexInstance ok = (IVertexInstance)item.Key;
								IDataTriangleInstance ov = (IDataTriangleInstance)item.Value;
								ok.Id = bigger;
								ov.V = v;
								output_value_instance.put (item);
							} else {
								IList<KeyValuePair<int,int>> l;
								if(!messages.TryGetValue(bigger, out l)){
									l = new List<KeyValuePair<int,int>> ();
									messages[bigger] = l;
								}
								l.Add (new KeyValuePair<int,int>(v,0));
							}
						}
					}
				}
			}
		}

		public void gust0(){
			IIteratorInstance<IKVPair<IVertex,IDataTriangle>> output_value_instance = (IIteratorInstance<IKVPair<IVertex,IDataTriangle>>)Output.Instance;

			ConcurrentDictionary<int, IList<KeyValuePair<int,int>>> buffer_tmp = new ConcurrentDictionary<int, IList<KeyValuePair<int,int>>>();

			IEnumerator<int> next = messages.Keys.GetEnumerator();

			while(next.MoveNext()){
				int w = next.Current;//kv.Key;
				IList<KeyValuePair<int,int>> l;// = kv.Value;
				messages.TryRemove(w, out l);
				IEnumerator<int> wneighbors = g.iteratorNeighborsOf (w);
				while (wneighbors.MoveNext ()) { 
					int z = wneighbors.Current;
					if (w < z) { 
						foreach (KeyValuePair<int,int> kvw in l) {
							int v = kvw.Key;
							if (isGhost (v)) {
								IKVPairInstance<IVertex,IDataTriangle> item = (IKVPairInstance<IVertex,IDataTriangle>)Output.createItem ();
								IVertexInstance ok = (IVertexInstance)item.Key;
								IDataTriangleInstance ov = (IDataTriangleInstance)item.Value;
								ok.Id = v;
								ov.V = z;
								ov.W = w;
								output_value_instance.put (item);
							} else {
								IList<KeyValuePair<int,int>> lz;
								if(!buffer_tmp.TryGetValue(v, out lz)){
									lz = new List<KeyValuePair<int,int>> ();
									buffer_tmp[v] = lz;
								}
								lz.Add (new KeyValuePair<int,int>(z,w));
							}
						}
					}
				}
			}
			messages = buffer_tmp;
		}

		public void gust1(){
			IIteratorInstance<IKVPair<IVertex,IDataTriangle>> output_value_instance = (IIteratorInstance<IKVPair<IVertex,IDataTriangle>>)Output.Instance;
			IEnumerator<int> next = messages.Keys.GetEnumerator();
			while(next.MoveNext()){
				int v = next.Current;
				IList<KeyValuePair<int,int>> l;// = kv.Value;
				messages.TryRemove(v, out l);

				ICollection<int> vneighbors = g.neighborsOf (v); // devolve ISet<int>, de modo que Contains() é O(1)
				foreach(KeyValuePair<int,int> kv in l){
					int z = kv.Key;//dt.V;
					int w = kv.Value;//dt.W;
					if (vneighbors.Contains (z)) { //Se z é vizinho de v, forma-se um triangulo

						// Descomentar para imprimir todos os triangulos
						//					IKVPairInstance<IVertex,IDataTriangle> item = (IKVPairInstance<IVertex,IDataTriangle>)Output.createItem ();
						//					((IVertexInstance)item.Key).Value = v;
						//					item.Value = dt;
						//					output_value_instance.put (item);

						count++;
					}
				}
			}
			IKVPairInstance<IVertex,IDataTriangle> item = (IKVPairInstance<IVertex,IDataTriangle>)Output.createItem ();
			((IVertexInstance)item.Key).Id = count;
			//IDataTriangleInstance dt = ((IDataTriangleInstance)item.Value);
			//item.Value = dt;
			output_value_instance.put (item);
		}
		private int count = 0;
	}
}
