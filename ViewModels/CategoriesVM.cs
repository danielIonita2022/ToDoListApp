using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoListApp.Commands;
using ToDoListApp.Models;
using ToDoListApp.Views;

namespace ToDoListApp.ViewModels
{
	internal class CategoriesVM : INotifyPropertyChanged
	{
		public CategoriesVM() 
		{
			Messenger.Default.Register<string>(this, ReceiveName);
		}

		private ObservableCollection<string> categoryList = Categories.CategoryList;
		public ObservableCollection<string> CategoryCollection
		{
			get { return categoryList; }
			set
			{
				categoryList = value;
				Categories.CategoryList = categoryList;
				NotifyPropertyChanged("CategoryCollection");
			}
		}

		public string selectedCategory;
		public string SelectedCategory
		{
			get { return selectedCategory; } 
			set
			{
				selectedCategory = value;
				NotifyPropertyChanged("SelectedCategory");
			}
		}

		private string newCategory;

		private ICommand addCategoryCommand;
		public ICommand AddCategoryCommand
		{
			get
			{
				if (addCategoryCommand == null)
				{
					addCategoryCommand = new RelayCommand(AddCategory);
				}
				return addCategoryCommand;
			}
		}
		private ICommand editCategoryCommand;
		public ICommand EditCategoryCommand
		{
			get
			{
				if (editCategoryCommand == null)
				{
					editCategoryCommand = new RelayCommand(EditCategory);
				}
				return editCategoryCommand;
			}
			set
			{
				editCategoryCommand = value;
			}
		}
		private ICommand deleteCategoryCommand;
		public ICommand DeleteCategoryCommand
		{
			get
			{
				if (deleteCategoryCommand == null)
				{
					deleteCategoryCommand = new RelayCommand(DeleteCategory);
				}
				return deleteCategoryCommand;
			}
			set
			{
				deleteCategoryCommand = value;
			}
		}
		

		public void AddCategory(object param)
		{
			AddCategoryWindow addCategoryWindow = new AddCategoryWindow();
			addCategoryWindow.ShowDialog();
			CategoryCollection.Add(newCategory);
		}

		public void EditCategory(object param)
		{
			AddCategoryWindow addCategoryWindow= new AddCategoryWindow();
			addCategoryWindow.ShowDialog();
			CategoryCollection[CategoryCollection.IndexOf(SelectedCategory)] = newCategory;
		}

		public void ReceiveName(string name)
		{
			newCategory = name;
		}

		public void DeleteCategory(object param)
		{
			CategoryCollection.Remove(SelectedCategory);
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
