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
            private readonly IUnitOfWork _unitOfWork;

            public DeleteDepartmentCommandHander(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
            {
                _departmentRepository = departmentRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
            {
                var department = await _departmentRepository.GetByIdAsync(request.Id);
                await _departmentRepository.DeleteAsync(department);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(department.Id);
            }
        }
    }
}
