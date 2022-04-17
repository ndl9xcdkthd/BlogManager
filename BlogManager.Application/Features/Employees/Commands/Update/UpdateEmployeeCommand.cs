using AspNetCoreHero.Results;
using BlogManager.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlogManager.Application.Features.Employees.Commands.Update
{
    public class UpdateEmployeeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public DateTime HireDate { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public int DepartmentId { get; set; }

        public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IEmployeeRepository _employeeRepository;

            public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
            {
                _employeeRepository = employeeRepository;
                _unitOfWork = unitOfWork;
            }
            public async Task<Result<int>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
            {
                var employee = await _employeeRepository.GetByIdAsync(request.Id);

                if (employee == null)
                {
                    return Result<int>.Fail($"Department Not Found.");
                }
                else
                {
                    employee.FristName = request.FristName;
                    employee.LastName = request.LastName;
                    employee.Status = request.Status;
                    employee.DateOfBirth = request.DateOfBirth;
                    employee.Gender = request.Gender;
                    employee.Position = request.Position;
                    employee.HireDate = request.HireDate;
                    employee.DepartmentId = request.DepartmentId;
                    await _employeeRepository.UpdateAsync(employee);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(employee.Id);
                }
            }
        }
    }
}
