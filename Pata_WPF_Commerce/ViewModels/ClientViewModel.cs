using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;
using Database.Classes;
using Pata_WPF_Commerce.Repositories;
using Pata_WPF_Commerce.ViewModels.DataBinding;

namespace Pata_WPF_Commerce.ViewModels
{
	public class ClientViewModel : BaseProperty
	{
		private DataClient _clientMap; // données bindée dans le formulaire
		private Client _selectedClient; // données du client sélectionné dans la dgv

		public DataClient ClientMap // données bindée dans le formulaire
		{
			get => _clientMap;
			set => AssignField(ref _clientMap, value, MethodBase.GetCurrentMethod().Name);
		}

		public Client SelectedClient // données du client sélectionné dans la dgv
		{
			get => _selectedClient;
			set => AssignField(ref _selectedClient, value, MethodBase.GetCurrentMethod().Name);
		}

		public ObservableCollection<Client> Clients { get; set; } // données bindée dans la dgv du client sélectionné

		public ClientViewModel()
		{
			Clients = LoadClients(); // récupère les clients dans la bdd

			ClientMap = Map(Clients.FirstOrDefault(), ClientMap); // injecte le premier client trouvé

			// bind les commandes au xaml
			CommandAdd = new BaseCommand(Add);
			CommandDelete = new BaseCommand(Delete);
			CommandModify = new BaseCommand(Modify);
		}

		/**
		 * <summary>Charge les clients depuis la bdd vers la dgv</summary>
		 * <returns>Une liste des clients bindable</returns>
		 */
		private ObservableCollection<Client> LoadClients()
		{
			ObservableCollection<Client> listClients = new ObservableCollection<Client>();
			IList<Client> tempClients = ClientsRepository.Instance.Lire();

			foreach (var client in tempClients)
				listClients.Add(client);

			return listClients;
		}

		/**
		 * <summary>Génère la liste complète des clients</summary>
		 * <returns>Un document formatté contenant la liste des clients</returns>
		 */
		public FlowDocument FullClientsList()
		{
			FlowDocument flowDocument = new FlowDocument();
			Paragraph paragraph = new Paragraph();

			paragraph.Inlines.Add(new Bold(new Run("Liste des clients encodés")));
			flowDocument.Blocks.Add(paragraph);

			List list = new List();

			foreach (Client client in Clients)
			{
				paragraph = new Paragraph(new Run(client.Prenom + " " + client.Nom
				                                  + " (" + client.Naissance.ToShortDateString() + ")"));

				list.ListItems.Add(new ListItem(paragraph));
			}

			flowDocument.Blocks.Add(list);

			return flowDocument;
		}

		/**
		 * <summary>Fais correspondre le contenu de l'élément sélectionné de la dgv dans le formulaire</summary>
		 */
		public void ChangedSelectedClient()
		{
			ClientMap = Map(SelectedClient, ClientMap);
		}

		/**
		 * <summary>Ajoute un élément à la dgv et la bdd</summary>
		 */
		private async void Add()
		{
			// ajoute le nouveau client à la bdd
			Client client = new Client();
			client = Map(ClientMap, client);
			int res = await ClientsRepository.Instance.AjouterAsync(client);

			// ajoute ce nouveau client à la dgv
			client = await ClientsRepository.Instance.LireIdAsync(res);
			Clients.Add(client);
		}

		/**
		 * <summary>Supprime un élément de la dgv et la bdd</summary>
		 */
		private async void Delete()
		{
			if(SelectedClient == null) return; // si aucun n'est sélectionné on quitte

			// confirmation de suppression
			var result = MessageBox.Show(
				$"Voulez-vous vraiment supprimer le client {SelectedClient.Nom} {SelectedClient.Prenom} ?",
				"Suppression", MessageBoxButton.YesNo
			);

			if(result == MessageBoxResult.No) return;

			// supprime le client de la bdd
			await ClientsRepository.Instance.SupprimerAsync(SelectedClient.Id);

			// supprime le client de la dgv
			Clients.Remove(SelectedClient);
		}

		/**
		 * <summary>Modifie un élément de la dgv et la bdd</summary>
		 */
		private async void Modify()
		{
			// map le client entre le formulaire et le model
			Client client = new Client();
			client = Map(ClientMap, client);

			await ClientsRepository.Instance.ModifierAsync(client); // ajoute à la bdd

			// rafraichit la dgv
			var element = Clients.First(item => item.Id == client.Id);
			int index = Clients.IndexOf(element);
			Clients[index] = client;
		}

		#region Commands
		public BaseCommand CommandAdd { get; set; }
		public BaseCommand CommandDelete { get; set; }
		public BaseCommand CommandModify { get; set; }
		#endregion
	}
}
