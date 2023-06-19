using IMSystemUI.Domain;

namespace IMSystemUI.Service.Interfaces;
public interface ISupplierService
{
    Task<IEnumerable<Supplier>> GetAllSupplierAsync(string token);
    Task<Supplier> GetSupplierAsync(Guid id, string token);
    Task<Supplier> CreateSupplierAsync(Supplier supplier, string token);
    Task UpdateSupplierAsync(Guid id, Supplier supplier , string token);
    Task RemoveSupplierAsync(Guid id, string token);

}
