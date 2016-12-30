using System;
using System.Collections;
using System.Collections.Generic;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.InputFormatImpl {
	public class IInputFormatImpl : BaseIInputFormatImpl, IData {
		public IInputFormatImpl () {
		}

		override public void after_initialize () {
			newInstance (); 
		}


		public IInputFormatInstance newInstanceIF () {
			IInputFormatInstance instance = (IInputFormatInstance)newInstance ();
			return instance;
		}

		public object newInstance () {
			this.instance = new IInputFormatInstanceImpl ();
			return this.Instance;
		}

		private IInputFormatInstance instance;

		public object Instance {
			get { return instance; }
			set { this.instance = (IInputFormatInstance)value; }
		}
	}


	[Serializable]
	public class IInputFormatInstanceImpl: IInputFormatInstance {
		public IInputFormatInstanceImpl () {
			source = new int[1];
			target = new int[1];
			weight = new float[1];
			partition_table = new int[1];
		}

		#region IInputFormatInstance implementation
		public static int DEFAULT_PARTITION = 8;
		private int[] source;
		private int[] target;
		private float[] weight;
		private int[] partition_table;
		private int esize = 0;
		private int vsize = 0;
		private int part = DEFAULT_PARTITION;
		private int count = 0;

		public int[] Source { get{ return source; } }
		public int[] Target { get{ return target; } }
		public int[] PartitionTABLE { get{ return partition_table; } set { partition_table = (int[]) value; } }
		public float[] Weight { get{ return weight; } }

		public int ESIZE { get{ return esize; } }
		public int VSIZE { get{ return vsize; } }
		public int PART { get{ return part; } set { part = (int) value; } }

		private int[] getInts() { int[] INTS = {esize,vsize,part,count}; return INTS; }
		public object ObjValue {
			get { return new Tuple<int[],int[],float[],int[],int[]>(source,target,weight,partition_table,getInts()); }
			set { 
				this.source =          ((Tuple<int[],int[],float[],int[],int[]>)value).Item1;
				this.target =          ((Tuple<int[],int[],float[],int[],int[]>)value).Item2;
				this.weight =          ((Tuple<int[],int[],float[],int[],int[]>)value).Item3;
				this.partition_table = ((Tuple<int[],int[],float[],int[],int[]>)value).Item4;
				int[] INTS = ((Tuple<int[],int[],float[],int[],int[]>)value).Item5;
				esize = INTS[0]; vsize = INTS[1]; part = INTS[2]; count = INTS[3];
			}
		}

		public IDictionary<int, IInputFormatInstance> extractBins(){
			this.extractFile();
			IDictionary<int, IInputFormatInstance> dic = new Dictionary<int, IInputFormatInstance> (this.PART);
			bool weighted = this.Weight.Length > 1;
			for (int i = 0; i < this.PART; i++) {
				dic [i] = new IInputFormatInstanceImpl ();
			}
			for (int i = 0; i < this.ESIZE; i++) {
				int s = this.Source [i];
				int t = this.Target [i];
				int spart = this.PartitionTABLE [s - 1];
				int tpart = this.PartitionTABLE [t - 1];
				if (!weighted) {
					dic [spart].Add (s, t);
					if(spart!=tpart)
						dic [tpart].Add (s, t);
				} else {
					float f = this.Weight[i];
					dic [spart].Add (s, t, f);
					if(spart!=tpart)
						dic [tpart].Add (s, t, f);
				}
			}
			for (int i = 0; i < this.PART; i++) {
				IInputFormatInstance tmp = dic [i]; 
				tmp.Trim ();
				tmp.PartitionTABLE = this.PartitionTABLE;
				tmp.PART = this.PART;
				//Graphviz.Directed (tmp.Source, tmp.Target, tmp.VPartition, "/home/cenez/Dropbox/Programacao/JGraphT/Graph-In-HPC-Shelf/tmp.dot");
			}
			source = new int[1];
			target = new int[1];
			weight = new float[1];
			esize = 0;
			vsize = 0;
			count = 0;
			return dic;
		}
		public void Trim(){
			Array.Resize (ref source, count);
			Array.Resize (ref target, count);
			if(weight.Length>1) Array.Resize (ref weight, count);
			esize = count;
		}
		public int firstVertex(int partID){
			for (int i = 0; i < partition_table.Length; i++) {
				if (partition_table [i] == partID) {
					return i + 1;
				}
			}
			return -1;
		}
		public void Add(int i, int j){
			if (count == source.Length) {
				Array.Resize (ref source, source.Length * 2);
				Array.Resize (ref target, target.Length * 2);
			}
			source [count] = i;
			target [count++] = j;
			int tmp = max (i, j);
			if (vsize < tmp)
				vsize = tmp;
		}
		private int max(int i, int j){ return i > j ? i : j; }
		public void Add(int i, int j, float f){
			this.Add (i, j);
			if ((count-1) == weight.Length) {
				Array.Resize (ref weight, weight.Length * 2);
			} 
			weight [count-1] = f;
		}
		public void Clear (){
			source = new int[1];
			target = new int[1];
			weight = new float[1];
			partition_table = new int[1];
			esize = 0;
			vsize = 0;
			part = DEFAULT_PARTITION;
			count = 0;
		}
		public void extractFile(){
			string fileName = System.Environment.GetEnvironmentVariable ("PATH_GRAPH_FILE");
			byte[] b = {9, 13, 32};
			System.IO.StreamReader file = null;
			checkFiles (fileName, b, file);
			try	{
				string line; bool weighted = false;
				file = new System.IO.StreamReader(fileName);

				if((line = file.ReadLine()) != null) {
					if (!(line.Trim().Equals (""))) {
						string[] ij = line.Split ((char)b[0],(char)b[1],(char)b[2]);
						int i = int.Parse(ij[0]);
						int j = int.Parse(ij[1]);
						if(ij.Length==2)
							this.Add(i,j);
						else 
							if(ij.Length==3){
								weighted = true;
								weight = new float[esize];
								this.Add(i,j,float.Parse(ij[2]));
							}
					}
				}
				while((line = file.ReadLine()) != null) {
					if (!(line.Trim().Equals (""))) {
						string[] ij = line.Split ((char)b[0],(char)b[1],(char)b[2]);
						int i = int.Parse(ij[0]);
						int j = int.Parse(ij[1]);
						if(!weighted) this.Add(i,j);
						else this.Add(i,j,float.Parse(ij[2]));
					}
				}

			}
			catch (System.IO.IOException e) {
				Console.WriteLine("Error reading from {0}. Message = {1}", fileName, e.Message);
			}
			finally {
				if (file != null) {
					file.Close();
				}
			}
		}
		private void checkFiles(string fileName, byte[] b, System.IO.StreamReader file){
			bool graph = System.IO.File.Exists (fileName);
			bool headExists = System.IO.File.Exists (fileName+".head");
			if (!graph)
				throw new EntryPointNotFoundException ("** PATH_GRAPH_FILE **: Environment Variable NOT FOUND EXCEPTION");
			try	{
				string line;
				if(headExists){
					file = new System.IO.StreamReader(fileName+".head");
					while((line = file.ReadLine()) != null) {
						if ( !(line.Trim().Equals ("")) && !(line.Trim()[0].Equals ('#'))) {
							string[] ij = line.Split ((char)b[0],(char)b[1],(char)b[2]);
							vsize = int.Parse(ij[0]);
							esize = int.Parse(ij[1]);
							part =  int.Parse(ij[2]);
							break;
						}
					}
				} else {
					try	{
						file = new System.IO.StreamReader(fileName);
						while((line = file.ReadLine()) != null) {
							if (!(line.Trim().Equals (""))) {
								string[] ij = line.Split ((char)b[0],(char)b[1],(char)b[2]);
								int i = int.Parse(ij[0]);
								int j = int.Parse(ij[1]);
								esize++;
								int tmp = max (i, j);
								if (vsize < tmp)
									vsize = tmp;
							}
						}
					}
					catch (System.IO.IOException e) {
						Console.WriteLine("Error reading from {0}. Message = {1}", fileName, e.Message);
					}
					finally {
						if (file != null) {
							file.Close();
						}
					}
					System.IO.StreamWriter wfile =  new System.IO.StreamWriter(fileName+".head");
					try{
						wfile.WriteLine(vsize+" "+esize+" "+part);
					}
					catch (System.IO.IOException e) {
						Console.WriteLine("Error writing from {0}. Message = {1}", fileName+".head", e.Message);
					}
					finally {
						wfile.Close();
						if (wfile != null) 
							wfile.Dispose();
					}
				}
			}
			catch (System.IO.IOException e) {
				Console.WriteLine("Error reading from {0}. Message = {1}", fileName+".head", e.Message);
			}
			finally {
				if (file != null) {
					file.Close();
				}
			}
			source = new int[esize];
			target = new int[esize]; //weight = new float[esize];
			partition_table = new int[vsize];
			bool metisPart = System.IO.File.Exists (fileName+".metis.part."+part);
			int idx = 0;
			try	{
				string line;
				if(metisPart){
					file = new System.IO.StreamReader(fileName+".metis.part."+part);
					while((line = file.ReadLine()) != null) {
						if (!(line.Trim().Equals (""))) {
							string[] p = line.Split ((char)b[0],(char)b[1],(char)b[2]);
							int i = int.Parse(p[0]);
							PartitionTABLE[idx++] = i;
						}
					}
				}
				else {
					int tmp = 0; //System.IO.StreamWriter wfile =  new System.IO.StreamWriter(fileName+".metis.part."+part);
					for(int i=0;i<vsize;i++){
						partition_table[i] = tmp; //wfile.Write(""+tmp+Environment.NewLine);
						if((i%part)==part-1) tmp = ++tmp==part?0:tmp;
					} //wfile.Close();
				}
			}
			catch (System.IO.IOException e) {
				Console.WriteLine("Error reading from {0}. Message = {1}", fileName+".metis.part."+part, e.Message);
			}
			finally {
				if (file != null) {
					file.Close();
				}
			}
		}

		#endregion

		#region ICloneable implementation

		public object Clone () {
			IInputFormatInstanceImpl clone = new IInputFormatInstanceImpl ();
			clone.source = (int[])this.source.Clone ();
			clone.target = (int[])this.target.Clone ();
			clone.weight = (float[])this.weight.Clone ();
			clone.partition_table = (int[])this.partition_table.Clone ();
			clone.esize = this.esize;
			clone.vsize = this.vsize;
			clone.part = this.part;
			clone.count = this.count;
			return clone;
		}

		#endregion

	}


}
