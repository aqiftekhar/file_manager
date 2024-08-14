using FileManagerBackend.Services.Data;
using FileManagerBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace FileManagerBackend.Repositories.VolumeRepository
{
    public class VolumeRepository : IVolumeRepository
    {
        private readonly FileManagementContext _context;

        public VolumeRepository(FileManagementContext context)
        {
            _context = context;
        }

        public async Task<Volume> GetVolumeAsync(int id)
        {
            return await _context.Volumes.FindAsync(id);
        }

        public async Task<IEnumerable<Volume>> GetVolumesAsync()
        {
            return await _context.Volumes.ToListAsync();
        }

        public async Task AddVolumeAsync(Volume volume)
        {
            _context.Volumes.Add(volume);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVolumeAsync(Volume volume)
        {
            _context.Volumes.Update(volume);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVolumeAsync(int id)
        {
            var volume = await GetVolumeAsync(id);
            if (volume != null)
            {
                _context.Volumes.Remove(volume);
                await _context.SaveChangesAsync();
            }
        }
    }
}
