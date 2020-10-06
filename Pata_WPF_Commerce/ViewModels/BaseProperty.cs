using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
	}
}
