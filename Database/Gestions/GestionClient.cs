using System;
using System.Collections.Generic;
using Database.Acces;
using Database.Classes;

namespace Database.Gestions
{
	public class GestionClient : Base
	{
			public GestionClient()
			{
			}

			public GestionClient(string sChaineConnexion) : base(sChaineConnexion)
			{
			}

			public int Ajouter(string nom, string prenom, DateTime naissance)
			{
				return new AccesClients(ChaineConnexion).Ajouter(nom, prenom, naissance);
			}

			public int Modifier(int id, string nom, byte[] password)
			{
				return new AccesClients(ChaineConnexion).Modifier(id, nom, password);
			}

			public List<Client> Lire(string index)
			{
				return new AccesClients(ChaineConnexion).Lire(index).ConvertAll(x => (Client)x);
			}

			public Client LireId(int id)
			{
				return new AccesClients(ChaineConnexion).LireId(id) as Client;
			}

			public new int Supprimer(int id)
			{
				return new AccesClients(ChaineConnexion).Supprimer(id);
			}
	}
}
