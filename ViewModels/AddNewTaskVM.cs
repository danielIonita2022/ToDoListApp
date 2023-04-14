using GalaSoft.MvvmLight.Messaging;
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
	internal class AddNewTaskVM : INotifyPropertyChanged
	{
		private string name;
		public string Name 
		{ 
			get { return name; } 
			set
			{
				name = value; 
				NotifyPropertyChanged("Name");
			}
		}
		private string description;
		public string Description
		{
			get { return description; }
			set
			{
				description = value; 
				NotifyPropertyChanged("Description");
			}
		}
		private string taskPriority;
		public string TaskPriority
		{
			get { return taskPriority; }
			set
			{
				taskPriority = value;
				NotifyPropertyChanged("TaskPriority");
			}
		}
		private string taskCategory;
		public string TaskCategory 
		{ 
			get { return taskCategory;} 
			set
			{
				taskCategory = value;
				NotifyPropertyChanged("TaskCategory");
			}
		}
		public DateTime Deadline { get; set; }
		public ObservableCollection<string> CategoryList { get; set; } = Categories.CategoryList;
		public ObservableCollection<string> PriorityList { get; set; } = new ObservableCollection<string>()
		{
			"Low", "Medium", "High"
		};

		public AddNewTaskVM()
		{
			Name = "";
			Description = "";
			TaskPriority = "";
			TaskCategory = "";
			Deadline = DateTime.Now;
		}

		private ICommand createTaskCommand;
		public ICommand CreateTaskCommand
		{
			get
			{
				if(createTaskCommand == null)
				{
					createTaskCommand = new RelayCommand(CreateTask);
				}
				return createTaskCommand;
			}
			set
			{
				createTaskCommand = value;
			}
		}


		private void CreateTask(object param)
		{
			Task newTask = new Task(Name, Description, TaskPriority, TaskCategory, Deadline);
			Messenger.Default.Send(newTask);
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
