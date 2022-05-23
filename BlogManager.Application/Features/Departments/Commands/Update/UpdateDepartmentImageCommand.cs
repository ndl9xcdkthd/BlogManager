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
    public class UpdateDepartmentImageCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Image { get; set; }

        public class UpdateDepartmentImageCommandHandler : IRequestHandler<UpdateDepartmentImageCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IDepartmentRepository _departmentRepository;
            public UpdateDepartmentImageCommandHandler(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _departmentRepository = departmentRepository;
            }
            public async Task<Result<int>> Handle(UpdateDepartmentImageCommand request, CancellationToken cancellationToken)
            {
                var department = await _departmentRepository.GetByIdAsync(request.Id);

                department.Image = request.Image;

                await _departmentRepository.UpdateAsync(department);
                await _unitOfWork.Commit(cancellationToken);

                return Result<int>.Success(department.Id);
            }
        }
    }
}
