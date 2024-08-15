using System.Net;
using FileManagerBackend.Models;
using FileManagerBackend.Repositories.TagAssignmentRepository;
using Microsoft.AspNetCore.Mvc;

namespace FileManagerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagAssignmentController : ControllerBase
    {
        private readonly ITagAssignmentRepository _tagAssignmentRepository;

        public TagAssignmentController(ITagAssignmentRepository tagAssignmentRepository)
        {
            _tagAssignmentRepository = tagAssignmentRepository;
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> GetTagAssignmentsForFile(int fileId)
        {
            try
            {
                var tagAssignments = await _tagAssignmentRepository.GetTagAssignmentsForFileAsync(fileId);
                return Ok(tagAssignments);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error retrieving Tags. Please try again later. Error = " + ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateTagAssignment(TagAssignment tagAssignment)
        {
            try
            {
                await _tagAssignmentRepository.AddTagAssignmentAsync(tagAssignment);
                return CreatedAtAction(nameof(GetTagAssignmentsForFile), new { fileId = tagAssignment.FileId }, tagAssignment);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error creating Tag Assignment. Please try again later. Error : " + ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateTagAssignment(TagAssignment tagAssignment)
        {
            try
            {
                var existingTagAssignment = await _tagAssignmentRepository.GetTagAssignmentAsync(tagAssignment.FileId, tagAssignment.TagId);
                if (existingTagAssignment == null)
                {
                    return NotFound("Tag Assignment not found.");
                }

                await _tagAssignmentRepository.UpdateTagAssignmentAsync(tagAssignment);
                return NoContent(); // Indicates successful update without returning any content
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error updating Tag Assignment. Please try again later. Error : " + ex.Message);
            }
        }
        
        [HttpDelete("{fileId}/{tagId}")]
        public async Task<IActionResult> RemoveTagAssignment(int fileId, int tagId)
        {
            try
            {
                await _tagAssignmentRepository.RemoveTagAssignmentAsync(fileId, tagId);
                return StatusCode((int)HttpStatusCode.OK, "Tag Assignment deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error deleting tag assignment. Please try again later. Error : " + ex.Message);
            }

        }
    }
}
