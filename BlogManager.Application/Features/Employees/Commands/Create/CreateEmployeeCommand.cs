using BlogManager.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BlogManager.Domain.Entities.Catalog;
using System;

namespace BlogManager.Application.Features.Employees.Commands.Create
{

    public partial class CreateEmployeeCommand : IRequest<Result<int>>
    {
        public string FristName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public DateTime HireDate { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public int DepartmentId { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<int>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(request);
            await _employeeRepository.InsertAsync(employee);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(employee.Id);
        }
    }
}
