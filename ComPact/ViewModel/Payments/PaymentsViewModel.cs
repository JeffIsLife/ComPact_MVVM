﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ComPact.Models;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.Payments
{
public class PaymentsViewModel : BaseViewModel
{
	readonly INavigationService _navigationService;
	readonly IPaymentDataService _paymentDataService;

	#region Parameters
	/**
	 * Parameters
	 */
	ObservableCollection<Payment> _payments = new ObservableCollection<Payment>();
	public ObservableCollection<Payment> Payments
	{
		get
		{
			return _payments;
		}
		set
		{
			_payments = value;
			RaisePropertyChanged(nameof(Payments));
		}
	}
	#endregion
	#region Commands
	public RelayCommand AddPaymentRedirectCommand { get; set; }
	public RelayCommand LoadDataCommand { get; set; }
	#endregion
	#region Constructor
	/**
	 * Init services & Init() & RegisterCommands();
	 */
	public PaymentsViewModel(INavigationService navigationService, IUserDataService userDataService, IPaymentDataService paymentDataService)
		: base(userDataService)
	{
		//Init Services
		_navigationService = navigationService;
		_paymentDataService = paymentDataService;

		Init();

		RegisterCommands();
	}

	void Init()
	{
		//Register values
	}
	void RegisterCommands()
	{
		AddPaymentRedirectCommand = new RelayCommand(PaymentRedirect);
		LoadDataCommand = new RelayCommand(async () =>
		{
			await LoadData();
		});
	}


	#endregion

	#region Methods
	void PaymentRedirect()
	{
		_navigationService.NavigateTo(LocatorViewModel.AddPaymentPageKey);
	}

	async Task LoadData()
	{
		Payments = Convert(await _paymentDataService.GetAll());
	}
		#endregion
	}
}