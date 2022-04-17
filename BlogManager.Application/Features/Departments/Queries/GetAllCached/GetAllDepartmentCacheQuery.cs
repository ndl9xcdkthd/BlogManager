using AspNetCoreHero.Results;
using AutoMapper;
using BlogManager.Application.Interfaces.CacheRepositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlogManager.Application.Features.Departments.Queries.GetAllCached
{
    public class GetAllDepartmentCacheQuery : IRequest<Result<List<GetAllDepartmentCacheRepose>>>
    {
        public GetAllDepartmentCacheQuery()
        {

        }

        public class GetAllDepartmentCacheQueryHandler : IRequestHandler<GetAllDepartmentCacheQuery, Result<List<GetAllDepartmentCacheRepose>>>
        {
            private readonly IDepartmentCacheRepository _departmentCache;
            private readonly IMapper _mapper;
            public GetAllDepartmentCacheQueryHandler(IDepartmentCacheRepository departmentCache, IMapper mapper)
            {
                _departmentCache = departmentCache;
                _mapper = mapper;
            }
            public async Task<Result<List<GetAllDepartmentCacheRepose>>> Handle(GetAllDepartmentCacheQuery request, CancellationToken cancellationToken)
            {
                var departmentList = await _departmentCache.GetCachedListAsync();
                var mappedDepartment = _mapper.Map<List<GetAllDepartmentCacheRepose>>(departmentList);
                return Result<List<GetAllDepartmentCacheRepose>>.Success(mappedDepartment);
            }
        }
    }
}
