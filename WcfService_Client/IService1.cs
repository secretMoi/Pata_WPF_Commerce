using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using Database.Classes;

namespace WcfService_Client
{
	// REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
	[ServiceContract]
	public interface IService1
	{

		[OperationContract]
		string GetData(int value);

		[OperationContract]
		CompositeType GetDataUsingDataContract(CompositeType composite);

		[OperationContract]
		List<Stock> GetVentesFromCommande(int id);

		// TODO: ajoutez vos opérations de service ici
	}


	// Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
	[DataContract]
	public class CompositeType
	{
		[DataMember]
		public bool BoolValue { get; set; } = true;

		[DataMember]
		public string StringValue { get; set; } = "Hello ";
	}
}
