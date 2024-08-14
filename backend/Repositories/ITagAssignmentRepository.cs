using FileManagerBackend.Models;

namespace FileManagementApi.Repositories
{
    public interface ITagAssignmentRepository
    {
        Task<IEnumerable<TagAssignment>> GetTagAssignmentsForFileAsync(int fileId);
        Task AddTagAssignmentAsync(TagAssignment tagAssignment);
        Task RemoveTagAssignmentAsync(int fileId, int tagId);
    }
}
