using System.Net;
using FileManagerBackend.Models;
using FileManagerBackend.Repositories.TagRepository;
using Microsoft.AspNetCore.Mvc;

namespace FileManagerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            try
            {
                var tags = await _tagRepository.GetTagsAsync();
                return Ok(tags);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error retrieving Tags. Please try again later. Error = " + ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTag(int id)
        {
            try
            {
                var tag = await _tagRepository.GetTagAsync(id);
                if (tag == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, "Tag not found.");
                }
                return Ok(tag);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error retrieving file. Please try again later. Error : " + ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateTag(Tag tag)
        {
            try
            {
                await _tagRepository.AddTagAsync(tag);
                return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error creating Tag. Please try again later. Error : " + ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, Tag tag)
        {
            try
            {
                if (id != tag.Id)
                {
                    return BadRequest("Id mismatch. Please ensure the id in the route matches the id in the request body.");
                }

                await _tagRepository.UpdateTagAsync(tag);
                return StatusCode((int)HttpStatusCode.OK, "Tag updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error updating Tag. Please try again later. Error : " + ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            try
            {
                await _tagRepository.DeleteTagAsync(id);
                return StatusCode((int)HttpStatusCode.OK, "Tag deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error deleting tag. Please try again later. Error : " + ex.Message);
            }

        }
    }
}
