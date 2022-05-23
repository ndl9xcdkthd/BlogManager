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

namespace BlogManager.Application.Features.Employees.Queries.GetAllActive
{
    public class GetAllActiveQuery : IRequest<Result<List<GetAllActiveRpsponse>>>
    {
        public GetAllActiveQuery()
        {
           
        }
        public class GetAllActiveQueryHandler : IRequestHandler<GetAllActiveQuery, Result<List<GetAllActiveRpsponse>>>
        {
            private readonly IEmployeeRepository _employee;
            private readonly IMapper _mapper;
            public GetAllActiveQueryHandler(IEmployeeRepository employee, IMapper mapper)
            {
                _employee = employee;
                _mapper = mapper;
            }
            public async Task<Result<List<GetAllActiveRpsponse>>> Handle(GetAllActiveQuery request, CancellationToken cancellationToken)
            {
                var employeeActiveList = await _employee.GetActiveListAsync();
                var mappedEmployee = _mapper.Map<List<GetAllActiveRpsponse>>(employeeActiveList);
                return Result<List<GetAllActiveRpsponse>>.Success(mappedEmployee);
            }
        }
    }
}
