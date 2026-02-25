using MediatR;
using Redarbor.Application.DTOs;
using Redarbor.Domain.Interfaces;

namespace Redarbor.Application.Queries.GetEmployeeById;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
{
    private readonly IEmployeeReadRepository _readRepository;

    public GetEmployeeByIdQueryHandler(IEmployeeReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<EmployeeDto?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _readRepository.GetByIdAsync(request.Id);

        if (employee is null) return null;

        return new EmployeeDto
        {
            Id         = employee.Id,
            CompanyId  = employee.CompanyId,
            CreatedOn  = employee.CreatedOn,
            DeletedOn  = employee.DeletedOn,
            Email      = employee.Email,
            Fax        = employee.Fax,
            Name       = employee.Name,
            LastLogin  = employee.LastLogin,
            Password   = employee.Password,
            PortalId   = employee.PortalId,
            RoleId     = employee.RoleId,
            StatusId   = employee.StatusId,
            Telephone  = employee.Telephone,
            UpdatedOn  = employee.UpdatedOn,
            Username   = employee.Username
        };
    }
}