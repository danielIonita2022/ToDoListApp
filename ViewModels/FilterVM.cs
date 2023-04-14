using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToDoListApp.Commands;
using ToDoListApp.Models;

namespace ToDoListApp.ViewModels
{
	internal class FilterVM
	{
		public string TaskFilter { get; set; }
		public ObservableCollection<string> CategoryList { get; set; } = Categories.CategoryList;

		public bool IsDoneChecked { get; set; }
		public bool IsOverdueChecked { get; set; }
		public bool IsUnfinishedOverdueChecked { get; set; }
		public bool IsUnfinishedDueChecked { get; set; }
		public bool IsCategoryChecked { get; set; }


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
			if (IsDoneChecked)
			{
				TaskFilter = "Done";
			}
			else if(IsOverdueChecked)
			{
				TaskFilter = "Overdue";
			}
			else if (IsUnfinishedOverdueChecked)
			{
				TaskFilter = "Unfinished overdue";
			}
			else if(IsUnfinishedDueChecked)
			{
				TaskFilter = "Unfinished due";
			}
			Messenger.Default.Send(TaskFilter);
			if(param is Window window)
			{
				window.Close();			
			}
		}
	}
}
