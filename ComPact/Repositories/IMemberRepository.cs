﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact.Repositories;

namespace ComPact
{
	public interface IMemberRepository: IBaseRepository<Member, string>
	{
	}
}