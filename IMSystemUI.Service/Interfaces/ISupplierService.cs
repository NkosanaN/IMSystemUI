using IMSystemUI.Domain;

namespace IMSystemUI.Service.Interfaces;
public interface ISupplierService
{
    Task<IEnumerable<Supplier>> GetAllSupplierAsync();
    //Task<IEnumerable<Post>> GetAllSupplierAsync();
    Task<Supplier> GetSupplierAsync();
    Task CreateSupplierAsync(Supplier supplier);
    Task<Supplier> RemoveSupplierAsync();

}
