using Database.Classes;

namespace Database.Acces
{
	public class AccesAchat : Base
	{
		public AccesAchat(string sChaineConnexion) : base(sChaineConnexion)
		{
			Table = "DetailAchat";

			_classesBase = new Achat();
		}
	}
}
