using System.Collections.Generic;
using Database.Acces;
using Database.Classes;

namespace Database.Gestions
{
	public class GestionStock : Base
	{
		public GestionStock()
		{
		}

		public GestionStock(string sChaineConnexion) : base(sChaineConnexion)
		{
		}

		public int Ajouter(string nom, int quantiteActuelle, int quantiteMin, decimal prixAchat, decimal prixVente)
		{
			return new AccesStock(ChaineConnexion).Ajouter(nom, quantiteActuelle, quantiteMin, prixAchat, prixVente);
		}

		public int Modifier(int id, string nom, int quantiteActuelle, int quantiteMin, decimal prixAchat, decimal prixVente)
		{
			return new AccesStock(ChaineConnexion).Modifier(id, nom, quantiteActuelle, quantiteMin, prixAchat, prixVente);
		}

		public List<Stock> Lire(string index)
		{
			return new AccesStock(ChaineConnexion).Lire(index).ConvertAll(x => (Stock)x);
		}

		public Stock LireId(int id)
		{
			return new AccesStock(ChaineConnexion).LireId(id) as Stock;
		}

		public int Supprimer(int id)
		{
			return new AccesStock(ChaineConnexion).Supprimer(id);
		}
	}
}
