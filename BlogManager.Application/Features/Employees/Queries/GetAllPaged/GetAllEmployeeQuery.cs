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

namespace BlogManager.Application.Features.Employees.Queries.GetAllPaged
{
    public class GetAllEmployeeQuery : IRequest<PaginatedResult<GetAllEmployeeResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllEmployeeQuery(int pageNumber , int pagSize )
        {
            PageNumber = pageNumber;
            PageSize = pagSize;
        }
    }

    public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, PaginatedResult<GetAllEmployeeResponse>>
    {
        private readonly IEmployeeRepository _repository;

        public GetAllEmployeeQueryHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllEmployeeResponse>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Employee, GetAllEmployeeResponse>> expression = e => new GetAllEmployeeResponse
            {
                Id = e.Id,
                FristName = e.FristName,
                LastName = e.LastName,
                DateOfBirth = e.DateOfBirth,
                DepartmentId = e.DepartmentId,
                Gender = e.Gender,
                HireDate = e.HireDate,
                Position = e.Position,
                Status = e.Status,
            };

            var paginatedList = await _repository.Employees
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}
