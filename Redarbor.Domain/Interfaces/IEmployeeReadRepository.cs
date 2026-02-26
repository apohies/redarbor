using Redarbor.Domain.Entities;

namespace Redarbor.Domain.Interfaces;

public interface IEmployeeReadRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task<bool> ExistsByIdAsync(int id);
    Task<bool> ExistsByEmailAsync(string email, int? excludeId = null);
    Task<bool> ExistsByUsernameAsync(string username, int? excludeId = null);
    
    Task<Employee?> GetByUsernameAndPasswordAsync(string username, string password);
}