using IMSystemUI.Domain;

namespace IMSystemUI.Service.Interfaces;
public interface ISupplierService
{
    Task<IEnumerable<Supplier>> GetAllSupplierAsync();
    Task<Supplier> GetSupplierAsync(Guid id);
    Task<Supplier> CreateSupplierAsync(Supplier supplier);
    Task UpdateSupplierAsync(Guid id, Supplier supplier);
    Task RemoveSupplierAsync(Guid id);

}
