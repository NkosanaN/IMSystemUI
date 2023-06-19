using IMSystemUI.Domain;

namespace IMSystemUI.Service.Interfaces;

public interface IItemEmployeeAssignmentService
{
    Task<IEnumerable<ItemEmployeeAssignment>> GetAllItemEmployeeAssignmentsAsync(string token);
    Task<ItemEmployeeAssignment> GetAllItemEmployeeAssignmentAsync(Guid id, string token);
    Task<ItemEmployeeAssignment> CreateItemEmployeeAssignmentAsync(ItemEmployeeAssignment item, string token);
    Task RemoveItemEmployeeAssignmentAsync(Guid id, string token);
    Task<bool> ReturnItem(ItemEmployeeAssignment iItem, string token);
    //Task UpdateItemEmployeeAssignmentAsync(Guid id, ItemEmployeeAssignment item, string token);
}
