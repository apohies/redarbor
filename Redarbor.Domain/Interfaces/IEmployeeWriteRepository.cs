using Redarbor.Domain.Entities;

namespace Redarbor.Domain.Interfaces;

public interface IEmployeeWriteRepository
{
    Task<Employee> CreateAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(int id);
}