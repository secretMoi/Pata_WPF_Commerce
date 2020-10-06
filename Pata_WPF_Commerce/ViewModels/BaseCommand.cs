using System;
using System.Windows.Input;

namespace Pata_WPF_Commerce.ViewModels
{
	public class BaseCommand : ICommand
	{
		private bool _lParam = false;
		private readonly Action _action;
		private readonly Action<object> _actionObject;

		public event EventHandler CanExecuteChanged;

		public BaseCommand(Action action)
		{
			_action = action;
		}

		public BaseCommand(Action<object> action)
		{
			_actionObject = action;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			if (_lParam)
				_actionObject?.Invoke(parameter);
			else
				_action?.Invoke();
		}
	}
}
