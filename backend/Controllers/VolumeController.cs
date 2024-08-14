using System.Net;
using FileManagerBackend.Models;
using FileManagerBackend.Repositories.VolumeRepository;
using Microsoft.AspNetCore.Mvc;

namespace FileManagerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolumeController : ControllerBase
    {
        private readonly IVolumeRepository _volumeRepository;

        public VolumeController(IVolumeRepository volumeRepository)
        {
            _volumeRepository = volumeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetVolumes()
        {
            try
            {
                var volumes = await _volumeRepository.GetVolumesAsync();
                return Ok(volumes);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error retrieving volumes. Please try again later. Error = " + ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVolume(int id)
        {
            try
            {
                var volume = await _volumeRepository.GetVolumeAsync(id);
                if (volume == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, "Volume not found.");
                }
                return Ok(volume);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error retrieving volume. Please try again later. Error : " + ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateVolume(Volume volume)
        {
            try
            {
                await _volumeRepository.AddVolumeAsync(volume);
                return CreatedAtAction(nameof(GetVolume), new { id = volume.Id }, volume);
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, "Error creating volume. Please try again later. Error : " + ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVolume(int id, Volume volume)
        {
            try
            {
                if (id != volume.Id)
                {
                    return BadRequest("Bad Request. Please check the values");
                }

                await _volumeRepository.UpdateVolumeAsync(volume);
                return StatusCode((int)HttpStatusCode.OK, "Volume updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error updating file. Please try again later. Error : " + ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVolume(int id)
        {
            try
            {
              await _volumeRepository.DeleteVolumeAsync(id);
            return StatusCode((int)HttpStatusCode.OK, "Volume deleted successfully.");  
            }
            catch (Exception ex)
            {
               return StatusCode((int)HttpStatusCode.InternalServerError, "Error deleting volume. Please try again later. Error : " + ex.Message);
            }
            
        }
    }
}
