using AspNetCoreHero.Results;
using BlogManager.Application.Extensions;
using BlogManager.Application.Interfaces.Repositories;
using BlogManager.Domain.Entities.Catalog;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlogManager.Application.Features.Departments.Queries.GetAllPaged
{
    public class GetAllDepartmentQuery : IRequest<PaginatedResult<GetAllDepartmentRepose>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllDepartmentQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetAllDepartmentQueryHandler : IRequestHandler<GetAllDepartmentQuery, PaginatedResult<GetAllDepartmentRepose>>
    {
        private readonly IDepartmentRepository _repository;
        public GetAllDepartmentQueryHandler(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<PaginatedResult<GetAllDepartmentRepose>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Department, GetAllDepartmentRepose>> expression = e => new GetAllDepartmentRepose 
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Status = e.Status,
            };

            var paginatedList = await _repository.Departments
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber,request.PageSize);
            return paginatedList;
        }
    }
}
