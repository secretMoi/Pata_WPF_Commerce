using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Classes;
using Database.Gestions;

namespace Pata_WPF_Commerce.Repositories
{
	public class CommandesAchatsRepository
	{
		private static readonly Lazy<CommandesAchatsRepository> Lazy = new Lazy<CommandesAchatsRepository>(() => new CommandesAchatsRepository());

		public static CommandesAchatsRepository Instance => Lazy.Value;

		public string Connexion { get; set; }

		private CommandesAchatsRepository()
		{

		}

		private static Gestion<CommandesAchat> _gestion;

		private Gestion<CommandesAchat> Gestion
		{
			get
			{
				if (_gestion == null)
					_gestion = new Gestion<CommandesAchat>(Configuration.Instance.Connexion);

				return _gestion;
			}
		}

		public IList<CommandesAchat> Lire(string index = "id")
		{
			try
			{
				return Gestion.Lire("id");
			}
			catch (Exception e)
			{
				throw new Exception("Impossible de lire les commandes : \n" + e.Message);
			}
		}

		public async Task<IList<CommandesAchat>> LireAsync(string index = "id")
		{
			return await Task.Run(() => Lire(index));
		}

		public CommandesAchat LireId(int id)
		{
			try
			{
				return Gestion.LireId(id);
			}
			catch (Exception e)
			{
				throw new Exception($"Impossible de lire la commande {id} : \n" + e.Message);
			}
		}

		public async Task<CommandesAchat> LireIdAsync(int id)
		{
			return await Task.Run(() => LireId(id));
		}

		public int Ajouter(CommandesAchat model)
		{
			try
			{
				return Gestion.Ajouter(model.IdFournisseur, model.Date);
			}
			catch (Exception e)
			{
				throw new Exception("Impossible d'ajouter la commande : \n" + e.Message);
			}
		}

		public async Task<int> AjouterAsync(CommandesAchat model)
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
				throw new Exception($"Impossible de supprimer la commande {id} : \n" + e.Message);
			}
		}

		public async Task<int> SupprimerAsync(int id)
		{
			return await Task.Run(() => Supprimer(id));
		}

		public int Modifier(CommandesAchat model)
		{
			try
			{
				return Gestion.Modifier(model.Id, model.IdFournisseur, model.Date);
			}
			catch (Exception e)
			{
				throw new Exception($"Impossible de modifier la commande {model.Id} : \n" + e.Message);
			}
		}

		public async Task<int> ModifierAsync(CommandesAchat model)
		{
			return await Task.Run(() => Modifier(model));
		}
	}
}
