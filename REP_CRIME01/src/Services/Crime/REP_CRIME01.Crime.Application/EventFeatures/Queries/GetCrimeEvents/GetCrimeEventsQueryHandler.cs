﻿using AutoMapper;
using MediatR;
using REP_CRIME01.Crime.Common.Models;
using REP_CRIME01.Crime.Domain.Contracts;
using REP_CRIME01.Crime.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace REP_CRIME01.Crime.Application.EventFeatures.Queries
{

    public static partial class GetCrimeEvents
    {
        public record Handler : IRequestHandler<Query, Response>
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
                var qs = request.CrimeEventsQueryString;
                Expression<Func<CrimeEvent, bool>> filterExpression = string.IsNullOrEmpty(qs.SearchPhrase) 
                    ? x => 1 == 1
                    : x => x.EventType.ToLower().Contains(qs.SearchPhrase.ToLower()) || x.Description.ToLower().Contains(qs.SearchPhrase.ToLower());

                Expression<Func<CrimeEvent, object>> sortBy = x => x.EventDate;

                bool sortDesc = string.IsNullOrEmpty(qs.OrderBy) || qs.OrderBy.ToLower().Contains("desc");
                int pageIndex = qs.PageIndex;
                int pageSize = qs.PageSize;

                var entities = await _repository.FindAllAsync(filterExpression, sortBy, sortDesc, pageIndex, pageSize);
                var count = await _repository.CountAsync(filterExpression);
                var items = _mapper.Map<List<CrimeEventVM>>(entities);
                
                return new Response { Result = new PaginatedResult<CrimeEventVM>(items, count, pageIndex, pageSize) };
            }
        }
    }
}
