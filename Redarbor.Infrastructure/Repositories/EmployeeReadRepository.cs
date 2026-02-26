using Dapper;
using MySqlConnector;
using Redarbor.Domain.Entities;
using Redarbor.Domain.Interfaces;

namespace Redarbor.Infrastructure.Repositories;

public class EmployeeReadRepository : IEmployeeReadRepository
{
    private readonly string _connectionString;

    public EmployeeReadRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private MySqlConnection CreateConnection() => new(_connectionString);

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Employee";

        using var connection = CreateConnection();
        return await connection.QueryAsync<Employee>(sql);
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        const string sql = "SELECT * FROM Employee WHERE Id = @Id";

        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Employee>(sql, new { Id = id });
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        const string sql = "SELECT COUNT(1) FROM Employee WHERE Id = @Id";

        using var connection = CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });
        return count > 0;
    }

    public async Task<bool> ExistsByEmailAsync(string email, int? excludeId = null)
    {
        const string sql = "SELECT COUNT(1) FROM Employee WHERE Email = @Email AND (@ExcludeId IS NULL OR Id != @ExcludeId)";

        using var connection = CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(sql, new { Email = email, ExcludeId = excludeId });
        return count > 0;
    }

    public async Task<bool> ExistsByUsernameAsync(string username, int? excludeId = null)
    {
        const string sql = "SELECT COUNT(1) FROM Employee WHERE Username = @Username AND (@ExcludeId IS NULL OR Id != @ExcludeId)";

        using var connection = CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(sql, new { Username = username, ExcludeId = excludeId });
        return count > 0;
    }
    
    public async Task<Employee?> GetByUsernameAndPasswordAsync(string username, string password)
    {
        const string sql = "SELECT * FROM Employee WHERE Username = @Username AND Password = @Password";
        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Employee>(sql, new { Username = username, Password = password });
    }
}