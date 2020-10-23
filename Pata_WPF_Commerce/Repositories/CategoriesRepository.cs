using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Classes;
using Database.Gestions;

namespace Pata_WPF_Commerce.Repositories
{
	public class CategoriesRepository
	{
		private static readonly Lazy<CategoriesRepository> Lazy = new Lazy<CategoriesRepository>(() => new CategoriesRepository());

		public static CategoriesRepository Instance => Lazy.Value;

		public string Connexion { get; set; }

		private CategoriesRepository()
		{

		}

		private static Gestion<CategorieComposant> _gestion;

		private Gestion<CategorieComposant> Gestion
		{
			get
			{
				if (_gestion == null)
					_gestion = new Gestion<CategorieComposant>(Configuration.Instance.Connexion);

				return _gestion;
			}
		}

		public IList<CategorieComposant> Lire(string index = "id")
		{
			try
			{
				return Gestion.Lire("id");
			}
			catch (Exception e)
			{
				throw new Exception("Impossible de lire les catégories : \n" + e.Message);
			}
		}

		public async Task<IList<CategorieComposant>> LireAsync(string index = "id")
		{
			return await Task.Run(() => Lire(index));
		}

		public CategorieComposant LireId(int id)
		{
			try
			{
				return Gestion.LireId(id);
			}
			catch (Exception e)
			{
				throw new Exception($"Impossible de lire la catégorie {id} : \n" + e.Message);
			}
		}

		public async Task<CategorieComposant> LireIdAsync(int id)
		{
			return await Task.Run(() => LireId(id));
		}

		public int Ajouter(CategorieComposant model)
		{
			try
			{
				return Gestion.Ajouter(model.Nom);
			}
			catch (Exception e)
			{
				throw new Exception("Impossible d'ajouter la catégorie : \n" + e.Message);
			}
		}

		public async Task<int> AjouterAsync(CategorieComposant model)
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
				throw new Exception($"Impossible de supprimer la catégorie {id} : \n" + e.Message);
			}
		}

		public async Task<int> SupprimerAsync(int id)
		{
			return await Task.Run(() => Supprimer(id));
		}

		public int Modifier(CategorieComposant model)
		{
			try
			{
				return Gestion.Modifier(model.Id, model.Nom);
			}
			catch (Exception e)
			{
				throw new Exception($"Impossible de modifier la catégorie {model.Id} : \n" + e.Message);
			}
		}

		public async Task<int> ModifierAsync(CategorieComposant model)
		{
			return await Task.Run(() => Modifier(model));
		}
	}
}
