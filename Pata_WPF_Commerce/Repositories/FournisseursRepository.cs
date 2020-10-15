using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Classes;
using Database.Gestions;
//todo generic
namespace Pata_WPF_Commerce.Repositories
{
	public class FournisseursRepository
	{
		private static readonly Lazy<FournisseursRepository> Lazy = new Lazy<FournisseursRepository>(() => new FournisseursRepository());

		public static FournisseursRepository Instance => Lazy.Value;

		private FournisseursRepository()
		{

		}

		private static Gestion<Fournisseur> _gestion;

		private Gestion<Fournisseur> Gestion
		{
			get
			{
				if (_gestion == null)
					_gestion = new Gestion<Fournisseur>(Configuration.Instance.Connexion);

				return _gestion;
			}
		}

		public IList<Fournisseur> Lire(string index = "id")
		{
			try
			{
				return Gestion.Lire("id");
			}
			catch (Exception e)
			{
				throw new Exception("Impossible de lire les fournisseurs : \n" + e.Message);
			}
		}

		public async Task<IList<Fournisseur>> LireAsync(string index = "id")
		{
			return await Task.Run(() => Lire(index));
		}

		public Fournisseur LireId(int id)
		{
			try
			{
				return Gestion.LireId(id);
			}
			catch (Exception e)
			{
				throw new Exception($"Impossible de lire le fournisseur {id} : \n" + e.Message);
			}
		}

		public async Task<Fournisseur> LireIdAsync(int id)
		{
			return await Task.Run(() => LireId(id));
		}

		public int Ajouter(Fournisseur model)
		{
			try
			{
				return Gestion.Ajouter(model.Nom);
			}
			catch (Exception e)
			{
				throw new Exception("Impossible d'ajouter le fournisseur : \n" + e.Message);
			}
		}

		public async Task<int> AjouterAsync(Fournisseur model)
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
				throw new Exception($"Impossible de supprimer le fournisseur {id} : \n" + e.Message);
			}
		}

		public async Task<int> SupprimerAsync(int id)
		{
			return await Task.Run(() => Supprimer(id));
		}

		public int Modifier(Fournisseur model)
		{
			try
			{
				return Gestion.Modifier(model.Id, model.Nom);
			}
			catch (Exception e)
			{
				throw new Exception($"Impossible de modifier le fournisseur {model.Id} : \n" + e.Message);
			}
		}

		public async Task<int> ModifierAsync(Fournisseur model)
		{
			return await Task.Run(() => Modifier(model));
		}
	}
}
