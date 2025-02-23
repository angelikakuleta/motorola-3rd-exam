﻿using REP_CRIME01.Police.Common.Models;
using System;
using System.Threading.Tasks;

namespace REP_CRIME01.Crime.Infrastructure.Clients
{
    public interface IPoliceClient
    {
        Task<Guid?> CreateCase(CreateCaseDto dto);
    }
}
