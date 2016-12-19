/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.example.cw.WordCounter;
using br.ufc.mdcc.common.String;
using br.ufc.mdcc.common.Integer;

namespace br.ufc.mdcc.hpcshelf.gust.example.cw.impl.WordCounterImpl { 

public abstract class BaseIWordCounterImpl: Computation, BaseIWordCounter
{

	private IIterator<IKVPair<IString,IInteger>> output_data = null;

	public IIterator<IKVPair<IString,IInteger>> Output_data {
	get {
		if (this.output_data == null)
				this.output_data = (IIterator<IKVPair<IString,IInteger>>) Services.getPort("output_data");
		return this.output_data;
	}
}

	private IInteger input_key = null;

	public IInteger Input_key {
	get {
		if (this.input_key == null)
				this.input_key = (IInteger) Services.getPort("input_key");
		return this.input_key;
	}
}

	private IString input_value = null;

	public IString Input_value {
	get {
		if (this.input_value == null)
				this.input_value = (IString) Services.getPort("input_value");
		return this.input_value;
	}
}



}

}
