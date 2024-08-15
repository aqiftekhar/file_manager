using FileManagerBackend.Models;

namespace FileManagerBackend.Repositories.TagAssignmentRepository
{
    public interface ITagAssignmentRepository
    {
        Task<IEnumerable<TagAssignment>> GetTagAssignmentsForFileAsync(int fileId);
        Task<TagAssignment> GetTagAssignmentAsync(int fileId, int tagId);
        Task AddTagAssignmentAsync(TagAssignment tagAssignment);

        Task UpdateTagAssignmentAsync(TagAssignment tagAssignment);
        Task RemoveTagAssignmentAsync(int fileId, int tagId);
    }
}
