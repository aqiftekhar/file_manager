using FileManagerBackend.Models;

namespace FileManagementApi.Repositories
{
    public interface ITagRepository
    {
        Task<Tag> GetTagAsync(int id);
        Task<IEnumerable<Tag>> GetTagsAsync();
        Task AddTagAsync(Tag tag);
        Task UpdateTagAsync(Tag tag);
        Task DeleteTagAsync(int id);
    }
}
