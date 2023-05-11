using IMSystemUI.Domain;

namespace IMSystemUI.Service.Interfaces
{
    public interface IHttpClientExtensions
    {
        Task<IEnumerable<T>> GetAllAsync<T>();
        Task<T> GetByIdAsync<T>(Guid id);
        Task CreateAsync<T>(T entity);
        Task UpdateAsync<T>(Guid id, T entity);
        Task DeleteAsync(Guid id);

    }
}
