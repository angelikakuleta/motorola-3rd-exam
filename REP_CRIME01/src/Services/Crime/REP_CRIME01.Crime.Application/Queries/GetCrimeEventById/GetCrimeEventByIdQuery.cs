﻿using MediatR;
using REP_CRIME01.Crime.Application.Models;
using REP_CRIME01.Crime.Application.Responses;
using System;

namespace REP_CRIME01.Crime.Application.Queries.GetCrimeEventById
{
    public static partial class GetCrimeEventById
    {
        public record Query : IRequest<Response>
        {
            public Guid EventId { get; set; }
        }

        public record Response : BaseResponse<CrimeEventVM> { }
    }
}
