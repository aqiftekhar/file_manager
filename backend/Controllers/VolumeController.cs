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
                throw ex;
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVolume(int id)
        {
            var volume = await _volumeRepository.GetVolumeAsync(id);
            if (volume == null)
            {
                return NotFound("Volumes not Found.");
            }
            return Ok(volume);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVolume(Volume volume)
        {
            await _volumeRepository.AddVolumeAsync(volume);
            return CreatedAtAction(nameof(GetVolume), new { id = volume.Id }, volume);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVolume(int id, Volume volume)
        {
            if (id != volume.Id)
            {
                return BadRequest("Bad Request. Please check the values");
            }

            await _volumeRepository.UpdateVolumeAsync(volume);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVolume(int id)
        {
            await _volumeRepository.DeleteVolumeAsync(id);
            return NoContent();
        }
    }
}
