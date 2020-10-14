using System;
using System.Collections.Generic;

namespace Database.Gestions
{
	public class Gestion<T, TU> where TU : Acces.Base where T : Classes.Base
	{
		public string ChaineConnexion { get; set; }

		public Gestion()
		{
			ChaineConnexion = "";
		}

		public Gestion(string sChaineConnexion)
		{
			ChaineConnexion = sChaineConnexion;
		}

		private TU GetInstance()
		{
			return (TU) Activator.CreateInstance(typeof(TU), ChaineConnexion);
		}

		public bool TestConnection()
		{
			return new Acces.Base(ChaineConnexion).TestConnection();
		}

		public List<T> Lire(string index)
		{
			return GetInstance().Lire(index).ConvertAll(x => (T)x);
		}
	}
}
