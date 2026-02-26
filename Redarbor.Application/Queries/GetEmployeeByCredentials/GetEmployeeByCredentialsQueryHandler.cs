using MediatR;
using Redarbor.Domain.Entities;
using Redarbor.Domain.Interfaces;

namespace Redarbor.Application.Queries.GetEmployeeByCredentials;

public class GetEmployeeByCredentialsQueryHandler : IRequestHandler<GetEmployeeByCredentialsQuery, Employee?>
{
    private readonly IEmployeeReadRepository _readRepository;

    public GetEmployeeByCredentialsQueryHandler(IEmployeeReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<Employee?> Handle(GetEmployeeByCredentialsQuery request, CancellationToken cancellationToken)
    {
        return await _readRepository.GetByUsernameAndPasswordAsync(
            request.Username, request.Password);
    }
}