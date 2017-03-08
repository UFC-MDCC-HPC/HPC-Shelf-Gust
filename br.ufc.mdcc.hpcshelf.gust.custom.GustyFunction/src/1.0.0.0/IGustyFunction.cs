using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;

namespace br.ufc.mdcc.hpcshelf.gust.custom.GustyFunction
{
	public interface IGustyFunction<GIF, TKey, TValue, OKey, OValue, G> : BaseIGustyFunction<GIF, TKey, TValue, OKey, OValue, G>
		where GIF:IInputFormat//Data
		where TKey:IData
		where TValue:IData
		where OKey:IData
		where OValue:IData
		where G:IData
	{
		// Método deve ser um componente aninhado, construtor de grafos
		void graph_creator(); 

		// Método getMessages
		void pull(); 

		// Iniciador do algoritmo, faz envio primário de mensagens
		void startup_push(); 

		// Algoritmo deve possuir pelo menos uma etapa, onde a assinatura é gustx, para x>=0
		void gust0(); 
	}
}