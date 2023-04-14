using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml.Linq;
using System.Xml.Serialization;
using ToDoListApp.Commands;
using ToDoListApp.Comparers;
using ToDoListApp.Models;
using ToDoListApp.Views;
using Task = ToDoListApp.Models.Task;

namespace ToDoListApp.ViewModels
{
	internal class MainWindowVM : INotifyPropertyChanged
	{
		public Database DB { get; set; }
		private ObservableCollection<ToDoList> itemsToDoList;
		public ObservableCollection<ToDoList> ItemsToDoList
		{
			get { return itemsToDoList; }
			set
			{
				itemsToDoList = value;
				NotifyPropertyChanged(nameof(ItemsToDoList));
			}
		}

		private ObservableCollection<Task> itemsTasks;
		public ObservableCollection<Task> ItemsTasks
		{
			get { return itemsTasks; }
			set
			{
				itemsTasks = value;
				NotifyPropertyChanged(nameof(ItemsTasks));
			}
		}

		private ToDoList selectedToDoList;
		public ToDoList SelectedToDoList
		{
			get { return selectedToDoList; }
			set
			{
				if (selectedToDoList != value)
				{
					selectedToDoList = value;
					NotifyPropertyChanged(nameof(SelectedToDoList));
				}
			}
		}

		private Task selectedTask;
		public Task SelectedTask
		{
			get { return selectedTask; }
			set
			{
				selectedTask = value;
				NotifyPropertyChanged("TaskDescription");
			}
		}

		public ToDoList ParentTDL { get; set; }
		public bool IsDone
		{
			get
			{
				return (bool)(selectedTask?.TaskStatus);
			}
			set
			{
				selectedTask.TaskStatus = value;
			}
		}
		public string CurrentFilter { get; set; }
		public string SortCriterion { get; set; }

		private ICommand changeToDoListCommand;
		public ICommand SelectedToDoListCommand
		{
			get
			{
				if (changeToDoListCommand == null)
				{
					changeToDoListCommand = new RelayCommand(UpdateTDLSelection);
				}
				return changeToDoListCommand;
			}
			set
			{
				changeToDoListCommand = value;
			}
		}

		private ICommand newDBCreated;
		public ICommand NewDBCreatedCommand
		{
			get
			{
				if (newDBCreated == null)
				{
					newDBCreated = new RelayCommand(CreateDatabase);
				}
				return newDBCreated;
			}
			set
			{
				newDBCreated = value;
			}
		}

		private ICommand dbSavedCommand;

		public ICommand DBSavedCommand
		{
			get
			{
				if (dbSavedCommand == null)
				{
					dbSavedCommand = new RelayCommand(SaveDatabase);
				}
				return dbSavedCommand;
			}
			set
			{
				dbSavedCommand = value;
			}
		}

		private ICommand dbLoadedCommand;
		public ICommand DBLoadedCommand
		{
			get
			{
				if(dbLoadedCommand == null)
				{
					dbLoadedCommand = new RelayCommand(LoadDatabase);
				}
				return dbLoadedCommand;
			}
			set
			{
				dbLoadedCommand = value;
			}
		}

		private ICommand addRootTDLCommand;
		public ICommand AddRootTDLCommand
		{
			get
			{
				if(addRootTDLCommand == null)
				{
					addRootTDLCommand = new RelayCommand(AddRootTDL);
				}
				return addRootTDLCommand;
			}
			set
			{
				addRootTDLCommand= value;
			}
		}

		private ICommand addSubTDLCommand;
		public ICommand AddSubTDLCommand
		{
			get
			{
				if (addSubTDLCommand == null)
				{
					addSubTDLCommand = new RelayCommand(AddSubTDL);
				}
				return addSubTDLCommand;
			}
			set
			{
				addSubTDLCommand = value;
			}
		}

		private ICommand editTDLCommand;
		public ICommand EditTDLCommand
		{
			get
			{
				if(editTDLCommand == null)
				{
					editTDLCommand = new RelayCommand(EditTDL);
				}
				return editTDLCommand;
			}
			set
			{ 
				editTDLCommand= value;
			}
		}

		private ICommand deleteTDLCommand;
		public ICommand DeleteTDLCommand
		{
			get
			{
				if (deleteTDLCommand == null)
				{
					deleteTDLCommand = new RelayCommand(DeleteTDL);
				}
				return deleteTDLCommand;
			}
			set
			{
				deleteTDLCommand = value;
			}
		}

