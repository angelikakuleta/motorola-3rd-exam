﻿using AutoMapper;
using MediatR;
using REP_CRIME01.CQRSResponse.Responses;
using REP_CRIME01.Crime.Common.Models;
using REP_CRIME01.Crime.Domain.Contracts;
using REP_CRIME01.Crime.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace REP_CRIME01.Crime.Application.EventFeatures.Queries
{
    public static partial class GetCrimeEventById
    {
        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IRepository<CrimeEvent> _repository;
            private readonly IMapper _mapper;

            public Handler(IMapper mapper, IRepository<CrimeEvent> repository)
            {
                _mapper = mapper;
                _repository = repository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _repository.FindByIdAsync(request.Id);
                if (result is null)
                {
                    return new Response { Status = ResponseStatus.NotFound };
                }                   

                var vm = _mapper.Map<CrimeEventVM>(result);
                return new Response { Result = vm };
            }
        }
    }
}
