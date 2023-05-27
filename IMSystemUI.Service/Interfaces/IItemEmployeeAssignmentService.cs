using IMSystemUI.Domain;

namespace IMSystemUI.Service.Interfaces;

public interface IItemEmployeeAssignmentService
{
    Task<IEnumerable<ItemEmployeeAssignment>> GetAllItemEmployeeAssignmentsAsync();
    Task<ItemEmployeeAssignment> GetAllItemEmployeeAssignmentAsync(Guid id);
    Task<ItemEmployeeAssignment> CreateItemEmployeeAssignmentAsync(ItemEmployeeAssignment item);
    Task RemoveItemEmployeeAssignmentAsync(Guid id);
}
