using FileManagerBackend.Services.Data;
using FileManagerBackend.Models;
using Microsoft.EntityFrameworkCore;
using FileManagerBackend.Repositories.Exceptions;

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
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("Id must be greater than 0.");
                }
                return await _context.Volumes.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error getting volume with id {id}", ex);
            }
        }

        public async Task<IEnumerable<Volume>> GetVolumesAsync()
        {
            try
            {
                return await _context.Volumes.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting volumes", ex);
            }
        }

        public async Task AddVolumeAsync(Volume volume)
        {
            try
            {
                if (volume == null)
                {
                    throw new ArgumentNullException(nameof(volume), "Volume cannot be null.");
                }
                _context.Volumes.Add(volume);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error adding volume", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error adding volume", ex);
            }
        }

        public async Task UpdateVolumeAsync(Volume volume)
        {
            try
            {
                if (volume == null)
                {
                    throw new ArgumentNullException(nameof(volume), "Volume cannot be null.");
                }
                _context.Volumes.Update(volume);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error updating volume", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error updating volume", ex);
            }
        }

        public async Task DeleteVolumeAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("Id must be greater than 0.");
                }
                var volume = await GetVolumeAsync(id);
                if (volume != null)
                {
                    _context.Volumes.Remove(volume);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new RepositoryException($"Volume with id {id} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error deleting volume with id {id}", ex);
            }
        }
    }
}
