﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Classes;
using Database.Gestions;

namespace Pata_WPF_Commerce.Repositories
{
	public class ClientsRepository
	{
		private static readonly Lazy<ClientsRepository> Lazy = new Lazy<ClientsRepository>(() => new ClientsRepository());

		public static ClientsRepository Instance => Lazy.Value;

		public string Connexion { get; set; }

		private ClientsRepository()
		{
			
		}

		private static GestionClient _gestionClient;

		private GestionClient Controller
		{
			get
			{
				if(_gestionClient == null)
					_gestionClient = new GestionClient(Configuration.Instance.Connexion);

				return _gestionClient;
			}
		}

		public IList<Client> Lire(string index = "id")
		{
			try
			{
				return Controller.Lire(index);
			}
			catch (Exception e)
			{
				throw new Exception("Impossible de lire les locatires : \n" + e.Message);
			}
		}

		public async Task<IList<Client>> LireAsync(string index = "id")
		{
			return await Task.Run(() => Lire(index));
		}

		public Client LireId(int id)
		{
			try
			{
				return Controller.LireId(id);
			}
			catch (Exception e)
			{
				throw new Exception($"Impossible de lire le locataire {id} : \n" + e.Message);
			}
		}

		public async Task<Client> LireIdAsync(int id)
		{
			return await Task.Run(() => LireId(id));
		}

		public int Ajouter(Client client)
		{
			try
			{
				return Controller.Ajouter(client.Nom, client.Prenom, client.Naissance);
			}
			catch (Exception e)
			{
				throw new Exception("Impossible d'ajouter le locataire : \n" + e.Message);
			}
		}

		public async Task<int> AjouterAsync(Client client)
		{
			return await Task.Run(() => Ajouter(client));
		}

		public int Supprimer(int id)
		{
			try
			{
				return Controller.Supprimer(id);
			}
			catch (Exception e)
			{
				throw new Exception($"Impossible de supprimer le locataire {id} : \n" + e.Message);
			}
		}

		public async Task<int> SupprimerAsync(int id)
		{
			return await Task.Run(() => Supprimer(id));
		}
	}
}
