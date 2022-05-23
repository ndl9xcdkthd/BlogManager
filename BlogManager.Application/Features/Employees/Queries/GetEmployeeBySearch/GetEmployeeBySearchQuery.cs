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

namespace BlogManager.Application.Features.Employees.Queries.GetAllBySearch
{
    public class GetEmployeeBySearchQuery : IRequest<Result<List<GetEmployeeBySearchReponse>>>
    {

        public string SearchValue { get; set; }

        public class GetEmployeeBySearchQueryHanler : IRequestHandler<GetEmployeeBySearchQuery, Result<List<GetEmployeeBySearchReponse>>>
        {

            private readonly IEmployeeRepository _employeeRepository;
            private readonly IMapper _mapper;

            public GetEmployeeBySearchQueryHanler(IEmployeeRepository employeeRepository, IMapper mapper)
            {
                _employeeRepository = employeeRepository;
                _mapper = mapper;
            }


            public async Task<Result<List<GetEmployeeBySearchReponse>>> Handle(GetEmployeeBySearchQuery request, CancellationToken cancellationToken)
            {
                var employeeSearchList = await _employeeRepository.GetEmployeeBySearchAsync(request.SearchValue);

                var mappedEmployee = _mapper.Map<List<GetEmployeeBySearchReponse>>(employeeSearchList);

                return Result<List<GetEmployeeBySearchReponse>>.Success(mappedEmployee);
            }
        }
    }
}



