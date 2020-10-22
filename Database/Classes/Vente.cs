using System;
using System.Collections.Generic;
using Database.Acces;

namespace Database.Classes
{
	public class Vente : Base
	{
		public Vente()
		{
		}

		public Vente(int id, int idStock, decimal prix, int quantite, int idCommande)
		{
			Id = id;
			IdStock = idStock;
			Prix = prix;
			Quantite = quantite;
			IdCommande = idCommande;
		}

		public override List<(string, Type)> GetChamps()
		{
			if (_champs.Count == 0)
			{
				_champs.Add(("id", typeof(int)));
				_champs.Add(("id_stock", typeof(int)));
				_champs.Add(("prix", typeof(decimal)));
				_champs.Add(("quantite", typeof(int)));
				_champs.Add(("id_commande", typeof(int)));
			}

			return _champs;
		}

		public override Type GetAcces()
		{
			return typeof(AccesVente);
		}

		public int Id { get; set; }

		public int IdStock { get; set; }

		public int IdCommande { get; set; }

		public int Quantite { get; set; }

		public decimal Prix { get; set; }
	}
}
