using File = FileManagerBackend.Models.File;

namespace FileManagerBackend.Repositories.FileRepository
{
    public interface IFileRepository
    {
        Task<File> GetFileAsync(int id);
        Task<IEnumerable<File>> GetFilesAsync();
        Task AddFileAsync(File file);
        Task UpdateFileAsync(File file);
        Task DeleteFileAsync(int id);
    }
}
