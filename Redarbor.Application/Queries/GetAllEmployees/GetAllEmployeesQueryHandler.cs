using MediatR;
using Redarbor.Application.DTOs;
using Redarbor.Domain.Interfaces;

namespace Redarbor.Application.Queries.GetAllEmployees;

public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeDto>>
{
    private readonly IEmployeeReadRepository _readRepository;

    public GetAllEmployeesQueryHandler(IEmployeeReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<IEnumerable<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        var employees = await _readRepository.GetAllAsync();

        return employees.Select(e => new EmployeeDto
        {
            Id         = e.Id,
            CompanyId  = e.CompanyId,
            CreatedOn  = e.CreatedOn,
            DeletedOn  = e.DeletedOn,
            Email      = e.Email,
            Fax        = e.Fax,
            Name       = e.Name,
            LastLogin  = e.LastLogin,
            Password   = e.Password,
            PortalId   = e.PortalId,
            RoleId     = e.RoleId,
            StatusId   = e.StatusId,
            Telephone  = e.Telephone,
            UpdatedOn  = e.UpdatedOn,
            Username   = e.Username
        });
    }
}