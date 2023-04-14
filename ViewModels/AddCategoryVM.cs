using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToDoListApp.Commands;

namespace ToDoListApp.ViewModels
{
	internal class AddCategoryVM : INotifyPropertyChanged
	{
		private string name;
		public string Name
		{
			get { return name; } 
			set 
			{
				name = value;
				NotifyPropertyChanged(nameof(Name));
			}
		}

		private ICommand confirmActionCommand;
		public ICommand ConfirmActionCommand
		{
			get
			{
				if (confirmActionCommand == null)
				{
					confirmActionCommand = new RelayCommand(ConfirmAction);
				}
				return confirmActionCommand;
			}
		}

		public void ConfirmAction(object param)
		{
			Messenger.Default.Send(Name);
			if(param is Window window)
			{
				window.Close();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
