using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoListApp.Models;

namespace ToDoListApp.Comparers
{
	public class TaskComparer : IComparer<Task>
	{
		private string sortBy;

		public TaskComparer(string sortBy)
		{
			this.sortBy = sortBy;
		}

		public int Compare(Task x, Task y)
		{
			if (sortBy == "priority")
			{
				if((x.TaskPriority == "Low" && (y.TaskPriority == "High" || y.TaskPriority == "Medium"))
					|| (x.TaskPriority == "Medium" && y.TaskPriority == "High"))
				{
					return 1;
				}
				else if ((y.TaskPriority == "Low" && (x.TaskPriority == "High" || x.TaskPriority == "Medium"))
					|| (y.TaskPriority == "Medium" && x.TaskPriority == "High"))
				{
					return -1;
				}
				else
				{
					return 0;
				}
			}
			else if (sortBy == "deadline")
			{
				return x.Deadline.CompareTo(y.Deadline);
			}
			else
			{
				throw new ArgumentException("Invalid sort parameter");
			}
		}
	}
}
