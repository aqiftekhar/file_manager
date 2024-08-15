using FileManagerBackend.Services.Data;
using FileManagerBackend.Models;
using Microsoft.EntityFrameworkCore;
using FileManagerBackend.Repositories.Exceptions;

namespace FileManagerBackend.Repositories.TagRepository
{
    public class TagRepository : ITagRepository
    {
        private readonly FileManagementContext _context;

        public TagRepository(FileManagementContext context)
        {
            _context = context;
        }

        public async Task<Tag> GetTagAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("Id must be greater than 0.");
                }
                return await _context.Tags.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error getting tag with id {id}", ex);
            }
        }

        public async Task<IEnumerable<Tag>> GetTagsAsync()
        {
            try
            {
                return await _context.Tags.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting tags", ex);
            }
        }

        public async Task AddTagAsync(Tag tag)
        {
            try
            {
                if (tag == null)
                {
                    throw new ArgumentNullException(nameof(tag), "Tag cannot be null.");
                }
                _context.Tags.Add(tag);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error adding tag", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error adding tag", ex);
            }
        }

        public async Task UpdateTagAsync(Tag tag)
        {
            try
            {
                if (tag == null)
                {
                    throw new ArgumentNullException(nameof(tag), "Tag cannot be null.");
                }
                _context.Tags.Update(tag);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error updating tag", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error updating tag", ex);
            }
        }

        public async Task DeleteTagAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("Id must be greater than 0.");
                }
                var tag = await GetTagAsync(id);
                if (tag != null)
                {
                    _context.Tags.Remove(tag);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new RepositoryException($"Tag with id {id} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error deleting tag with id {id}", ex);
            }
        }
    }
}
