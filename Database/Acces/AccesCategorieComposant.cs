using Database.Classes;

namespace Database.Acces
{
	public class AccesCategorieComposant : Base
	{
		public AccesCategorieComposant(string sChaineConnexion) : base(sChaineConnexion)
		{
			Table = "CategorieComposant";

			_classesBase = new CategorieComposant();
		}
	}
}
