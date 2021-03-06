﻿using System;
using System.Collections.Generic;
using ComPact.Models;
using ComPact.WebServices.Models;

namespace ComPact
{
	public interface IWebMapper
	{
		Assignment Map(WebAssignment assignment);
		WebAssignment InvertMap(Assignment assignment);
		IEnumerable<Assignment> Map(IEnumerable<WebAssignment> assignments);
		IEnumerable<WebAssignment> InvertMap(IEnumerable<Assignment> assignments);


		User Map(WebUser user);
		WebUser InvertMap(User user);

		Member Map(WebMember member);
		WebMember InvertMap(Member member);
		IEnumerable<Member> Map(IEnumerable<WebMember> members);
		IEnumerable<WebMember> InvertMap(IEnumerable<Member> members);

		Payment Map(WebPayment payment);
		WebPayment InvertMap(Payment payment);
		IEnumerable<Payment> Map(IEnumerable<WebPayment> payments);
		IEnumerable<WebPayment> InvertMap(IEnumerable<Payment> payments);

	}
}
