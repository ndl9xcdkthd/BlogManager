using AspNetCoreHero.Results;
using AutoMapper;
using BlogManager.Application.Interfaces.Repositories;
using BlogManager.Domain.Entities.Catalog;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlogManager.Application.Features.Departments.Commands.Create
{
    public partial class CreateDepartmentCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

    }

    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result<int>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public IUnitOfWork _unitOfWork { get; set; }

        public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {

            var department = _mapper.Map<Department>(request);
            await _departmentRepository.InserAsync(department);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(department.Id);
        }
    }
}
