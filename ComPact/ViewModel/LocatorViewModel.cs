﻿using System;
using System.Diagnostics;
using ComPact;
using ComPact.Helpers;
using ComPact.Members;
using ComPact.Repositories;
using ComPact.Services;
using ComPact.ViewModel;
using ComPact.ViewModel.Members;
using ComPact.WebServices;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace ComPact
{
	public class LocatorViewModel
	{
		/**
		 * Set a key for each page => App.cs
		 */
		public const string SplashPageKey = "SplashPageKey";
		public const string LoginPageKey = "LoginPage";
		public const string LoginQrPageKey = "LoginQrPage";
		public const string RegisterPageKey = "RegisterPage";
		public const string PasswordRetrievalPageKey = "PasswordRetrievalPage";
		public const string HomePageKey = "HomePagekey";
		public const string HelpPageKey = "HelpPageKey";
		public const string SettingsPageKey = "SettingsPageKey";
		public const string TasksPageKey = "TasksPageKey";
		public const string AddTaskPageKey = "AddTaskPageKey";
		public const string MembersPageKey = "MembersPageKey";
		public const string AddMembersPageKey = "AddMembersPagekey";

		public LoginViewModel LoginViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<LoginViewModel>();
			}
		}

		public LoginQrViewModel LoginQrViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<LoginQrViewModel>();
			}
		}

		public RegisterViewModel RegisterViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<RegisterViewModel>();
			}
		}

		public PasswordRetrievalViewModel PasswordRetrievalViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<PasswordRetrievalViewModel>();
			}
		}

		public HomeViewModel HomeViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<HomeViewModel>();
			}
		}

		public HelpViewModel HelpViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<HelpViewModel>();
			}
		}

		public SettingsViewModel SettingsViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SettingsViewModel>();
			}
		}

		public TasksViewModel TasksViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<TasksViewModel>();
			}
		}
		public AddTaskViewModel AddTaskViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<AddTaskViewModel>();
			}
		}

		public SplashViewModel SplashViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SplashViewModel>();
			}
		}
		public MembersViewModel MembersViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<MembersViewModel>();
			}
		}
		public AddMembersViewModel AddMembersViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<AddMembersViewModel>();
			}
		}
		/**
		 * Register every ViewModel to the IOC container
		 */
		public LocatorViewModel()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
			RegisterRepositories();
			RegisterWebServices();
			RegisterViewModels();
			RegisterServices();
		}

		void RegisterViewModels() 
		{
			SimpleIoc.Default.Register<SplashViewModel>();
			SimpleIoc.Default.Register<LoginViewModel>();
			SimpleIoc.Default.Register<LoginQrViewModel>();
			SimpleIoc.Default.Register<RegisterViewModel>();
			SimpleIoc.Default.Register<PasswordRetrievalViewModel>();
			SimpleIoc.Default.Register<HomeViewModel>();
			SimpleIoc.Default.Register<HelpViewModel>();
			SimpleIoc.Default.Register<SettingsViewModel>();

			SimpleIoc.Default.Register<TasksViewModel>();
			SimpleIoc.Default.Register<AddTaskViewModel>();

			SimpleIoc.Default.Register<MembersViewModel>();
			SimpleIoc.Default.Register<AddMembersViewModel>();
		}

		void RegisterServices()
		{
			SimpleIoc.Default.Register<IUserDataService, UserDataService>();
			SimpleIoc.Default.Register<ITaskDataService, TaskDataService>();
			SimpleIoc.Default.Register<IAuthenticationService, AuthenticationService>();
		}
		void RegisterWebServices()
		{
			SimpleIoc.Default.Register<IUserWebservice, UserWebservice>();
			SimpleIoc.Default.Register<ITaskWebService, TaskWebService>();
			SimpleIoc.Default.Register<IPersonalUserWebService, PersonalUserWebService>();
		}
		void RegisterRepositories()
		{
			SimpleIoc.Default.Register<IUserRepository, UserRepository>();
			SimpleIoc.Default.Register<IPersonalRepository, PersonalRepository>();
		}
	}
}