		private ICommand moveUpTDLCommand;
		public ICommand MoveUpTDLCommand
		{
			get
			{
				if(moveUpTDLCommand == null)
				{
					moveUpTDLCommand = new RelayCommand(MoveUpTDL);
				}
				return moveUpTDLCommand;
			}
			set
			{
				moveUpTDLCommand = value;
			}
		}

		private ICommand moveDownTDLCommand;
		public ICommand MoveDownTDLCommand
		{
			get
			{
				if (moveDownTDLCommand == null)
				{
					moveDownTDLCommand = new RelayCommand(MoveDownTDL);
				}
				return moveDownTDLCommand;
			}
			set
			{
				moveDownTDLCommand = value;
			}
		}

		private ICommand changePathCommand;
		public ICommand ChangePathCommand
		{
			get
			{
				if (changePathCommand == null)
				{
					changePathCommand = new RelayCommand(ChangePath);
				}
				return changePathCommand;
			}
			set
			{
				changePathCommand = value;
			}
		}

		private ICommand addTaskCommand;
		public ICommand AddTaskCommand
		{
			get
			{
				if (addTaskCommand == null)
				{
					addTaskCommand = new RelayCommand(AddTask);
				}
				return addTaskCommand;
			}
			set
			{
				addTaskCommand = value;
			}
		}
		private ICommand editTaskCommand;
		public ICommand EditTaskCommand
		{
			get
			{
				if (editTaskCommand == null)
				{
					editTaskCommand = new RelayCommand(EditTask);
				}
				return editTaskCommand;
			}
			set
			{
				editTaskCommand = value;
			}
		}


		private ICommand removeTaskCommand;
		public ICommand RemoveTaskCommand
		{
			get
			{
				if (removeTaskCommand == null)
				{
					removeTaskCommand = new RelayCommand(RemoveTask);
				}
				return removeTaskCommand;
			}
			set
			{
				removeTaskCommand = value;
			}
		}

		private ICommand moveUpTaskCommand;
		public ICommand MoveUpTaskCommand
		{
			get
			{
				if (moveUpTaskCommand == null)
				{
					moveUpTaskCommand = new RelayCommand(MoveUpTask);
				}
				return moveUpTaskCommand;
			}
			set
			{
				moveUpTaskCommand = value;
			}
		}

		private ICommand moveDownTaskCommand;
		public ICommand MoveDownTaskCommand
		{
			get
			{
				if (moveDownTaskCommand == null)
				{
					moveDownTaskCommand = new RelayCommand(MoveDownTask);
				}
				return moveDownTaskCommand;
			}
			set
			{
				moveDownTaskCommand = value;
			}
		}

		private ICommand manageCategoryCommand;
		public ICommand ManageCategoryCommand
		{
			get
			{
				if (manageCategoryCommand == null)
				{
					manageCategoryCommand = new RelayCommand(ManageCategory);
				}
				return manageCategoryCommand;
			}
			set
			{
				manageCategoryCommand = value;
			}
		}

		private ICommand statsCommand;
		public ICommand StatsCommand
		{
			get
			{
				if (statsCommand == null)
				{
					statsCommand = new RelayCommand(ShowStatistics);
				}
				return statsCommand;
			}
			set
			{
				statsCommand = value;
			}
		}

		private ICommand filterCommand;
		public ICommand FilterCommand
		{
			get
			{
				if (filterCommand == null)
				{
					filterCommand = new RelayCommand(Filter);
				}
				return filterCommand;
			}
			set
			{
				filterCommand = value;
			}
		}

		private ICommand sortCommand;
		public ICommand SortCommand
		{
			get
			{
				if (sortCommand == null)
				{
					sortCommand = new RelayCommand(SortTasks);
				}
				return sortCommand;
			}
			set
			{
				sortCommand = value;
			}
		}

		private ICommand findTaskCommand;
		public ICommand FindTaskCommand
		{
			get
			{
				if (findTaskCommand == null)
				{
					findTaskCommand = new RelayCommand(FindTask);
				}
				return findTaskCommand;
			}
			set
			{
				findTaskCommand = value;
			}
		}

		private ICommand aboutCommand;
		public ICommand AboutCommand
		{
			get
			{
				if (aboutCommand == null)
				{
					aboutCommand = new RelayCommand(About);
				}
				return aboutCommand;
			}
			set
			{
				aboutCommand = value;
			}
		}

