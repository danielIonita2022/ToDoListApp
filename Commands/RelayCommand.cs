using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ToDoListApp.Commands
{
	internal class RelayCommand : ICommand
	{
		private Action<object> execute;

		public event EventHandler CanExecuteChanged
		{
			add
			{
				//+=asociaza un handler la un eveniment
				CommandManager.RequerySuggested += value;
			}

			remove
			{
				//-=sterge un handler de la un eveniment
				CommandManager.RequerySuggested -= value;
			}
		}

		public RelayCommand(Action<object> execute)
		{
			this.execute = execute;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			execute(parameter);
		}
	}
}
