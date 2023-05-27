using IMSystemUI.Domain;

namespace IMSystemUI.Service.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<Department>> GetAllDepartmentsAsync();
    Task<Department> GetAllDepartmentAsync(Guid id);
    Task<Department> CreateDepartmentAsync(Department department);
    Task RemoveDepartmentAsync(Guid id);
}
