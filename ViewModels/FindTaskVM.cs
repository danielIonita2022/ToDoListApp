using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToDoListApp.Commands;
using ToDoListApp.Models;
using Task = ToDoListApp.Models.Task;

namespace ToDoListApp.ViewModels
{
	internal class FindTaskVM : INotifyPropertyChanged
	{
		public ObservableCollection<ToDoList> ToDoLists { get; set; }
		public ObservableCollection<Task> Tasks { get; set; }
		public string SearchedTask { get; set; }
		public DateTime SearchedDate { get; set; } = DateTime.Now;
		public bool IsInCurrentView { get; set; }

		private bool isSearchingDeadline;
		public bool IsSearchingDeadline
		{
			get { return isSearchingDeadline; }
			set
			{
				isSearchingDeadline = value;
				NotifyPropertyChanged(nameof(IsSearchingDeadline));
			}
		}

		public FindTaskVM()
		{
			Tasks = new ObservableCollection<Task>();
			ToDoLists = new ObservableCollection<ToDoList>();
		}

		public FindTaskVM(ObservableCollection<ToDoList> tdls)
		{
			ToDoLists = tdls;
		}

		private ICommand findCommand;
		public ICommand FindCommand
		{
			get
			{
				if(findCommand == null)
				{
					findCommand = new RelayCommand(Find);
				}
				return findCommand;
			}
			set { findCommand = value; }
		}

		private ICommand closeCommand;
		public ICommand CloseCommand
		{
			get
			{
				if (closeCommand == null)
				{
					closeCommand = new RelayCommand(Close);
				}
				return closeCommand;
			}
		}

		private void Find(object param)
		{
			Tasks.Clear();
			if (!IsSearchingDeadline)
			{
				RecursiveSearch(ToDoLists, "", false);
			}
			else RecursiveSearch(ToDoLists, "", true);
		}

		private void RecursiveSearch(ObservableCollection<ToDoList> listTDL, string path, bool isDeadline)
		{
			foreach(ToDoList item in listTDL)
			{
				path += item.Name;
				foreach(Task task in item.Tasks)
				{
					if (task.Name == SearchedTask && !isDeadline)
					{
						string finalpath = path;
						if (path.EndsWith(" -> "))
						{
							finalpath = path.Remove(path.Length - 3);
						}
						task.TaskPath = finalpath;
						Tasks.Add(task);
					}
					if(task.Deadline == SearchedDate && isDeadline)
					{
						string finalpath = path;
						if (path.EndsWith(" -> "))
						{
							finalpath = path.Remove(path.Length - 3);
						}
						task.TaskPath = finalpath;
						Tasks.Add(task);
					}
				}
				if (!IsInCurrentView)
				{
					path += " -> ";
					RecursiveSearch(item.SubToDoLists, path, isDeadline);
					path = "";
				}
				path = "";
			}
		}

		private void Close(object param)
		{
			if (param is Window window)
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
