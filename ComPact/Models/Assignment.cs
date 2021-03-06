﻿using System;
using Newtonsoft.Json;
using SQLite;

namespace ComPact.Models
{
	public class Assignment
	{

		public string Id { get; set; }
		public string ItemName { get; set; }
		public string Description { get; set; }
		public string AdminId { get; set; }
		public Member Member { get; set; }
		public string IconName { get; set; }
		public bool Done { get; set; }

		public override string ToString()
		{
			return string.Format("[Assignment: Id={0}, ItemName={1}, Description={2}, member={3}, IconName={4}, Done={5}]", Id, ItemName, Description, Member, IconName, Done);
		}

	}
}
