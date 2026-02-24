using Redarbor.Domain.Entities;

namespace Redarbor.Domain.Interfaces;

public interface IEmployeeReadRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
}