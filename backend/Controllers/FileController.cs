using FileManagerBackend.Models;
using FileManagerBackend.Repositories.FileRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using File = FileManagerBackend.Models.File;

namespace FileManagerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;

        public FileController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetFiles()
        {
            try
            {
                var files = await _fileRepository.GetFilesAsync();
                return Ok(files);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error retrieving files. Please try again later. Error = " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(int id)
        {
            try
            {
                var file = await _fileRepository.GetFileAsync(id);
                if (file == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, "File not found.");
                }
                return Ok(file);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error retrieving file. Please try again later. Error : " + ex.Message);
            }
        }

        [HttpPost]
        [RequestSizeLimit(10_000_000)]
        public async Task<IActionResult> CreateFile([FromForm] IFormFile file, [FromForm] string fileDataJson)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            File fileEntry;
            File fileData;

            try
            {
                // Deserialize the JSON file data
                fileData = JsonSerializer.Deserialize<File>(fileDataJson);

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var binaryData = memoryStream.ToArray();

                    fileEntry = new File
                    {
                        VolumeId = fileData.VolumeId,
                        Name = fileData.Name,
                        Description = fileData.Description,
                        SavePaper = fileData.SavePaper,
                        CreateDate = DateTime.Now,
                        ModifyDate = DateTime.Now,
                        BinaryData = binaryData,
                    };

                    await _fileRepository.AddFileAsync(fileEntry);
                }

                return CreatedAtAction(nameof(GetFile), new { id = fileEntry.Id }, fileEntry);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error creating file. Please try again later. Error : " + ex);
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFile(int id, File file)
        {
            try
            {
                if (id != file.Id)
                {
                    return BadRequest("Id mismatch. Please ensure the id in the route matches the id in the request body.");
                }

                await _fileRepository.UpdateFileAsync(file);
                return StatusCode((int)HttpStatusCode.OK, "File updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error updating file. Please try again later. Error : " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            try
            {
                await _fileRepository.DeleteFileAsync(id);
                return StatusCode((int)HttpStatusCode.OK, "File deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error deleting file. Please try again later. Error : " + ex.Message);
            }
        }

        [HttpGet("filter/{volumeId}")]
        public async Task<IActionResult> GetFilesByVolumeId(int volumeId)
        {
            try
            {
                var files = await _fileRepository.GetFilesByVolumeIdAsync(volumeId);
                return Ok(files);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error retrieving files. Please try again later. Error = " + ex.Message);
            }
        }

        [HttpGet("volumes")]
        public async Task<IActionResult> GetVolumesWithFilesAndTags()
        {
            try
            {
                var volumes = await _fileRepository.GetVolumesWithFilesAndTagsAsync();

                if (volumes == null || !volumes.Any())
                {
                    return NotFound("No volumes found.");
                }

                return Ok(volumes);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error retrieving volumes. Please try again later. Error = " + ex.Message);
            }
        }
    }
}