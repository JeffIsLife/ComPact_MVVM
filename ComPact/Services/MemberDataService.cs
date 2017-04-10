﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ComPact.Models;
using ComPact.WebServices;

namespace ComPact
{
	public class MemberDataService: IMemberDataService
	{
		private readonly IApiService _apiService;
		private readonly IMemberRepository _memberRepository;
		readonly IRepositoryMapper _mapper;

		public MemberDataService(IApiService apiService, IMemberRepository memberRepository, IRepositoryMapper mapper)
		{
			_apiService = apiService;
			_memberRepository = memberRepository;
			_mapper = mapper;
		}

		public async Task<Member> Get(string memberId)
		{
			return _mapper.Map(await _memberRepository.Get(memberId));
		}
		/**
		 * Web call and save in localstorage
		 */
		public async Task<IEnumerable<Member>> GetAll(string adminId)
		{
			IEnumerable<Member> webResponse = await _apiService.GetMembers(adminId);
			IEnumerable<RepoMember> members = _mapper.InvertMap(webResponse);

			await _memberRepository.Insert(members);

			return await GetAll();
		}
		/**
		 * Local storage call
		 */
		public async Task<IEnumerable<Member>> GetAll()
		{
			return _mapper.Map(await _memberRepository?.All());
		}
		public async Task LogOut()
		{
			await _memberRepository?.Delete(await _memberRepository?.All());
		}



		/**
		 * Create User
		 */
		//public async Task<Member> Create(Member user)
		//{
		//	return await _memberWebservice.Create(APICalls.CreateAuthPath, user);
		//}
		//public async Task<Member> Get(string email)
		//{
		//	string getUserPathEmailPassword = "/api/users?email=\"" + email + "\"";
		//	try
		//	{
		//		Member member = await _apiService.
		//		return member;
		//	}
		//	catch (Exception)
		//	{
		//		throw new Exception("Can't connect to server. Please check your network.");
		//	}
		//}

		//public async Task<IEnumerable<Member>> Save(IEnumerable<Member> members)
		//{
		//	//try
		//	//{
		//	//	await _memberRepository.Insert(members);

		//	//}
		//	//catch (Exception ex)
		//	//{
		//	//	Debug.WriteLine(ex);
		//	//}
		//	//return await GetAll();
		//	throw new NotImplementedException();
		//}
		//public async Task<IEnumerable<Member>> GetAll()
		//{
		//	throw new NotImplementedException();
		//	//return await _memberRepository.All();
		//}

		//public void Forgot(Member user)
		//{
		//	_memberWebservice.Forgot("/api/users/forgot", user);
		//}

	}
}
	
		