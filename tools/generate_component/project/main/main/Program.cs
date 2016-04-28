using System;
using System.IO;
//using System.Collections.Generic;

namespace br.ufc.mdcc.gust {

	public class Program{
		public static void Main (string[] args){
			
		}
		public static string readInput (string file){
			return System.IO.File.ReadAllText(file);

		}
		public static void writeFile(string PATH, string saida, bool clear){
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(@PATH, !clear)){
				file.WriteLine(saida);
			}
		}
	}
}
