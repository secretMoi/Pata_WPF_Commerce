using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Classes
{
	public abstract class Base
	{
		protected readonly List<(string, Type)> _champs = new List<(string, Type)>();

		public abstract List<(string, Type)> GetChamps(); // liste des champs de la classe
	}
}
