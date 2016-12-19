/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.String;
using br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction;

namespace br.ufc.mdcc.hpcshelf.gust.impl.custom.PartitionFunctionStringKeyDefault { 

public abstract class BaseIPartitionStringKeyDefaultImpl<TKey>: Computation, BaseIPartitionFunction<TKey>
where TKey:IString
{

private IInteger output_key = null;

public IInteger Output_key {
	get {
		if (this.output_key == null)
			this.output_key = (IInteger) Services.getPort("output_key");
		return this.output_key;
	}
}

private TKey input_key = default(TKey);

public TKey Input_key {
	get {
		if (this.input_key == null)
			this.input_key = (TKey) Services.getPort("input_key");
		return this.input_key;
	}
}



}

}
