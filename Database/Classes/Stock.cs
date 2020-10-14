﻿using System;
using System.Collections.Generic;

namespace Database.Classes
{
	public class Stock : Base
	{
		public Stock()
		{
		}

		public Stock(int id, string nom, int quantiteActuelle, int quantiteMin, decimal prixAchat, decimal prixVente)
		{
			Id = id;
			Nom = nom;
			QuantiteActuelle = quantiteActuelle;
			QuantiteMin = quantiteMin;
			PrixAchat = prixAchat;
			PrixVente = prixVente;
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
			}

			return _champs;
		}

		public int Id { get; set; }

		public string Nom { get; set; }

		public int QuantiteActuelle { get; set; }

		public int QuantiteMin { get; set; }

		public decimal PrixAchat { get; set; }

		public decimal PrixVente { get; set; }
	}
}