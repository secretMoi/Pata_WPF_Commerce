using Database.Classes;

namespace Database.Acces
{
	public class AccesFournisseur : Base
	{
		public AccesFournisseur(string sChaineConnexion) : base(sChaineConnexion)
		{
			Table = "Fournisseurs";

			_classesBase = new Fournisseur();
		}
	}
}
