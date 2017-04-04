﻿using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Helpers;
using Android.Views;
using ComPact.Droid.Models;
using System.Collections.Generic;
using ComPact.Models;

namespace ComPact.Droid.Fragments
{
	public class AssignmentsFragment : BaseFragment
	{
		//EditText _itemNameEditText;
		//EditText _describtionEditText;
		//Button _createTaskButton;
		FloatingActionButton _addTaskFloatingActionButton;
		ListView _tasksListView;
		//data
		List<Assignment> items;

		AssignmentsViewModel ViewModel
		{
			get
			{
				return App.Locator.TasksViewModel;
			}
		}
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);
			Init();

			HandleEvents();


			//Data & services
			SetBindings();
			SetCommands();

			items = new List<Assignment>();
			items.Add(new Assignment() { ItemName = "item 1" });
			items.Add(new Assignment() { ItemName = "item 2" });
			items.Add(new Assignment() { ItemName = "item 3" });

			var adapter = new AdapterAssignment(Application.Context, items);
			// Assign adapter to ListView
			_tasksListView.Adapter = adapter;

		

			// ListView Item Click Listener
			_tasksListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
			{
				String selectedFromList = (string)_tasksListView.GetItemAtPosition(e.Position);
				Toast.MakeText(Application.Context, selectedFromList, ToastLength.Long).Show();
				ViewModel.AssignmentsPostionCommand?.Execute(e.Position);
			};
		}
	





		protected void Init()
		{
			//_itemNameEditText = View.FindViewById<EditText>(Resource.Id.FragmentTasksNameItemTextView);
			//_describtionEditText = View.FindViewById<EditText>(Resource.Id.FragmentTasksDescribtionTextView);
			//_createTaskButton = View.FindViewById<Button>(Resource.Id.FragmentTasksCreateTaskButton);
			_addTaskFloatingActionButton = View.FindViewById<FloatingActionButton>(Resource.Id.activityTasksAddTaskFloatingActionButton);
			_tasksListView = View.FindViewById<ListView>(Resource.Id.activityTasksTasksListView);
		}

		void SetBindings()
		{
			//this.SetBindings(() => ViewModel.Done, () => _tasksListView, BindingMode.TwoWay);
			//this.SetBinding(() => ViewModel.ItemName, () => _itemNameEditText.Text, BindingMode.TwoWay);
			//Binding itemPosition = this.SetBinding(() => ViewModel., () => _describtionEditText.Text, BindingMode.TwoWay);
		}
		void SetCommands()
		{
			//_createTaskButton.SetCommand("Click", ViewModel.CreateTaskAsyncCommand);
			//ViewModel.CreateTaskAsyncCommand
			_addTaskFloatingActionButton.SetCommand("Click", ViewModel.AddAssignmentRedirectCommand);
			//_tasksListView.SetCommand("Click", ViewModel.AssignmentDetailRedirectCommand); // Bind nog met item, list 


		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			return inflater.Inflate(Resource.Layout.FragmentTasks, container, false);
		}





		//          // ListView Item Click Listener
		//          listView.setOnItemClickListener(new OnItemClickListener()
		//{

		//	@Override

		//		  public void onItemClick(AdapterView<?> parent, View view,
		//			 int position, long id)
		//{

		//	// ListView Clicked item index
		//	int itemPosition = position;

		//	// ListView Clicked item value
		//	String itemValue = (String)listView.getItemAtPosition(position);

		//	// Show Alert 
		//	Toast.makeText(getApplicationContext(),
		//	  "Position :" + itemPosition + "  ListItem : " + itemValue, Toast.LENGTH_LONG)
		//	  .show();

	}
}



// Defined Array values to show in ListView
			//string[] values = new string[] { "Android List View",
			//								 "Adapter implementation",
			//								 "Simple List View In Android",
			//								 "Create List View Android",
			//								 "Android Example",
			//								 "List View Source Code",
			//								 "List View Array Adapter",
			//								 "Android Example List View"
			//								};

			// Define a new Adapter
			// First parameter - Context
			// Second parameter - Layout for the row
			// Third parameter - ID of the TextView to which the data is written
			// Forth - the Array of data

			//ArrayAdapter<string> adapter =
			//	new ArrayAdapter<string>(Application.Context, Android.Resource.Layout.SimpleListItem1, Android.Resource.Id.Text1, values);