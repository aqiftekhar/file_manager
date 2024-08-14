using FileManagerBackend.Models;

namespace FileManagerBackend.Repositories.TagAssignmentRepository
{
    public interface ITagAssignmentRepository
    {
        Task<IEnumerable<TagAssignment>> GetTagAssignmentsForFileAsync(int fileId);
        Task AddTagAssignmentAsync(TagAssignment tagAssignment);
        Task RemoveTagAssignmentAsync(int fileId, int tagId);
    }
}
