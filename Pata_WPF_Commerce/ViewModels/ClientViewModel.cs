using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Database.Classes;
using Pata_WPF_Commerce.Repositories;
using Pata_WPF_Commerce.ViewModels.DataBinding;

namespace Pata_WPF_Commerce.ViewModels
{
	public class ClientViewModel : BaseProperty
	{
		private DataClient _clientMap;

		public ClientViewModel()
		{
			Clients = LoadClients(); // récupère les clients dans la bdd

			ClientMap = Map(Clients.FirstOrDefault(), ClientMap); // injecte le premier client trouvé

			// bind les commandes au xaml
			CommandeConfirm = new BaseCommand(Confirm);
			CommandeCancel = new BaseCommand(Cancel);
			CommandAdd = new BaseCommand(Add);
			CommandDelete = new BaseCommand(Delete);
		}

		private ObservableCollection<Client> LoadClients()
		{
			ObservableCollection<Client> listClients = new ObservableCollection<Client>();
			IList<Client> tempClients = ClientsRepository.Instance.Lire();

			foreach (var client in tempClients)
			{
				listClients.Add(client);	
			}

			return listClients;
		}

		public DataClient ClientMap
		{
			get => _clientMap;
			set => AssignField(ref _clientMap, value, MethodBase.GetCurrentMethod().Name);
		}

		public ObservableCollection<Client> Clients { get; set; }

		public void Confirm()
		{
			ClientMap.Nom = "coucou";
		}

		public void Cancel()
		{
			ClientMap.Prenom = "test";
		}

		private async void Add()
		{
			Client client = new Client()
			{
				Nom = "testi",
				Prenom = "trtr",
				Naissance = DateTime.Now
			};

			int res = await ClientsRepository.Instance.AjouterAsync(client);

			client = await ClientsRepository.Instance.LireIdAsync(res);
			Clients.Add(client);
		}

		private async void Delete()
		{
			int id = 7;

			await ClientsRepository.Instance.SupprimerAsync(id);

			Client clientToRemove = Clients.FirstOrDefault(client => client.Id == id);
			Clients.Remove(clientToRemove);
		}

		#region Commands
		public BaseCommand CommandeConfirm { get; set; }
		public BaseCommand CommandeCancel { get; set; }
		public BaseCommand CommandAdd { get; set; }
		public BaseCommand CommandDelete { get; set; }
		#endregion
	}
}
