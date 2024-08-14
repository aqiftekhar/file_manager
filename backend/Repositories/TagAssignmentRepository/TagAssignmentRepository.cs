using FileManagerBackend.Services.Data;
using FileManagerBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace FileManagerBackend.Repositories.TagAssignmentRepository
{
    public class TagAssignmentRepository : ITagAssignmentRepository
    {
        private readonly FileManagementContext _context;

        public TagAssignmentRepository(FileManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TagAssignment>> GetTagAssignmentsForFileAsync(int fileId)
        {
            return await _context.TagAssignments
                .Where(ta => ta.FileId == fileId)
                .ToListAsync();
        }

        public async Task AddTagAssignmentAsync(TagAssignment tagAssignment)
        {
            _context.TagAssignments.Add(tagAssignment);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveTagAssignmentAsync(int fileId, int tagId)
        {
            var tagAssignment = await _context.TagAssignments
                .FirstOrDefaultAsync(ta => ta.FileId == fileId && ta.TagId == tagId);
            if (tagAssignment != null)
            {
                _context.TagAssignments.Remove(tagAssignment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
