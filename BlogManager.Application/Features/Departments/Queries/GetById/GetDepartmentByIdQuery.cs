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

namespace BlogManager.Application.Features.Departments.Queries.GetById
{
    public class GetDepartmentByIdQuery : IRequest<Result<GetDepartmentByIdResponse>>
    {
        public int Id { get; set; }
        public GetDepartmentByIdQuery(int id)
        {
            Id = id;
        }
        public GetDepartmentByIdQuery()
        {

        }

        public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, Result<GetDepartmentByIdResponse>>
        {
            private readonly IDepartmentCacheRepository _departmentCache;
            private readonly IMapper _mapper;

            public GetDepartmentByIdQueryHandler(IDepartmentCacheRepository departmentCache, IMapper mapper)
            {
                _departmentCache = departmentCache;
                _mapper = mapper;
            }

            public async Task<Result<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
            {
                var department = await _departmentCache.GetByIdAsync(request.Id);
                var mappedProduct = _mapper.Map<GetDepartmentByIdResponse>(department);
                return Result<GetDepartmentByIdResponse>.Success(mappedProduct);
            }
        }
    }
}
