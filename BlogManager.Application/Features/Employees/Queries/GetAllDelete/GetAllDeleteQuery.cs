using AspNetCoreHero.Results;
using AutoMapper;
using BlogManager.Application.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlogManager.Application.Features.Employees.Queries.GetAllDelete
{
    public class GetAllDeleteQuery : IRequest<Result<List<GetAllDeleteReponse>>>
    {
        public GetAllDeleteQuery()
        {

        }

        public class GetAllDeleteQueryHandler : IRequestHandler<GetAllDeleteQuery, Result<List<GetAllDeleteReponse>>>
        {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IMapper _mapper;
            public GetAllDeleteQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
            {
                _employeeRepository = employeeRepository;
                _mapper = mapper;
            }
            public async Task<Result<List<GetAllDeleteReponse>>> Handle(GetAllDeleteQuery request, CancellationToken cancellationToken)
            {
                var employeeDetelete = await _employeeRepository.GetDeleteListAsync();
                var mappedEmployee = _mapper.Map<List<GetAllDeleteReponse>>(employeeDetelete);
                return Result<List<GetAllDeleteReponse>>.Success(mappedEmployee);
            }
        }
    }
}
