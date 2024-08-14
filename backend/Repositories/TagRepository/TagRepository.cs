using FileManagerBackend.Services.Data;
using FileManagerBackend.Models;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Tags.FindAsync(id);
        }

        public async Task<IEnumerable<Tag>> GetTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task AddTagAsync(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTagAsync(Tag tag)
        {
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTagAsync(int id)
        {
            var tag = await GetTagAsync(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
        }
    }
}
