using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Classes;
using Database.Gestions;

namespace Pata_WPF_Commerce.Repositories
{
	public class DetailVentesRepository
	{
		private static readonly Lazy<DetailVentesRepository> Lazy = new Lazy<DetailVentesRepository>(() => new DetailVentesRepository());

		public static DetailVentesRepository Instance => Lazy.Value;

		public string Connexion { get; set; }

		private DetailVentesRepository()
		{

		}

		private static Gestion<Vente> _gestion;

		private Gestion<Vente> Gestion
		{
			get
			{
				if (_gestion == null)
					_gestion = new Gestion<Vente>(Configuration.Instance.Connexion);

				return _gestion;
			}
		}

		public IList<Vente> Lire(string index = "id")
		{
			try
			{
				return Gestion.Lire("id");
			}
			catch (Exception e)
			{
				throw new Exception("Impossible de lire les ventes : \n" + e.Message);
			}
		}

		public async Task<IList<Vente>> LireAsync(string index = "id")
		{
			return await Task.Run(() => Lire(index));
		}

		public Vente LireId(int id)
		{
			try
			{
				return Gestion.LireId(id);
			}
			catch (Exception e)
			{
				throw new Exception($"Impossible de lire la vente {id} : \n" + e.Message);
			}
		}

		public async Task<Vente> LireIdAsync(int id)
		{
			return await Task.Run(() => LireId(id));
		}

		public int Ajouter(Vente model)
		{
			try
			{
				return Gestion.Ajouter(model.IdStock, model.Prix, model.Quantite, model.IdCommande);
			}
			catch (Exception e)
			{
				throw new Exception("Impossible d'ajouter la vente : \n" + e.Message);
			}
		}

		public async Task<int> AjouterAsync(Vente model)
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
				throw new Exception($"Impossible de supprimer la vente {id} : \n" + e.Message);
			}
		}

		public async Task<int> SupprimerAsync(int id)
		{
			return await Task.Run(() => Supprimer(id));
		}

		public int Modifier(Vente model)
		{
			try
			{
				return Gestion.Modifier(model.Id, model.IdStock, model.Prix, model.Quantite, model.IdCommande);
			}
			catch (Exception e)
			{
				throw new Exception($"Impossible de modifier la vente {model.Id} : \n" + e.Message);
			}
		}

		public async Task<int> ModifierAsync(Vente model)
		{
			return await Task.Run(() => Modifier(model));
		}
	}
}
