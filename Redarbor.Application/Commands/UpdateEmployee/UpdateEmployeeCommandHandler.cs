using MediatR;
using Redarbor.Domain.Entities;
using Redarbor.Domain.Interfaces;

namespace Redarbor.Application.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
{
    private readonly IEmployeeWriteRepository _writeRepository;
    private readonly IEmployeeReadRepository _readRepository;

    public UpdateEmployeeCommandHandler(
        IEmployeeWriteRepository writeRepository,
        IEmployeeReadRepository readRepository)
    {
        _writeRepository = writeRepository;
        _readRepository  = readRepository;
    }

    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (!await _readRepository.ExistsByIdAsync(request.Id))
            throw new KeyNotFoundException($"Employee with id {request.Id} not found.");

        if (await _readRepository.ExistsByEmailAsync(request.Email, request.Id))
            throw new InvalidOperationException($"Email '{request.Email}' already exists.");

        if (await _readRepository.ExistsByUsernameAsync(request.Username, request.Id))
            throw new InvalidOperationException($"Username '{request.Username}' already exists.");

        var employee = new Employee
        {
            Id         = request.Id,
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

        await _writeRepository.UpdateAsync(employee);
    }
}