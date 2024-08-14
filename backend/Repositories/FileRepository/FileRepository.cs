using FileManagerBackend.Services.Data;
using Microsoft.EntityFrameworkCore;
using File = FileManagerBackend.Models.File;

namespace  FileManagerBackend.Repositories.FileRepository
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
            return await _context.Files.FindAsync(id);
        }

        public async Task<IEnumerable<File>> GetFilesAsync()
        {
            return await _context.Files.ToListAsync();
        }

        public async Task AddFileAsync(File file)
        {
            _context.Files.Add(file);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFileAsync(File file)
        {
            _context.Files.Update(file);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFileAsync(int id)
        {
            var file = await GetFileAsync(id);
            if (file != null)
            {
                _context.Files.Remove(file);
                await _context.SaveChangesAsync();
            }
        }
    }
}
