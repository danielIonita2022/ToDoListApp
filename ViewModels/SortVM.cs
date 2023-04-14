using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToDoListApp.Commands;

namespace ToDoListApp.ViewModels
{
	internal class SortVM
	{
		public bool IsDeadlineChecked { get; set; }
		public bool IsPriorityChecked { get; set; }

		private ICommand confirmCommand;
		public ICommand ConfirmCommand
		{
			get
			{
				if (confirmCommand == null)
				{
					confirmCommand = new RelayCommand(Confirm);
				}
				return confirmCommand;
			}
		}

		private void Confirm(object param)
		{
			string sortedBy = null;
			if (IsDeadlineChecked)
			{
				sortedBy = "deadline";
			}
			else if (IsPriorityChecked)
			{
				sortedBy = "priority";
			}

			Messenger.Default.Send(sortedBy);

			if(param is Window window)
			{
				window.Close();			
			}
		}
	}
}
