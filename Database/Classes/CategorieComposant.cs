using System;
using System.Collections.Generic;
using Database.Acces;

namespace Database.Classes
{
	public class CategorieComposant : Base
	{
		public CategorieComposant()
		{
		}

		public CategorieComposant(int id, string nom)
		{
			Id = id;
			Nom = nom;
		}

		public override List<(string, Type)> GetChamps()
		{
			if (_champs.Count == 0)
			{
				_champs.Add(("Id", typeof(int)));
				_champs.Add(("nom", typeof(string)));
			}

			return _champs;
		}

		public override Type GetAcces()
		{
			return typeof(AccesCategorieComposant);
		}

		public int Id { get; set; }

		public string Nom { get; set; }
	}
}
