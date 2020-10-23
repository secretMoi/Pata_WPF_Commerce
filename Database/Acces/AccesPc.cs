using Database.Classes;

namespace Database.Acces
{
	public class AccesPc : Base
	{
		public AccesPc(string sChaineConnexion) : base(sChaineConnexion)
		{
			Table = "Pc";

			_classesBase = new Pc();
		}
	}
}
