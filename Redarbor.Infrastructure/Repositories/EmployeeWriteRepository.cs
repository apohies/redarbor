using Microsoft.EntityFrameworkCore;
using Redarbor.Domain.Entities;
using Redarbor.Domain.Interfaces;
using Redarbor.Infrastructure.Context;

namespace Redarbor.Infrastructure.Repositories;

public class EmployeeWriteRepository : IEmployeeWriteRepository
{
    private readonly RedarborDbContext _context;

    public EmployeeWriteRepository(RedarborDbContext context)
    {
        _context = context;
    }

    public async Task<Employee> CreateAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task UpdateAsync(Employee employee)
    {
        var existing = await _context.Employees.FindAsync(employee.Id);

        if (existing is null)
            throw new KeyNotFoundException($"Employee with id {employee.Id} not found.");

        existing.CompanyId  = employee.CompanyId;
        existing.CreatedOn  = employee.CreatedOn;
        existing.DeletedOn  = employee.DeletedOn;
        existing.Email      = employee.Email;
        existing.Fax        = employee.Fax;
        existing.Name       = employee.Name;
        existing.LastLogin  = employee.LastLogin;
        existing.Password   = employee.Password;
        existing.PortalId   = employee.PortalId;
        existing.RoleId     = employee.RoleId;
        existing.StatusId   = employee.StatusId;
        existing.Telephone  = employee.Telephone;
        existing.UpdatedOn  = employee.UpdatedOn;
        existing.Username   = employee.Username;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _context.Employees.FindAsync(id);

        if (existing is null)
            throw new KeyNotFoundException($"Employee with id {id} not found.");

        _context.Employees.Remove(existing);
        await _context.SaveChangesAsync();
    }
}