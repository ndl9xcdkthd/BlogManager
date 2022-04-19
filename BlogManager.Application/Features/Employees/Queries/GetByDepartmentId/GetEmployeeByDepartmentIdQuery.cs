using AspNetCoreHero.Results;
using AutoMapper;
using BlogManager.Application.Interfaces.CacheRepositories;
using BlogManager.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlogManager.Application.Features.Employees.Queries.GetByDepartmentId
{
    public class GetEmployeeByDepartmentIdQuery : IRequest<Result<GetEmployeeByDepartmentIdResponse>>
    {
        public int DepartmentId { get; set; }

        public class GetEmployeeByDepartmentIdHandler : IRequestHandler<GetEmployeeByDepartmentIdQuery, Result<GetEmployeeByDepartmentIdResponse>>
        {
            private readonly IEmployeeCacheRepository _employeeCache;
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IMapper _mapper;
            public GetEmployeeByDepartmentIdHandler(IEmployeeCacheRepository employeeCacheRepository , IMapper mapper , IEmployeeRepository employeeRepository )
            {
                _employeeCache = employeeCacheRepository;
                _employeeRepository = employeeRepository;
                _mapper = mapper;
            }
            public async Task<Result<GetEmployeeByDepartmentIdResponse>> Handle(GetEmployeeByDepartmentIdQuery request, CancellationToken cancellationToken)
            {
                var employee = await _employeeRepository.GetByDepartmentIdAsync(request.DepartmentId);
                var mapperEmployee = _mapper.Map<GetEmployeeByDepartmentIdResponse>(employee);
                return Result<GetEmployeeByDepartmentIdResponse>.Success(mapperEmployee);
            }
        }
    }
}
