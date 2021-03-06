﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact.Models;

namespace ComPact.WebServices
{
	public interface IBaseWebService<T>
	{
		Task<T> Create(string urlExtend, T obj);
		Task<T> Create(string urlExtend, IEnumerable<T> obj);
		Task<T> Read(string urlExtend);
		Task<IEnumerable<T>> ReadAll(string urlExtend);
		Task<T> Update(string urlExtend, T obj);
		Task<CallResult> Delete(string url);
	}
}
