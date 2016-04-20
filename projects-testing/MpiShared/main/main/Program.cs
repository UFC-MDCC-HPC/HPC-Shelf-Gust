using System;
using System.IO;
using System.Runtime.Remoting;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MPI;

namespace GRAFOS{

	class Program{
		[DllImport("libshared.so", EntryPoint="func_c")]
		static extern int func_c(int argc, string[] argv);

		public static void Main (string[] args){


		
			MPI.Environment mpi = new MPI.Environment(ref args);
			MPI.Intracommunicator worldcomm = MPI.Communicator.world;
			int np = worldcomm.Size;
			int node = worldcomm.Rank;

			//Process P = System.Diagnostics.Process.Start ("/bin/hostname");

			func_c(args.Length, args);

			worldcomm.Barrier ();
			if(node==0) System.Console.WriteLine ("*************************************");
			worldcomm.Barrier ();

			int soma = worldcomm.Reduce<int>(node, Operation<int>.Add, 0);
			System.Console.WriteLine("RANK.NET: "+node+" SIZE: "+np+" soma: "+soma);
			mpi.Dispose();

		}
	}
}
