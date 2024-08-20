using FileManagerBackend.Models;
using FileManagerBackend.Repositories.Exceptions;
using FileManagerBackend.Services.Data;
using Microsoft.EntityFrameworkCore;
using File = FileManagerBackend.Models.File;
using System.Text.Json;

namespace FileManagerBackend.Repositories.FileRepository
{
    public class FileRepository : IFileRepository
    {
        private readonly FileManagementContext _context; 

        public FileRepository(FileManagementContext context)
        {
            _context = context;
        }

        public async Task<File> GetFileAsync(int id)
        {
            try
            {
                return await _context.Files.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error getting file with id {id}", ex);
            }

        }

        public async Task<IEnumerable<File>> GetFilesAsync()
        {
            try
            {
                var files = await _context.Files
                    .Include(f => f.Volume)
                    .Include(f => f.TagAssignments)
                        .ThenInclude(ta => ta.Tag)
                    .ToListAsync();
                return files;
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error getting files", ex);
            }

        }

        public async Task<File> AddFileAsync(File file)
        {
            try
            {
                var volumeExists = await _context.Volumes.AnyAsync(v => v.Id == file.VolumeId);
                var fileJson = JsonSerializer.Serialize(file);
                if (!volumeExists)
                {
                    
                    throw new ArgumentException("The specified VolumeId does not exist. " + file);
                }

                _context.Files.Add(file);
                await _context.SaveChangesAsync();
                return file;
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error adding file", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error adding file", ex);
            }

        }

        public async Task UpdateFileAsync(File file)
        {
            try
            {
                _context.Files.Update(file);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error updating file", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error updating file", ex);
            }
        }

        public async Task DeleteFileAsync(int id)
        {
            try
            {
                var file = await GetFileAsync(id);
                if (file != null)
                {
                    _context.Files.Remove(file);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new RepositoryException($"File with id {id} not found");
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error deleting file with id {id}", ex);
            }
        }
        public async Task<IEnumerable<File>> GetFilesByVolumeIdAsync(int volumeId)
        {
            try
            {
                return await _context.Files
                    .Include(f => f.Volume)
                    .Include(f => f.TagAssignments)
                        .ThenInclude(ta => ta.Tag)
                    .Where(f => f.VolumeId == volumeId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error fetching files based on volumeId {volumeId}", ex);
            }

        }
        public async Task<IEnumerable<Volume>> GetVolumesWithFilesAndTagsAsync()
        {
            try
            {
                return await _context.Volumes
                    .Include(v => v.Files)
                        .ThenInclude(f => f.TagAssignments)
                            .ThenInclude(ta => ta.Tag)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error fetching volumes with files and tags", ex);
            }
        }

    }
}
