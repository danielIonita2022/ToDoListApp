using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.Models
{
	public static class Categories
	{
		public static ObservableCollection<string> CategoryList { get; set; }
	}
}
