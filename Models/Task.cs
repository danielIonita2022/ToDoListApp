using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ToDoListApp.Models
{

	[Serializable]
	public class Task : INotifyPropertyChanged
	{
		public Task() { }
		public Task(string name, string description, string priority, string category, DateTime deadline)
		{
			Name = name;
			Description = description;
			TaskPriority = priority;
			TaskCategory = category;
			Deadline = deadline;
		}

		private string name;
		private string description;
		private bool taskStatus;
		private string taskPriority;
		private string taskCategory;
		private DateTime deadline;
		private DateTime finishDate;

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
		public string Description
		{
			get { return description; }
			set
			{
				description = value;
				NotifyPropertyChanged(nameof(Description));
			}
		}

		[XmlAttribute]
		public bool TaskStatus
		{
			get { return taskStatus; }
			set
			{
				taskStatus = value;
				FinishDate = DateTime.Now;
				NotifyPropertyChanged(nameof(TaskStatus));
			}
		}

		[XmlAttribute]
		public string TaskPriority 
		{ 
			get { return taskPriority; }
			set
			{
				taskPriority = value;
				NotifyPropertyChanged(nameof(TaskPriority));
			}
		}

		[XmlAttribute]
		public string TaskCategory 
		{ 
			get { return taskCategory; } 
			set 
			{ 
				taskCategory = value;
				NotifyPropertyChanged(nameof(TaskCategory));
			}
		}

		[XmlAttribute]
		public DateTime Deadline
		{
			get { return deadline; }
			set 
			{ 
				deadline = value;
				NotifyPropertyChanged(nameof(Deadline));
			}
		}

		[XmlAttribute]
		public DateTime FinishDate
		{
			get { return finishDate; }
			set
			{
				finishDate = value;
				NotifyPropertyChanged(nameof(FinishDate));
			}
		}

		private string taskPath;
		public string TaskPath
		{
			get { return taskPath; }
			set
			{
				taskPath = value;
				NotifyPropertyChanged(nameof(TaskPath));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
