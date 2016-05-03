using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.application.App;

namespace br.ufc.mdcc.hpcshelf.gust.application.impl.AppImpl
{
	public class IAppImpl : BaseIAppImpl, IApp
	{
		public override void main()
		{
		   writeFile("/home/cenez/workspace/gust/HPC-Shelf-Gust/log","AppImpl",true);
		}
		public static void writeFile(string PATH, string text, bool clear){
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(@PATH, !clear)){
				file.WriteLine(text);
			}
		}
	}
}
