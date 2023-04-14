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
using System.Xml.Linq;
using ToDoListApp.Commands;
using ToDoListApp.Models;

namespace ToDoListApp.ViewModels
{
	internal class AddNewTDLVM : INotifyPropertyChanged
	{

		public AddNewTDLVM() 
		{
			IconList = new List<string>()
			{
				"D:\\SEM2\\MVP\\ToDoListApp\\ToDoListApp\\resources\\icons\\briefcase.png",
				"D:\\SEM2\\MVP\\ToDoListApp\\ToDoListApp\\resources\\icons\\bulb.png",
				"D:\\SEM2\\MVP\\ToDoListApp\\ToDoListApp\\resources\\icons\\graduation-cap.png",
				"D:\\SEM2\\MVP\\ToDoListApp\\ToDoListApp\\resources\\icons\\home.png",
				"D:\\SEM2\\MVP\\ToDoListApp\\ToDoListApp\\resources\\icons\\star.png",
				"D:\\SEM2\\MVP\\ToDoListApp\\ToDoListApp\\resources\\icons\\user.png",
			};

			IconIndex = 0;
			ImagePath = IconList[IconIndex];
			Messenger.Default.Register<ObservableCollection<ToDoList>>(this, ReceiveSet);
		}

		HashSet<string> TDLNameSet { get; set; } = new HashSet<string>();

		private void ReceiveSet(ObservableCollection<ToDoList> obj)
		{
			foreach (ToDoList item in obj)
			{
				TDLNameSet.Add(item.Name);
				if(item.SubToDoLists != null)
				{
					ReceiveSet(item.SubToDoLists);
				}
			}
		}

		public List<string> IconList { get; set; }

		public ToDoList CreatedTDL { get; set; }

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

		private string imagePath;
		public string ImagePath
		{
			get { return imagePath; }
			set
			{
				imagePath = value;
				NotifyPropertyChanged("ImagePath");
			}
		}

		public int IconIndex;

		private ICommand nextIconCommand;
		public ICommand NextIconCommand
		{
			get
			{
				if (nextIconCommand == null)
				{
					nextIconCommand = new RelayCommand(NextIcon);
				}
				return nextIconCommand;
			}
			set
			{
				nextIconCommand = value;
			}
		}


		private ICommand prevIconCommand;
		public ICommand PrevIconCommand
		{
			get
			{
				if (prevIconCommand == null)
				{
					prevIconCommand = new RelayCommand(PrevIcon);
				}
				return prevIconCommand;
			}
			set
			{
				prevIconCommand = value;
			}
		}

		private ICommand confirmCommand;
		public ICommand ConfirmCommand
		{
			get
			{
				if(confirmCommand == null)
				{
					confirmCommand = new RelayCommand(Confirm);
				}
				return confirmCommand;
			}
			set
			{
				confirmCommand = value;
			}
		}

		public void NextIcon(object param)
		{
			IconIndex++;
			if (IconIndex >= IconList.Count)
				IconIndex = 0;
			ImagePath = IconList[IconIndex];
		}

		public void PrevIcon(object param)
		{
			IconIndex--;
			if(IconIndex < 0)
				IconIndex = IconList.Count - 1;
			ImagePath = IconList[IconIndex];
		}

		public void Confirm(object param)
		{
			if (Name == null)
			{
				MessageBox.Show("Enter a name!");
				return;
			}
			if (TDLNameSet.Contains(Name))
			{
				MessageBox.Show("This To-Do List already exists!");
				return;
			}
			CreatedTDL = new ToDoList(Name, ImagePath);
			Messenger.Default.Send(CreatedTDL);
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
