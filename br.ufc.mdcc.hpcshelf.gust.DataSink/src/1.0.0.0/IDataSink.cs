using br.ufc.pargo.hpe.kinds;

namespace br.ufc.mdcc.model.Model { 

public interface IModel : BaseIModel
{
		// void loadFrom (IModel o);
		// IModel newInstance ();
		// IModel clone();

		object Instance { get; set;}
		object newInstance ();

} // end main interface 

} // end namespace 
