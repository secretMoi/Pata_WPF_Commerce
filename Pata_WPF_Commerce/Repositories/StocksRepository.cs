﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Classes;
using Database.Gestions;

namespace Pata_WPF_Commerce.Repositories
{
	public class StocksRepository
	{
		private static readonly Lazy<StocksRepository> Lazy = new Lazy<StocksRepository>(() => new StocksRepository());

		public static StocksRepository Instance => Lazy.Value;

		public string Connexion { get; set; }

		private StocksRepository()
		{

		}

		private static Gestion<Stock> _gestion;

		private Gestion<Stock> Gestion
		{
			get
			{
				if (_gestion == null)
					_gestion = new Gestion<Stock>(Configuration.Instance.Connexion);

				return _gestion;
			}
		}

		public IList<Stock> Lire(string index = "id")
		{
			try
			{
				return Gestion.Lire("id");
			}
			catch (Exception e)
			{
				throw new Exception("Impossible de lire les stocks : \n" + e.Message);
			}
		}

		public async Task<IList<Stock>> LireAsync(string index = "id")
		{
			return await Task.Run(() => Lire(index));
		}

		public Stock LireId(int id)
		{
			try
			{
				return Gestion.LireId(id);
			}
			catch (Exception e)
			{
				throw new Exception($"Impossible de lire le stock {id} : \n" + e.Message);
			}
		}

		public async Task<Stock> LireIdAsync(int id)
		{
			return await Task.Run(() => LireId(id));
		}

		public int Ajouter(Stock model)
		{
			try
			{
				return Gestion.Ajouter(model.Nom, model.QuantiteActuelle, model.QuantiteMin, model.PrixAchat, model.PrixVente, model.Categorie);
			}
			catch (Exception e)
			{
				throw new Exception("Impossible d'ajouter le stock : \n" + e.Message);
			}
		}

		public async Task<int> AjouterAsync(Stock model)
		{
			return await Task.Run(() => Ajouter(model));
		}

		public int Supprimer(int id)
		{
			try
			{
				return Gestion.Supprimer(id);
			}
			catch (Exception e)
			{
				throw new Exception($"Impossible de supprimer le stock {id} : \n" + e.Message);
			}
		}

		public async Task<int> SupprimerAsync(int id)
		{
			return await Task.Run(() => Supprimer(id));
		}

		public int Modifier(Stock model)
		{
			try
			{
				return Gestion.Modifier(model.Id, model.Nom, model.QuantiteActuelle, model.QuantiteMin, model.PrixAchat, model.PrixVente, model.Categorie);
			}
			catch (Exception e)
			{
				throw new Exception($"Impossible de modifier le stock {model.Id} : \n" + e.Message);
			}
		}

		public async Task<int> ModifierAsync(Stock stock)
		{
			return await Task.Run(() => Modifier(stock));
		}
	}
}
