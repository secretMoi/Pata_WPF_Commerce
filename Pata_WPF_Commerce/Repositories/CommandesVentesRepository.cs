using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Classes;
using Database.Gestions;

namespace Pata_WPF_Commerce.Repositories
{
	public class CommandesVentesRepository
	{
		private static readonly Lazy<CommandesVentesRepository> Lazy = new Lazy<CommandesVentesRepository>(() => new CommandesVentesRepository());

		public static CommandesVentesRepository Instance => Lazy.Value;

		public string Connexion { get; set; }

		private CommandesVentesRepository()
		{

		}

		private static Gestion<CommandesVente> _gestion;

		private Gestion<CommandesVente> Gestion
		{
			get
			{
				if (_gestion == null)
					_gestion = new Gestion<CommandesVente>(Configuration.Instance.Connexion);

				return _gestion;
			}
		}

		public IList<CommandesVente> Lire(string index = "id")
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

		public async Task<IList<CommandesVente>> LireAsync(string index = "id")
		{
			return await Task.Run(() => Lire(index));
		}

		public CommandesVente LireId(int id)
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

		public async Task<CommandesVente> LireIdAsync(int id)
		{
			return await Task.Run(() => LireId(id));
		}

		public int Ajouter(CommandesVente model)
		{
			try
			{
				return Gestion.Ajouter(model.IdClient, model.Date);
			}
			catch (Exception e)
			{
				throw new Exception("Impossible d'ajouter la vente : \n" + e.Message);
			}
		}

		public async Task<int> AjouterAsync(CommandesVente model)
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

		public int Modifier(CommandesVente model)
		{
			try
			{
				return Gestion.Modifier(model.Id, model.IdClient, model.Date);
			}
			catch (Exception e)
			{
				throw new Exception($"Impossible de modifier la commande {model.Id} : \n" + e.Message);
			}
		}

		public async Task<int> ModifierAsync(CommandesVente model)
		{
			return await Task.Run(() => Modifier(model));
		}
	}
}
