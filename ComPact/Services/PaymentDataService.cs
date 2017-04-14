﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComPact.Models;
using ComPact.WebServices;

namespace ComPact
{
	public class PaymentDataService : IPaymentDataService
	{
		readonly IApiService _apiService;
		readonly IPaymentRepository _paymentRepository;
		readonly IRepositoryMapper _mapper;

		public PaymentDataService(IApiService apiService, IPaymentRepository paymentRepository, IRepositoryMapper mapper)
		{
			_apiService = apiService;
			_paymentRepository = paymentRepository;
			_mapper = mapper;
		}

		public async Task<Payment> Create(Payment payment)
		{
			Payment response = await _apiService.AddPayment(payment);
			RepoPayment data = _mapper.InvertMap(response);
			await _paymentRepository.Insert(data);
			return response;
		}

		public async Task<bool> Delete(string id)
		{
			bool isSuccessful = await _apiService.DeletePayment(id);
			if (isSuccessful)
			{
				IEnumerable<RepoPayment> response = await _paymentRepository.Where((x => x.Id == id));
				await _paymentRepository.Delete(response.FirstOrDefault());
				isSuccessful = true;
			}
			return isSuccessful;
		}

		public async Task<IEnumerable<Payment>> GetAll()
		{
			return _mapper.Map(await _paymentRepository?.All());
		}

		public async Task<IEnumerable<Payment>> GetAll(string userId, bool isAdmin)
		{
			IEnumerable<Payment> response = await _apiService.GetPayments(userId, isAdmin);
			IEnumerable<RepoPayment> data = _mapper.InvertMap(response);
			await _paymentRepository.Insert(data);
			return await GetAll();
		}

		public async Task LogOut()
		{
			await _paymentRepository.Delete(await _paymentRepository?.All());
		}

		public Task<Payment> Update(Payment payment)
		{
			throw new NotImplementedException();
		}
	}
}
