﻿using AspNetCoreHero.Results;
using AutoMapper;
using BlogManager.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlogManager.Application.Features.Employees.Commands.Delete
{
    public class DeleteEmployeeSqlCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteEmployeeSqlCommandHandler : IRequestHandler<DeleteEmployeeSqlCommand, Result<int>>
        {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IUnitOfWork _unitOfWork;
            public DeleteEmployeeSqlCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
            {
                _employeeRepository = employeeRepository ;
                _unitOfWork = unitOfWork;
            }
            public async Task<Result<int>> Handle(DeleteEmployeeSqlCommand request, CancellationToken cancellationToken)
            {
                var employee = await _employeeRepository.GetByIdAsync(request.Id);
                await _employeeRepository.DeleteAsync(employee);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(employee.Id);
            }
        }
    }
}