		private ICommand exitCommand;
		public ICommand ExitCommand
		{
			get
			{
				if(exitCommand == null)
				{
					exitCommand = new RelayCommand(Exit);
				}
				return exitCommand;
			}
			set
			{
				exitCommand= value;
			}
		}

		public string TaskDescription
		{
			get 
			{
				return SelectedTask?.Description;
			}
		}
		private bool isButtonEnabled;
		public bool IsButtonEnabled
		{
			get
			{
				return isButtonEnabled;
			}
			set
			{
				isButtonEnabled = value;
				NotifyPropertyChanged("IsButtonEnabled");
			}
		}

		private bool isTDLSelected;
		public bool IsTDLSelected
		{
			get
			{
				return isTDLSelected;
			}
			set
			{
				isTDLSelected = value;
				NotifyPropertyChanged("IsTDLSelected");
			}
		}

		public bool IsEditing { get; set; } = false;

		public MainWindowVM()
		{
			ItemsToDoList = new ObservableCollection<ToDoList>();
			DB = new Database();
			Categories.CategoryList = new ObservableCollection<string>()
			{
				"Home", "Personal", "School", "Work", "Pet" ,"Miscellaneous"
			};
		}

		public void UpdateTDLSelection(object param)
		{
			SelectedToDoList = param as ToDoList;
			if(SelectedToDoList != null)
				ItemsTasks = SelectedToDoList.Tasks;
			IsTDLSelected = true;
		}

		public void CreateDatabase(object param)
		{
			NewDBWindow newDBWindow = new NewDBWindow();
			newDBWindow.ShowDialog();
			IsButtonEnabled = true;
			ItemsToDoList = new ObservableCollection<ToDoList>();
			ItemsTasks = new ObservableCollection<Task>();
		}

		public void SaveDatabase(object param)
		{
			DB.ToDoLists = new ObservableCollection<ToDoList>(ItemsToDoList);
			DB.Categories = Categories.CategoryList;

			if (!File.Exists(Database.Path))
			{
				FileStream fileStream = File.Create(Database.Path);
				fileStream.Close();
			}

			XmlSerializer serializer = new XmlSerializer(typeof(Database));
			using (TextWriter writer = new StreamWriter(Database.Path))
			{
				serializer.Serialize(writer, DB);
			}
			MessageBox.Show("Database successfully saved!");
		}

		public void LoadDatabase(object param)
		{

			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				// Set the initial directory for the file explorer dialog
				InitialDirectory = "D:\\SEM2\\MVP\\ToDoListApp\\ToDoListApp\\resources\\databases",
				Title = "Open Database",
				Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*"
			};

			if (openFileDialog.ShowDialog() == true)
			{
				// Get the path of the selected file
				Database.Path = openFileDialog.FileName;
			}

			XmlSerializer serializer = new XmlSerializer(typeof(Database));
			using (TextReader reader = new StreamReader(Database.Path))
			{
				DB = (Database)serializer.Deserialize(reader);
			}
			if (DB.Categories != null)
			{
				Categories.CategoryList = DB.Categories;
			}
			ItemsToDoList = DB.ToDoLists;
			IsButtonEnabled = true;
			MessageBox.Show("Database successfully loaded!");
		}

		public void AddTask(object param)
		{
			AddNewTask addNewTask = new AddNewTask();
			Messenger.Default.Register<Task>(this, ReceiveTask);
			addNewTask.ShowDialog();
		}
		public void EditTask(object param)
		{
			AddNewTask addNewTask = new AddNewTask();
			IsEditing = true;
			Messenger.Default.Register<Task>(this, ReceiveTask);
			addNewTask.ShowDialog();
		}
		public void ReceiveTask(Task task)
		{
			if (IsEditing)
			{
				int replacedIndex = SelectedToDoList.Tasks.IndexOf(SelectedTask);
				SelectedToDoList.Tasks[replacedIndex] = task;
				IsEditing = false;
			}

			else
			{
				SelectedToDoList.Tasks.Add(task);
			}
			Messenger.Default.Unregister<Task>(this, ReceiveTask);

		}
		public void RemoveTask(object param)
		{
			SelectedToDoList.Tasks.Remove(SelectedTask);
		}

