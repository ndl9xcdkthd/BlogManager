using AspNetCoreHero.Results;
using AutoMapper;
using BlogManager.Application.Extensions;
using BlogManager.Application.Interfaces.CacheRepositories;
using BlogManager.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlogManager.Application.Features.Employees.Queries.GetAllJs
{
    public class GetAllJsQuery : IRequest<Tuple<List<GetAllJsRepose>,int>>
    {
        public DataTableAjaxPostModel model;
        public GetAllJsQuery()
        {

        }

        public class GetAllJsQueryHanler : IRequestHandler<GetAllJsQuery, Tuple<List<GetAllJsRepose>, int>>
        {
            private readonly IEmployeeCacheRepository _employeeCache;
            private readonly IEmployeeRepository _employee;
            private readonly IMapper _mapper;
            public GetAllJsQueryHanler(IEmployeeCacheRepository employeeCache, IMapper mapper , IEmployeeRepository employee)
            {
                _employee = employee;
                _employeeCache = employeeCache;
                _mapper = mapper;
            }
            public async Task<Tuple<List<GetAllJsRepose>, int>> Handle(GetAllJsQuery request, CancellationToken cancellationToken)
            {
                var employeeListPage = await _employeeCache.GetCachedListPageAsync(request.model);
                var mappedEmployeePage = _mapper.Map<List<GetAllJsRepose>>(employeeListPage);

                var employeeList = await _employee.GetListAsync();
                var mappedEmployee = _mapper.Map<List<GetAllJsRepose>>(employeeList);
                var count = mappedEmployee.Count;

                return new Tuple<List<GetAllJsRepose>, int>(mappedEmployeePage, count);
            }
        }
    }
}
