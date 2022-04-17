using AspNetCoreHero.Results;
using AutoMapper;
using BlogManager.Application.Interfaces.CacheRepositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlogManager.Application.Features.Employees.Queries.GetAllCached
{
    public class GetAllEmployeeCachedQuery : IRequest<Result<List<GetAllEmployeeCachedResponse>>>
    {
        public GetAllEmployeeCachedQuery()
        {

        }

        public class GetAllEmployeeCachedQueryHandler : IRequestHandler<GetAllEmployeeCachedQuery, Result<List<GetAllEmployeeCachedResponse>>>
        {
            private readonly IEmployeeCacheRepository _employeeCache;
            private readonly IMapper _mapper;
            public GetAllEmployeeCachedQueryHandler(IEmployeeCacheRepository employeeCache, IMapper mapper)
            {
                _employeeCache = employeeCache;
                _mapper = mapper;
            }
            public async Task<Result<List<GetAllEmployeeCachedResponse>>> Handle(GetAllEmployeeCachedQuery request, CancellationToken cancellationToken)
            {
                var employeeList = await _employeeCache.GetCachedListAsync();
                var mappedEmployee = _mapper.Map<List<GetAllEmployeeCachedResponse>>(employeeList);
                return Result<List<GetAllEmployeeCachedResponse>>.Success(mappedEmployee);
            }
        }
    } 
}
