using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Classes;
using Database.Gestions;

namespace Pata_WPF_Commerce.Repositories
{
	public class PcRepository
	{
		private static readonly Lazy<PcRepository> Lazy = new Lazy<PcRepository>(() => new PcRepository());

		public static PcRepository Instance => Lazy.Value;

		public string Connexion { get; set; }

		private PcRepository()
		{

		}

		private static Gestion<Pc> _gestion;

		private Gestion<Pc> Gestion
		{
			get
			{
				if (_gestion == null)
					_gestion = new Gestion<Pc>(Configuration.Instance.Connexion);

				return _gestion;
			}
		}

		public IList<Pc> Lire(string index = "id")
		{
			try
			{
				return Gestion.Lire("id");
			}
			catch (Exception e)
			{
				throw new Exception("Impossible de lire les pcs : \n" + e.Message);
			}
		}

		public async Task<IList<Pc>> LireAsync(string index = "id")
		{
			return await Task.Run(() => Lire(index));
		}

		public Pc LireId(int id)
		{
			try
			{
				return Gestion.LireId(id);
			}
			catch (Exception e)
			{
				throw new Exception($"Impossible de lire le pc {id} : \n" + e.Message);
			}
		}

		public async Task<Pc> LireIdAsync(int id)
		{
			return await Task.Run(() => LireId(id));
		}

		public int Ajouter(Pc model)
		{
			try
			{
				return Gestion.Ajouter(
					model.Nom, model.Prix, model.PrixPromo,
					model.Processeur, model.CarteMere, model.Ram,
					model.CarteGraphique, model.DisqueDur1, model.DisqueDur2,
					model.Boitier, model.Alimentation, model.Refroidissement
				);
			}
			catch (Exception e)
			{
				throw new Exception("Impossible d'ajouter le stock : \n" + e.Message);
			}
		}

		public async Task<int> AjouterAsync(Pc model)
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
				throw new Exception($"Impossible de supprimer le pc {id} : \n" + e.Message);
			}
		}

		public async Task<int> SupprimerAsync(int id)
		{
			return await Task.Run(() => Supprimer(id));
		}

		public int Modifier(Pc model)
		{
			try
			{
				return Gestion.Modifier(model.Id, model.Nom, model.Prix, model.PrixPromo,
					model.Processeur, model.CarteMere, model.Ram,
					model.CarteGraphique, model.DisqueDur1, model.DisqueDur2,
					model.Boitier, model.Alimentation, model.Refroidissement
				);
			}
			catch (Exception e)
			{
				throw new Exception($"Impossible de modifier le pc {model.Id} : \n" + e.Message);
			}
		}

		public async Task<int> ModifierAsync(Pc stock)
		{
			return await Task.Run(() => Modifier(stock));
		}
	}
}
