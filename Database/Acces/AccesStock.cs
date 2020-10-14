using Database.Classes;

namespace Database.Acces
{
	public class AccesStock : Base
	{
		public AccesStock(string sChaineConnexion) : base(sChaineConnexion)
		{
			Table = "Stock";

			_classesBase = new Stock();
		}
	}
}
