using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = ToDoListApp.Models.Task;

namespace ToDoListApp.ViewModels
{
	internal class StatisticsVM : INotifyPropertyChanged
	{
		public StatisticsVM() 
		{
			Messenger.Default.Register<ObservableCollection<Task>>(this, RegisterTasks);
		}
		public ObservableCollection<Task> TaskList { get; set; }
		private int nrOfDeadlineToday;
		public int NrOfDeadlineToday
		{
			get { return nrOfDeadlineToday; }
			set
			{
				nrOfDeadlineToday = value;
				NotifyPropertyChanged(nameof(NrOfDeadlineToday));
			}
		}
		private int nrOfDeadlineTomorrow;
		public int NrOfDeadlineTomorrow
		{
			get { return nrOfDeadlineTomorrow; }
			set
			{
				nrOfDeadlineTomorrow = value;
				NotifyPropertyChanged(nameof(NrOfDeadlineTomorrow));
			}
		}
		private int nrOfOverdue;
		public int NrOfOverdue
		{
			get { return nrOfOverdue; }
			set
			{
				nrOfOverdue = value;
				NotifyPropertyChanged(nameof(NrOfOverdue));
			}
		}
		private int nrOfUndone;
		public int NrOfUndone
		{
			get { return nrOfUndone; }
			set
			{
				nrOfUndone = value;
				NotifyPropertyChanged(nameof(NrOfUndone));
			}
		}
		private int nrOfDone;
		public int NrOfDone
		{
			get { return nrOfDone; }
			set
			{
				nrOfDone = value;
				NotifyPropertyChanged(nameof(NrOfDone));
			}
		}

		public void RegisterTasks(ObservableCollection<Task> tasks)
		{
			TaskList = tasks;
			CountDeadlineToday();
			CountDeadlineTomorrow();
			CountOverdue();
			CountUndone();
			CountDone();
		}
		public void CountDeadlineToday()
		{
			foreach (Task task in TaskList)
			{
				if (!task.TaskStatus)
				{
					if (task.Deadline.Date == DateTime.Today)
					{
						NrOfDeadlineToday++;
					}
				}
			}
		}
		public void CountDeadlineTomorrow()
		{
			foreach (Task task in TaskList)
			{
				if (!task.TaskStatus)
				{
					if (task.Deadline.Date == DateTime.Today.AddDays(1))
					{
						NrOfDeadlineTomorrow++;
					}
				}
			}
		}

		public void CountOverdue()
		{
			foreach (Task task in TaskList)
			{
				if (task.TaskStatus == true)
				{
					if (task.FinishDate.Date > task.Deadline.Date)
					{
						NrOfOverdue++;
					}
				}
			}
		}
		public void CountUndone()
		{
			foreach (Task task in TaskList)
			{
				if (task.TaskStatus == false)
				{
					NrOfUndone++;
				}
			}
		}
		public void CountDone()
		{
			foreach (Task task in TaskList)
			{
				if (task.TaskStatus == true)
				{
					NrOfDone++;
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
