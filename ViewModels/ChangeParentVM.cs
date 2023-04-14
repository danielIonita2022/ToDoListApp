using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.Models;
using ToDoListApp.Commands;
using System.ComponentModel;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel.Design.Serialization;
using System.Windows;
using System.Windows.Input;

namespace ToDoListApp.ViewModels
{
	internal class ChangeParentVM : INotifyPropertyChanged
	{

		public ChangeParentVM()
		{
			TDLItems = new ObservableCollection<ToDoList>();
			Messenger.Default.Register<string>(this, ReceiveCurrentToDoList);
			Messenger.Default.Register<ObservableCollection<ToDoList>>(this, ReceiveToDoList);
		}

		private bool isRoot = true;
		public bool IsRoot
		{
			get { return isRoot; }
			set
			{
				isRoot = value;
				NotifyPropertyChanged("IsRoot");
			}
		}
		
		public string ToDoListName { get; set; }
		public ToDoList SelectedParent { get; set; }
		public ObservableCollection<ToDoList> TDLItems { get; set; }

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

		public event PropertyChangedEventHandler PropertyChanged;

		public void ReceiveCurrentToDoList(string toDoList)
		{
			ToDoListName = toDoList;
			NotifyPropertyChanged("ToDoListName");
		}
		public void ReceiveToDoList(ObservableCollection<ToDoList> TDLCollection)
		{
			GetAllToDoLists(TDLCollection);
			NotifyPropertyChanged("TDLItems");
		}
		public void GetAllToDoLists(ObservableCollection<ToDoList> TDLCollection)
		{
			foreach(ToDoList toDo in TDLCollection)
			{
				if (toDo.Name != ToDoListName)
				{
					TDLItems.Add(toDo);
				}
				GetAllToDoLists(toDo.SubToDoLists);
			}
		}
		public void Confirm(object param)
		{
			Messenger.Default.Send(SelectedParent);
			if(param is Window window)
			{
				window.Close();
			}
		}
		protected void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
