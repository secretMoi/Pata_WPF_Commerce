using System;
using System.Collections.Generic;
using Database.Acces;

namespace Database.Classes
{
	public class Stock : Base
	{
		public Stock()
		{
		}

		public Stock(int id, string nom, int quantiteActuelle, int quantiteMin, decimal prixAchat, decimal prixVente, int categorie)
		{
			Id = id;
			Nom = nom;
			QuantiteActuelle = quantiteActuelle;
			QuantiteMin = quantiteMin;
			PrixAchat = prixAchat;
			PrixVente = prixVente;
			Categorie = categorie;
		}

		public override List<(string, Type)> GetChamps()
		{
			if (_champs.Count == 0)
			{
				_champs.Add(("id", typeof(int)));
				_champs.Add(("nom", typeof(string)));
				_champs.Add(("quantiteActuelle", typeof(int)));
				_champs.Add(("quantiteMin", typeof(int)));
				_champs.Add(("prix_achat", typeof(decimal)));
				_champs.Add(("prix_vente", typeof(decimal)));
				_champs.Add(("categorie", typeof(int)));
			}

			return _champs;
		}

		public override Type GetAcces()
		{
			return typeof(AccesStock);
		}

		public int Id { get; set; }

		public string Nom { get; set; }

		public int QuantiteActuelle { get; set; }

		public int QuantiteMin { get; set; }

		public decimal PrixAchat { get; set; }

		public decimal PrixVente { get; set; }

		public int Categorie { get; set; }
	}
}
