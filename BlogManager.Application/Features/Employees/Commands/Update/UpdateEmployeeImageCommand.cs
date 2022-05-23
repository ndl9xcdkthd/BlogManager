using AspNetCoreHero.Results;
using BlogManager.Application.Exceptions;
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
    public class UpdateEmployeeImageCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }

        public class UpadteEmpoyeeImageCommandHandler : IRequestHandler<UpdateEmployeeImageCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IEmployeeRepository _employeeRepository;
            public UpadteEmpoyeeImageCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _employeeRepository = employeeRepository;
            }
            public async Task<Result<int>> Handle(UpdateEmployeeImageCommand command, CancellationToken cancellationToken)
            {
                var employee = await _employeeRepository.GetByIdAsync(command.Id);

                if(employee == null)
                {
                    throw new ApiException($"Employee Not Found.");
                }
                else
                {
                    employee.Image = command.Image;
                    await _employeeRepository.UpdateAsync(employee);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(employee.Id);
                }
            }
        }
    }
}
