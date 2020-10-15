using Database.Classes;

namespace Database.Acces
{
	public class AccesCommandesAchat : Base
	{
		public AccesCommandesAchat(string sChaineConnexion) : base(sChaineConnexion)
		{
			Table = "CommandesFournisseurs";

			_classesBase = new CommandesAchat();
		}
	}
}
