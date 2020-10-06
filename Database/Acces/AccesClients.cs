using Database.Classes;

namespace Database.Acces
{
	public class AccesClients : Base
	{
		public AccesClients(string sChaineConnexion) : base(sChaineConnexion)
		{
			Table = "Clients";

			_classesBase = new Client();
		}
	}
}
