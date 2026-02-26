using MediatR;
using Redarbor.Application.DTOs;
using Redarbor.Domain.Entities;
using Redarbor.Domain.Interfaces;

namespace Redarbor.Application.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
{
    private readonly IEmployeeWriteRepository _writeRepository;
    private readonly IEmployeeReadRepository _readRepository;

    public CreateEmployeeCommandHandler(
        IEmployeeWriteRepository writeRepository,
        IEmployeeReadRepository readRepository)
    {
        _writeRepository = writeRepository;
        _readRepository  = readRepository;
    }

    public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        
        var validator = new CreateEmployeeCommandValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
            throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));
        
        if (await _readRepository.ExistsByEmailAsync(request.Email))
            throw new InvalidOperationException($"Email '{request.Email}' already exists.");

        if (await _readRepository.ExistsByUsernameAsync(request.Username))
            throw new InvalidOperationException($"Username '{request.Username}' already exists.");

        var employee = new Employee
        {
            CompanyId  = request.CompanyId,
            CreatedOn  = request.CreatedOn,
            DeletedOn  = request.DeletedOn,
            Email      = request.Email,
            Fax        = request.Fax,
            Name       = request.Name,
            LastLogin  = request.LastLogin,
            Password   = request.Password,
            PortalId   = request.PortalId,
            RoleId     = request.RoleId,
            StatusId   = request.StatusId,
            Telephone  = request.Telephone,
            UpdatedOn  = request.UpdatedOn,
            Username   = request.Username
        };

        var created = await _writeRepository.CreateAsync(employee);

        return MapToDto(created);
    }

    private static EmployeeDto MapToDto(Employee employee) => new()
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