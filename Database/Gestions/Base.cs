namespace Database.Gestions
{
	public class Base
	{
		#region Constructeurs
		public Base()
		{
			ChaineConnexion = "";
		}

		public Base(string sChaineConnexion)
		{
			ChaineConnexion = sChaineConnexion;
		}
		#endregion

		public bool TestConnection()
		{
			return new Acces.Base(ChaineConnexion).TestConnection();
		}

		public virtual void Supprimer(int id)
		{
			new Acces.Base(ChaineConnexion).Supprimer(id);
		}

		#region Accesseur
		public string ChaineConnexion { get; set; }

		#endregion
	}
}
