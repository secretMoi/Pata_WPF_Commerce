using Database.Classes;

namespace Database.Acces
{
	public class AccesCommandesVente : Base
	{
		public AccesCommandesVente(string sChaineConnexion) : base(sChaineConnexion)
		{
			Table = "CommandesClients";

			_classesBase = new CommandesVente();
		}
	}
}
