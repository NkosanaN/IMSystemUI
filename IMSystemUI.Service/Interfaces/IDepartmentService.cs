using IMSystemUI.Domain;

namespace IMSystemUI.Service.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<Department>> GetAllDepartmentsAsync(string token);
    Task<Department> GetAllDepartmentAsync(Guid id, string token);
    Task<Department> CreateDepartmentAsync(Department department, string token);
    Task RemoveDepartmentAsync(Guid id, string token);
}
