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

            public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _departmentRepository = departmentRepository;
            }

            public async Task<Result<int>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
            {
                var department = await _departmentRepository.GetByIdAsync(request.Id);

                if (department == null)
                {
                    return Result<int>.Fail($"Product not found !!!");
                }
                else
                {
                    department.Name = request.Name;
                    department.Description = request.Description;
                    department.Status = request.Status;

                    await _departmentRepository.UpdateAsync(department);
                    await _unitOfWork.Commit(cancellationToken);
                    
                    return Result<int>.Success(department.Id);
                }
            }
        }
    }
}
