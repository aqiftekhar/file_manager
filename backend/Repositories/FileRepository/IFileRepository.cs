using FileManagerBackend.Models;
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
         Task<IEnumerable<File>> GetFilesByVolumeIdAsync(int volumeId);

         Task<IEnumerable<Volume>> GetVolumesWithFilesAndTagsAsync();
    }
}
