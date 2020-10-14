using System;
using System.Collections.Generic;

namespace Database.Gestions
{
	public class Gestion<T> where T : Classes.Base, new()
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

		private Acces.Base GetInstance()
		{
			return (dynamic)Activator.CreateInstance(new T().GetAcces(), ChaineConnexion);
		}

		public bool TestConnection()
		{
			return new Acces.Base(ChaineConnexion).TestConnection();
		}

		public List<T> Lire(string index)
		{
			return GetInstance().Lire(index).ConvertAll(x => (T)x);
		}

		public T LireId(int id)
		{
			return GetInstance().LireId(id) as T;
		}

		public int Ajouter(params object[] arguments)
		{
			return GetInstance().Ajouter(arguments);
		}

		public int Modifier(params object[] arguments)
		{
			return GetInstance().Modifier(arguments);
		}

		public int Supprimer(int id)
		{
			return GetInstance().Supprimer(id);
		}
	}
}