		public void MoveUpTask(object param)
		{
			int index = SelectedToDoList.Tasks.IndexOf(SelectedTask);
			if(index > 0)
			{
				(SelectedToDoList.Tasks[index], SelectedToDoList.Tasks[index - 1]) =
					(SelectedToDoList.Tasks[index - 1], SelectedToDoList.Tasks[index]);
			}
		}
		public void MoveDownTask(object param)
		{
			int index = SelectedToDoList.Tasks.IndexOf(SelectedTask);
			if (index < SelectedToDoList.Tasks.Count - 1)
			{
				(SelectedToDoList.Tasks[index], SelectedToDoList.Tasks[index + 1]) =
					(SelectedToDoList.Tasks[index + 1], SelectedToDoList.Tasks[index]);
			}
		}

		public void ManageCategory(object param)
		{
			CategoriesWindow categoriesWindow = new CategoriesWindow();
			categoriesWindow.ShowDialog();
		}

		public void ShowStatistics(object param)
		{
			StatisticsWindow statisticsWindow = new StatisticsWindow();
			ObservableCollection<Task> allTasks = new ObservableCollection<Task>();
			CountTasks(allTasks, ItemsToDoList);
			Messenger.Default.Send(allTasks);
			statisticsWindow.ShowDialog();
		}

		void CountTasks(ObservableCollection<Task> tasks, ObservableCollection<ToDoList> currentTDL)
		{
			foreach (ToDoList tdl in currentTDL)
			{
				foreach (Task task in tdl.Tasks)
				{
					tasks.Add(task);
				}
				CountTasks(tasks, tdl.SubToDoLists);
			}
		}

		public void AddRootTDL(object param)
		{
			AddNewTDL addNewTDL = new AddNewTDL();
			Messenger.Default.Send(ItemsToDoList);
			Messenger.Default.Register<ToDoList>(this, ReceiveObject);
			addNewTDL.ShowDialog();
		}

		public void AddSubTDL(object param)
		{
			ParentTDL = param as ToDoList;
			AddNewTDL addNewTDL = new AddNewTDL();
			Messenger.Default.Send(ItemsToDoList);
			Messenger.Default.Register<ToDoList>(this, ReceiveObject);
			addNewTDL.ShowDialog();
		}

		public void EditTDL(object param)
		{
			AddNewTDL editTDL = new AddNewTDL();
			IsEditing = true;
			Messenger.Default.Send(ItemsToDoList);
			Messenger.Default.Register<ToDoList>(this, ReceiveObject);
			editTDL.ShowDialog();
		}

		public void DeleteTDL(object param)
		{
			ToDoList tdl = SelectedToDoList;
			FindAndEditTDL(tdl, ItemsToDoList, true);
		}

		public void MoveUpTDL(object param)
		{
			FindAndSwapTDL(ItemsToDoList, -1);
		}

		public void MoveDownTDL(object param)
		{
			FindAndSwapTDL(ItemsToDoList, 1);
		}

		public void ChangePath(object param)
		{
			ChangeParentWindow changeParentWindow = new ChangeParentWindow();
			string toDoListName = SelectedToDoList.Name;
			Messenger.Default.Send(toDoListName);
			Messenger.Default.Send(ItemsToDoList);
			Messenger.Default.Register<ToDoList>(this, ReceiveParent);
			changeParentWindow.ShowDialog();
		}
		public void ReceiveParent(ToDoList parentTDL)
		{
			ToDoList temp = SelectedToDoList;
			ObservableCollection<ToDoList> formerParentList = FindParent(SelectedToDoList, ItemsToDoList);
			if (formerParentList != ItemsToDoList || parentTDL != null)
			{
				formerParentList.Remove(temp);
			}
			else
			{
				ItemsToDoList.Remove(temp);
			}

			if (parentTDL != null)
			{
				parentTDL.SubToDoLists.Add(temp);
			}
			else
			{
				ItemsToDoList.Add(temp);
			}
			Messenger.Default.Unregister<ToDoList>(this, ReceiveParent);
		}

		private ObservableCollection<ToDoList> FindParent(ToDoList sonTDL, ObservableCollection<ToDoList> sourceTDL)
		{
			for (int i = 0; i < sourceTDL.Count; ++i)
			{
				if (sourceTDL[i] == sonTDL)
				{
					return sourceTDL;
				}
				if (sourceTDL[i] is ToDoList)
				{
					ObservableCollection<ToDoList> parentTDL = FindParent(sonTDL, sourceTDL[i].SubToDoLists);
					if (parentTDL != null)
					{
						return parentTDL;
					}
				}
			}

			return null;
		}

