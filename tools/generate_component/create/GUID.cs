using System;
using System.IO;
namespace generate {
	public class Program{
		public static void Main (string[] args){
			string file = "";
			try{
				file = args[0];
			} catch(Exception e){
				Console.WriteLine ("Indique arquivo xml");
				Console.WriteLine (e.Message);
				System.Environment.Exit (0);
			}
			Console.WriteLine ("Start ....");
			string text = readInput(file);
			string xml = split (text);
			writeFile (file,xml, true);
			Console.WriteLine ("           ........ End");
		}
		public static string split(string file){
			string tmp = "";
			string[] lines = file.Split(new char[] {System.Environment.NewLine[0]});
			foreach (string l in lines) {
				string line = l.Trim ();
				if (!line.Equals ("")) {
					if (line.Contains ("ProjectGuid") && !line.Contains ("<!--")) {
						tmp = tmp + "<ProjectGuid>{" + Guid.NewGuid () + "}</ProjectGuid>" + System.Environment.NewLine[0];
					}
					else {
						tmp = tmp + line + System.Environment.NewLine[0];
					}
				} 
			}
			return tmp;
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

