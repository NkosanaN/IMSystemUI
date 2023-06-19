using IMSystemUI.Domain;

namespace IMSystemUI.Service.Interfaces;

public interface IShelveTypeService
{
    Task<IEnumerable<ShelveType>> GetAllShelveTypesAsync(string token);
    Task<ShelveType> GetAllShelveTypeAsync(Guid id, string token);
    Task<ShelveType> CreateShelveTypeAsync(ShelveType shelvetype, string token);

    Task UpdateShelveTypeAsync(Guid id, ShelveType shelvetype, string token);
    Task RemoveShelveTypeAsync(Guid id, string token);
}