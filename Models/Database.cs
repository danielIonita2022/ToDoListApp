using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ToDoListApp.Models
{
	[Serializable]
	public class Database
	{
		public static string Name { get; set; }

		public static string Path { get; set; }

		[XmlArray]
		public ObservableCollection<ToDoList> ToDoLists { get; set; }

		[XmlArray]
		public ObservableCollection<string> Categories { get; set; }
	}
}
