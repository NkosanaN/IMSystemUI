using IMSystemUI.Domain;

namespace IMSystemUI.Service.Interfaces;

public interface IShelveTypeService
{
    Task<IEnumerable<ShelveType>> GetAllShelveTypesAsync();
    Task<ShelveType> GetAllShelveTypeAsync(Guid id);
    Task<ShelveType> CreateShelveTypeAsync(ShelveType shelvetype);

    Task UpdateShelveTypeAsync(Guid id, ShelveType shelvetype);
    Task RemoveShelveTypeAsync(Guid id);
}