using br.ufc.pargo.hpe.kinds;

namespace br.ufc.mdcc.common.Data { 

public interface IData : BaseIData
{
		// void loadFrom (IData o);
		// IData newInstance ();
		// IData clone();

		object Instance { get; set;}
		object newInstance ();

} // end main interface 

	public interface IDataInstance
	{
		object ObjValue { set; get; }
	}

} // end namespace 
