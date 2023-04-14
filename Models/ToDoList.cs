using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ToDoListApp.Models
{
	[Serializable]
	public class ToDoList : INotifyPropertyChanged
	{
		public ToDoList() { }
		public ToDoList(string name, string imagePath) 
		{
			Name = name;
			ImagePath = imagePath;
			SubToDoLists = new ObservableCollection<ToDoList>();
			Tasks = new ObservableCollection<Task>();
		}
		private string name;
		private string imagePath;
		private ObservableCollection<ToDoList> subToDoLists;
		private ObservableCollection<Task> tasks;

		[XmlAttribute]
		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				NotifyPropertyChanged(nameof(Name));
			}
		}

		[XmlAttribute]
		public string ImagePath
		{
			get { return imagePath; }
			set
			{
				imagePath = value;
				NotifyPropertyChanged(nameof(ImagePath));
			}
		}

		[XmlArray]
		public ObservableCollection<ToDoList> SubToDoLists
		{
			get { return subToDoLists; }
			set
			{
				subToDoLists = value;
			}
		}

		[XmlArray]
		public ObservableCollection<Task> Tasks
		{
			get { return tasks; }
			set
			{
				tasks = value;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
