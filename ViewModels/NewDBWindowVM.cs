using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using ToDoListApp.Commands;
using ToDoListApp.Models;
using ToDoListApp.Views;

namespace ToDoListApp.ViewModels
{
	internal class NewDBWindowVM : INotifyPropertyChanged
	{
		public string Name
		{
			get;
			set;
		}

		private ICommand commandCreateDB;

		public event PropertyChangedEventHandler PropertyChanged;

		public ICommand CommandCreateDB
		{
			get
			{
				if (commandCreateDB == null)
				{
					commandCreateDB = new RelayCommand(CreateDB);
				}
				return commandCreateDB;
			}
		}
		public NewDBWindowVM()
		{

		}

		public void CreateDB(object parameter)
		{
			Database.Path = "D:\\SEM2\\MVP\\ToDoListApp\\ToDoListApp\\resources\\databases" + "\\" + Name + ".xml";
			Database.Name = Name;

			if (!File.Exists(Database.Path))
			{
				if (parameter is Window window)
				{
					window.Close();
				}
				else
				{
					MessageBox.Show("File already exists!");
				}
			}
		}
	}
}
