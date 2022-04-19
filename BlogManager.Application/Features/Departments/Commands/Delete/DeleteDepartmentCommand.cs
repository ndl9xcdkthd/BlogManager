using AspNetCoreHero.Results;
using BlogManager.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlogManager.Application.Features.Departments.Commands.Delete
{
    public partial class DeleteDepartmentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteDepartmentCommandHander : IRequestHandler<DeleteDepartmentCommand, Result<int>>
        {
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteDepartmentCommandHander(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork,IEmployeeRepository employeeRepository)
            {
                _employeeRepository = employeeRepository;
                _departmentRepository = departmentRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
            {
                var department = await _departmentRepository.GetByIdAsync(request.Id);
                var getEmployee = await _employeeRepository.GetByDepartmentIdAsync(request.Id);
                if (getEmployee.Count > 0)
                {
                    foreach(var employee in getEmployee)
                    {
                        await _employeeRepository.DeleteAsync(employee);
                    }
                }
                await _departmentRepository.DeleteAsync(department);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(department.Id);
            }
        }
    }
}
