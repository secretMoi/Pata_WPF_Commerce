using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Classes;
using Database.Gestions;

namespace Pata_WPF_Commerce.Repositories
{
	public class DetailAchatsRepository
	{
		private static readonly Lazy<DetailAchatsRepository> Lazy = new Lazy<DetailAchatsRepository>(() => new DetailAchatsRepository());

		public static DetailAchatsRepository Instance => Lazy.Value;

		public string Connexion { get; set; }

		private DetailAchatsRepository()
		{

		}

		private static Gestion<Achat> _gestion;

		private Gestion<Achat> Gestion
		{
			get
			{
				if (_gestion == null)
					_gestion = new Gestion<Achat>(Configuration.Instance.Connexion);

				return _gestion;
			}
		}

		public IList<Achat> Lire(string index = "id")
		{
			try
			{
				return Gestion.Lire("id");
			}
			catch (Exception e)
			{
				throw new Exception("Impossible de lire les achats : \n" + e.Message);
			}
		}

		public async Task<IList<Achat>> LireAsync(string index = "id")
		{
			return await Task.Run(() => Lire(index));
		}

		public Achat LireId(int id)
		{
			try
			{
				return Gestion.LireId(id);
			}
			catch (Exception e)
			{
				throw new Exception($"Impossible de lire l'achat {id} : \n" + e.Message);
			}
		}

		public async Task<Achat> LireIdAsync(int id)
		{
			return await Task.Run(() => LireId(id));
		}

		public int Ajouter(Achat model)
		{
			try
			{
				return Gestion.Ajouter(model.IdStock, model.Prix, model.Quantite, model.IdCommande);
			}
			catch (Exception e)
			{
				throw new Exception("Impossible d'ajouter l'achat : \n" + e.Message);
			}
		}

		public async Task<int> AjouterAsync(Achat model)
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
				throw new Exception($"Impossible de supprimer l'achat {id} : \n" + e.Message);
			}
		}

		public async Task<int> SupprimerAsync(int id)
		{
			return await Task.Run(() => Supprimer(id));
		}

		public int Modifier(Achat model)
		{
			try
			{
				return Gestion.Modifier(model.Id, model.IdStock, model.Prix, model.Quantite, model.IdCommande);
			}
			catch (Exception e)
			{
				throw new Exception($"Impossible de modifier l'achat {model.Id} : \n" + e.Message);
			}
		}

		public async Task<int> ModifierAsync(Achat Achat)
		{
			return await Task.Run(() => Modifier(Achat));
		}
	}
}