		private void FindAndSwapTDL(ObservableCollection<ToDoList> sourceTDL, int direction)
		{
			for (int i = 0; i < sourceTDL.Count; ++i)
			{
				if (sourceTDL[i] == SelectedToDoList)
				{
					if((i > 0 && direction == -1) || (i < sourceTDL.Count - 1 && direction == 1))
					{
						(sourceTDL[i], sourceTDL[i + direction]) = (sourceTDL[i + direction], sourceTDL[i]);
						return;
					}
				}

				if (sourceTDL[i] is ToDoList)
				{
					FindAndSwapTDL(sourceTDL[i].SubToDoLists, direction);
				}
			}
		}

		private void FindAndEditTDL(ToDoList tdl, ObservableCollection<ToDoList> sourceTDL, bool delete = false)
		{
			for (int i = 0; i < sourceTDL.Count; ++i)
			{
				if (sourceTDL[i] == SelectedToDoList)
				{
					if (delete)
					{
						SelectedToDoList = null;
						sourceTDL.RemoveAt(i);
					}
					else
					{
						SelectedToDoList.Name = tdl.Name;
						SelectedToDoList.ImagePath = tdl.ImagePath;
						sourceTDL[i] = SelectedToDoList;
					}
					return;
				}
				if (sourceTDL[i] is ToDoList)
				{
					FindAndEditTDL(tdl, sourceTDL[i].SubToDoLists, delete);
				}
			}
		}

		private void ReceiveObject(ToDoList tdl)
		{
			if (IsEditing)
			{
				FindAndEditTDL(tdl, ItemsToDoList);
				IsEditing = false;
			}
			else
			{
				if (ParentTDL == null)
				{
					ItemsToDoList.Add(tdl);
				}
				else
				{
					ParentTDL.SubToDoLists.Add(tdl);
				}
			}
			ParentTDL = null;
			Messenger.Default.Unregister<ToDoList>(this, ReceiveObject);
		}

		private void Filter(object param)
		{
			FilterWindow filterWindow = new FilterWindow();
			Messenger.Default.Register<string>(this, ReceivedFilter);
			filterWindow.ShowDialog();

			if(CurrentFilter == "Done")
			{
				ItemsTasks = new ObservableCollection<Task>(ItemsTasks.Where(x => x.TaskStatus == true));
			}
			else if(CurrentFilter == "Overdue")
			{
				ItemsTasks = new ObservableCollection<Task>(ItemsTasks.Where(x => x.TaskStatus == true && x.FinishDate > x.Deadline));
			}
			else if(CurrentFilter == "Unfinished overdue")
			{
				ItemsTasks = new ObservableCollection<Task>(ItemsTasks.Where(x => x.TaskStatus == false && x.Deadline < DateTime.Now));
			}
			else if(CurrentFilter == "Unfinished due")
			{
				ItemsTasks = new ObservableCollection<Task>(ItemsTasks.Where(x => x.TaskStatus == false && x.Deadline > DateTime.Now));
			}
			else
			{
				foreach(string category in Categories.CategoryList)
				{
					if(category == CurrentFilter)
					{
						ItemsTasks = new ObservableCollection<Task>(ItemsTasks.Where(x => x.TaskCategory == category));
						break;
					}
				}
			}
		}

		private void ReceivedFilter(string filter)
		{
			CurrentFilter = filter;
			Messenger.Default.Unregister<string>(this, ReceivedFilter);
		}
		private void SortTasks(object param)
		{
			SortWindow sortWindow = new SortWindow();
			Messenger.Default.Register<string>(this, ReceivedSort);
			sortWindow.ShowDialog();

			List<Task> tasks = ItemsTasks.OrderBy(task => task, new TaskComparer(SortCriterion)).ToList();
			ItemsTasks = new ObservableCollection<Task>(tasks);
		}
		private void ReceivedSort(string sortCriterion)
		{
			SortCriterion = sortCriterion;
			Messenger.Default.Unregister<string>(this, ReceivedSort);
		}

		private void FindTask(object param)
		{
			FindTaskWindow findTaskWindow = new FindTaskWindow();
			FindTaskVM findTaskVM = findTaskWindow.DataContext as FindTaskVM;
			findTaskVM.ToDoLists = ItemsToDoList;
			findTaskWindow.ShowDialog();
		}
		private void About(object param)
		{
			AboutWindow aboutWindow = new AboutWindow();
			aboutWindow.ShowDialog();
		}
		private void Exit(object param)
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
