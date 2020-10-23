using System;
using System.Collections.Generic;
using Database.Acces;

namespace Database.Classes
{
	public class Pc : Base
	{
		public Pc()
		{
		}

		public Pc(
			int id, string nom, decimal prix, decimal prixPromo, 
			int processeur, int carteMere, int ram,
			int carteGraphique, int disqueDur1, int disqueDur2,
			int boitier, int alimentation, int refroidissement
			)
		{
			Id = id;
			Nom = nom;
			Prix = prix;
			PrixPromo = prixPromo;
			Processeur = processeur;
			CarteMere = carteMere;
			Ram = ram;
			CarteGraphique = carteGraphique;
			DisqueDur1 = disqueDur1;
			DisqueDur2 = disqueDur2;
			Boitier = boitier;
			Alimentation = alimentation;
			Refroidissement = refroidissement;
		}

		public override List<(string, Type)> GetChamps()
		{
			if (_champs.Count == 0)
			{
				_champs.Add(("id", typeof(int)));
				_champs.Add(("nom", typeof(string)));
				_champs.Add(("prix", typeof(decimal)));
				_champs.Add(("prixPromo", typeof(decimal)));
				_champs.Add(("processeur", typeof(int)));
				_champs.Add(("carteMere", typeof(int)));
				_champs.Add(("ram", typeof(int)));
				_champs.Add(("carteGraphique", typeof(int)));
				_champs.Add(("disqueDur1", typeof(int)));
				_champs.Add(("disqueDur2", typeof(int)));
				_champs.Add(("boitier", typeof(int)));
				_champs.Add(("alimentation", typeof(int)));
				_champs.Add(("refroidissement", typeof(int)));
			}

			return _champs;
		}

		public override Type GetAcces()
		{
			return typeof(AccesPc);
		}

		public int Id { get; set; }

		public string Nom { get; set; }

		public decimal Prix { get; set; }
		public decimal PrixPromo { get; set; }

		public int Processeur { get; set; }

		public int CarteMere { get; set; }
		public int Ram { get; set; }
		public int CarteGraphique { get; set; }
		public int DisqueDur1 { get; set; }
		public int DisqueDur2 { get; set; }
		public int Boitier { get; set; }
		public int Alimentation { get; set; }
		public int Refroidissement { get; set; }
	}
}
