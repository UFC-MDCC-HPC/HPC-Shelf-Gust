using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeSinglePartner;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeDataSourceInterface {
	public interface IPortTypeDataSourceInterface : BaseIPortTypeDataSourceInterface, IEnvironmentPortTypeSinglePartner {
		//string[] fetchFileNames ();
		//IEnumerable<string> fetchFileContents(string fileName);
		IEnumerable<IInputFormatInstance> fetchFileContent();
	}

	// IInputFormatInstance contém os dados do arquivo fonte, na forma int[0..|E|] Source, int[0..|E|] Target, onde |E| é o total de arestas i j em cada linha do arquivo.
	// Executando IInputFormatInstance.extractBins() na instancia, os dados IInputFormatInstance.Source, IInputFormatInstance.Target 
	// são particionados, sendo gerada a tabela de partes int[0 .. |V|] PartitionTABLE, onde |V| é o total de vértices.
	// cada IInputFormatInstance.PartitionTABLE[VERTICEID] contém um inteiro de 0 até IInputFormatInstance.PARTITION_SIZE.
	// IInputFormatInstance.extractBins() retorna um Dictionary<int, IInputFormatInstance>, onde chave (mesmo valor que IInputFormatInstance.PARTID) é a partiçao que pertece o IInputFormatInstance.
	// É necessária a variável de ambiente PATH_GRAPH_FILE, apontando para o endereço do arquivo fonte de dados do grafo
}