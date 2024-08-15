using FileManagerBackend.Repositories.Exceptions;
using FileManagerBackend.Services.Data;
using Microsoft.EntityFrameworkCore;
using File = FileManagerBackend.Models.File;

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
                return await _context.Files.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error getting files", ex);
            }

        }

        public async Task AddFileAsync(File file)
        {
            try
            {
                _context.Files.Add(file);
                await _context.SaveChangesAsync();
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
    }
}
