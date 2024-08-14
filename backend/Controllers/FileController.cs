using FileManagerBackend.Models;
using FileManagerBackend.Repositories.FileRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        public async Task<IActionResult> CreateFile(File file)
        {
            try
            {
                await _fileRepository.AddFileAsync(file);
                return CreatedAtAction(nameof(GetFile), new { id = file.Id }, file);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error creating file. Please try again later. Error : "+ ex.Message);
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
    }
}