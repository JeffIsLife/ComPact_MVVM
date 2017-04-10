﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact.Models;
using ComPact.WebServices.Data;
using ComPact.WebServices.Models;

namespace ComPact.WebServices
{
	public class ApiService : IApiService
	{
		IWebMapper _mapper;
		IAssignmentWebService _assignmentWebService;
		IUserWebService _userWebService;
		IMemberWebService _memberWebService;

		public ApiService(IWebMapper mapper, IAssignmentWebService assignmentWebService, IUserWebService userWebService, IMemberWebService memberWebService)
		{
			_mapper = mapper;
			_assignmentWebService = assignmentWebService;
			_userWebService = userWebService;
			_memberWebService = memberWebService;
		}

		public async Task<User> AddUser(User user)
		{
			WebUser data = _mapper.InvertMap(user);
			WebUser response = await _userWebService.Create(ApiCalls.CreateUserPath, data);
			return _mapper.Map(response);
		}

		public async Task<User> LoginUser(User user)
		{
			WebUser data = _mapper.InvertMap(user);
			WebUser response = await _userWebService.Create(ApiCalls.LoginAuthPath, data);
			return _mapper.Map(response);
		}

		public async Task<User> GetUser(string email)
		{
			string url = ApiCalls.BaseAuthPath + String.Format("?email={0}", email);
			try
			{
				WebUser response = await _userWebService.Read(url);
				return _mapper.Map(response);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<Assignment> AddAssignment(Assignment assignment)
		{
			WebAssignment data = _mapper.InvertMap(assignment);
			WebAssignment response = await _assignmentWebService.Create(ApiCalls.CreateAssignmentPath, data);
			return _mapper.Map(response);
		}


		public async Task<IEnumerable<Member>> GetMembers(string adminId)
		{
			string url = ApiCalls.BaseUserPath + String.Format("?adminId={0}", adminId);
			IEnumerable<WebMember> response = await _memberWebService.ReadAll(url);
			return _mapper.Map(response);
			
		}

		public Task<Member> GetMember(string adminId)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Assignment>> GetAssignments(string userId, bool isAdmin)
		{
			string url;
			if (isAdmin)
			{
				url = ApiCalls.BaseAssignemntPath + String.Format("?adminId={0}", userId);
			}
			else
			{
				url = ApiCalls.BaseAssignemntPath + String.Format("?memberId={0}", userId);
			}
			IEnumerable<WebAssignment> response = await _assignmentWebService.ReadAll(url);
			return _mapper.Map(response);
		}

		public async Task<Assignment> UpdateAssignment(Assignment assignment)
		{
			string url = ApiCalls.BaseAssignemntPath + String.Format("/{0}", assignment.Id);
			assignment.Id = null;
			WebAssignment data = _mapper.InvertMap(assignment);
			WebAssignment response = await _assignmentWebService.Update(url, data);
			return _mapper.Map(response);
		}
	}
}
