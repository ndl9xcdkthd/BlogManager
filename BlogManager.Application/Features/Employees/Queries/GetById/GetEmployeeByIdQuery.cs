using AspNetCoreHero.Results;
using AutoMapper;
using BlogManager.Application.Interfaces.CacheRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlogManager.Application.Features.Employees.Queries.GetById
{
    public class GetEmployeeByIdQuery : IRequest<Result<GetEmployeeByIdResponse>>
    {
        public int Id { get; set; }

        public class GetEmployeeByIdQueryByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Result<GetEmployeeByIdResponse>>
        {
            private readonly IEmployeeCacheRepository _employeeCache;
            private readonly IMapper _mapper;

            public GetEmployeeByIdQueryByIdQueryHandler(IEmployeeCacheRepository employeeCacheRepository , IMapper mapper)
            {
                _employeeCache = employeeCacheRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetEmployeeByIdResponse>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
            {
                var employee = await _employeeCache.GetByIdAsync(request.Id);
                var mappedEmployee = _mapper.Map<GetEmployeeByIdResponse>(employee);
                return Result<GetEmployeeByIdResponse>.Success(mappedEmployee);
            }
        }
    }
}
