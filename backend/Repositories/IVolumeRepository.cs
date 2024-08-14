using FileManagerBackend.Models;

namespace FileManagementApi.Repositories
{
    public interface IVolumeRepository
    {
        Task<Volume> GetVolumeAsync(int id);
        Task<IEnumerable<Volume>> GetVolumesAsync();
        Task AddVolumeAsync(Volume volume);
        Task UpdateVolumeAsync(Volume volume);
        Task DeleteVolumeAsync(int id);
    }
}
