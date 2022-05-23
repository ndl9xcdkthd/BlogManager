using AspNetCoreHero.Results;
using AutoMapper;
using BlogManager.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlogManager.Application.Features.Employees.Queries.GetEmployeeOrderby
{
    public class GetEmployeeOrderByQuery : IRequest<Result<List<GetEmployeeOrderByRepone>>>
    {
        public string SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public class GetEmployeeOrderByQueryHandler : IRequestHandler<GetEmployeeOrderByQuery, Result<List<GetEmployeeOrderByRepone>>>
        {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IMapper _mapper;
            public GetEmployeeOrderByQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
            {
                _employeeRepository = employeeRepository;
                _mapper = mapper;
            }
            public Task<Result<List<GetEmployeeOrderByRepone>>> Handle(GetEmployeeOrderByQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
