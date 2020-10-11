using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AutoMapper;
using Pata_WPF_Commerce.Annotations;

namespace Pata_WPF_Commerce.ViewModels
{
	public class BaseProperty : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool AssignField<T>(ref T field, T value, string propertyName)
		{
			if (EqualityComparer<T>.Default.Equals(field, value))
				return false;

			PropertyChangedEventHandler handler = PropertyChanged;
			field = value;

			handler?.Invoke(this, new PropertyChangedEventArgs(propertyName.Substring(4)));

			return true;
		}

		/**
		 * <summary>Map une source vers une destination</summary>
		 * <param name="source">Objet source</param>
		 * <param name="destination">Objet destination (c'est le type qui importe)</param>
		 * <returns>Retourne un nouvel objet du type de la destination avec les valeurs de la source</returns>
		 */
		public static TU Map<T, TU>(T source, TU destination)
		{
			// configure le mapper
			var config = new MapperConfiguration(cfg => cfg.CreateMap<T, TU>());

			var mapper = config.CreateMapper(); // crée le mapper
			return mapper.Map<TU>(source); // map et retourne le résultat
		}
	}
}
