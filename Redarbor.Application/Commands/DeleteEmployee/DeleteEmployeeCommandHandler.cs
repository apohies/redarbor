using MediatR;
using Redarbor.Domain.Interfaces;

namespace Redarbor.Application.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IEmployeeWriteRepository _writeRepository;
    private readonly IEmployeeReadRepository _readRepository;

    public DeleteEmployeeCommandHandler(
        IEmployeeWriteRepository writeRepository,
        IEmployeeReadRepository readRepository)
    {
        _writeRepository = writeRepository;
        _readRepository  = readRepository;
    }

    public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (!await _readRepository.ExistsByIdAsync(request.Id))
            throw new KeyNotFoundException($"Employee with id {request.Id} not found.");

        await _writeRepository.DeleteAsync(request.Id);
    }
}