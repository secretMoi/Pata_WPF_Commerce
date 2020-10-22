using Database.Classes;

namespace Database.Acces
{
	public class AccesVente : Base
	{
		public AccesVente(string sChaineConnexion) : base(sChaineConnexion)
		{
			Table = "DetailVente";

			_classesBase = new Vente();
		}
	}
}
