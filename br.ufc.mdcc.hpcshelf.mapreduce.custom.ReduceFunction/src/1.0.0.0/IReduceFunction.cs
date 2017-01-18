using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;

namespace br.ufc.mdcc.hpcshelf.mapreduce.custom.ReduceFunction
{
	public interface IReduceFunction<GIF, TKey, TValue, OKey, OValue, G> : BaseIReduceFunction<GIF, TKey, TValue, OKey, OValue, G>
		where GIF:IInputFormat//Data
		where TKey:IData
		where TValue:IData
		where OKey:IData
		where OValue:IData
		where G:IData
	{
		/* método poderia ser um componenten aninhado para construir grafos, guiado pelo contexto do componente "Graph". */
		void graph_creator(); 

		/* método diretamente ligado ao algoritmo */
		void startup_processing(); 

		/* O Algoritmo deve possuir pelo menos uma etapa 0 para iterar. 
		 Para mais etapas, deve-se escrever métodos na forma gustx, onde x>=1 */
		void gust0(); 

		// Antes de emitir KVPairs, pode-se avaliar se há redundância, uma espécie de combine para chave única.
		void output_filter();
	}
}