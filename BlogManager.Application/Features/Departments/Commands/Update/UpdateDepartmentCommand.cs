using AspNetCoreHero.Results;
using BlogManager.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlogManager.Application.Features.Departments.Commands.Update
{
    public class UpdateDepartmentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IEmployeeRepository _employeeRepository;

            public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository)
            {
                _unitOfWork = unitOfWork;
                _departmentRepository = departmentRepository;
                _employeeRepository = employeeRepository;

            }

            public async Task<Result<int>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
            {
                var department = await _departmentRepository.GetByIdAsync(request.Id);

                var employeeList = await _employeeRepository.GetByDepartmentIdAsync(department.Id);

                if (department == null)
                {
                    return Result<int>.Fail($"Product not found !!!");
                }
                else
                {
                    department.Name = request.Name;
                    department.Description = request.Description;
                    if(department.Status == "Delete")
                    {
                        department.Status = request.Status;

                        if (employeeList.Count > 0)
                        {
                            if (department.Status == "Active" || department.Status == "In Active")
                            {
                                foreach (var employee in employeeList)
                                {
                                    if (employee.Status == "Delete")
                                    {
                                        employee.Status = "Active";
                                    }
                                } 
                            }
                        }
                    }
                    else
                    {
                        department.Status = request.Status;
                    }

                    await _departmentRepository.UpdateAsync(department);
                    await _unitOfWork.Commit(cancellationToken);
                    
                    return Result<int>.Success(department.Id);
                }
            }
        }
    }
}
