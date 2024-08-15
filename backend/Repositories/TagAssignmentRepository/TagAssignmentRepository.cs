using FileManagerBackend.Services.Data;
using FileManagerBackend.Models;
using Microsoft.EntityFrameworkCore;
using FileManagerBackend.Repositories.Exceptions;

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
            try
            {
                if (fileId <= 0)
                {
                    throw new ArgumentException("FileId must be greater than 0.");
                }
                return await _context.TagAssignments
                    .Where(ta => ta.FileId == fileId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error getting tag assignments for file with id {fileId}", ex);
            }
        }

        public async Task<TagAssignment?> GetTagAssignmentAsync(int fileId, int tagId)
        {
            try
            {
                if (fileId <= 0 || tagId <= 0)
                {
                    throw new ArgumentException("FileId and TagId must be greater than 0.");
                }
                return await _context.TagAssignments.FindAsync(fileId, tagId);
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error getting tag assignment for file with id {fileId} and tag with id {tagId}", ex);
            }

        }
        public async Task AddTagAssignmentAsync(TagAssignment tagAssignment)
        {
            try
            {
                if (tagAssignment == null)
                {
                    throw new ArgumentNullException(nameof(tagAssignment), "TagAssignment cannot be null.");
                }
                _context.TagAssignments.Add(tagAssignment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error adding tag assignment", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error adding tag assignment", ex);
            }
        }

        public async Task UpdateTagAssignmentAsync(TagAssignment tagAssignment)
        {
            try
            {
                if (tagAssignment == null)
                {
                    throw new ArgumentNullException(nameof(tagAssignment), "TagAssignment cannot be null.");
                }
                _context.TagAssignments.Update(tagAssignment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error updating tag assignment", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error updating tag assignment", ex);
            }
        }

        public async Task RemoveTagAssignmentAsync(int fileId, int tagId)
        {
            try
            {
                if (fileId <= 0 || tagId <= 0)
                {
                    throw new ArgumentException("FileId and TagId must be greater than 0.");
                }
                var tagAssignment = await _context.TagAssignments
                    .FirstOrDefaultAsync(ta => ta.FileId == fileId && ta.TagId == tagId);
                if (tagAssignment != null)
                {
                    _context.TagAssignments.Remove(tagAssignment);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new RepositoryException($"Tag assignment for file with id {fileId} and tag with id {tagId} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error removing tag assignment for file with id {fileId} and tag with id {tagId}", ex);
            }
        }
    }
}
