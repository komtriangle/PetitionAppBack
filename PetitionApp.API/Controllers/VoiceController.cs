using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetitionApp.API.DTO.Voice;
using PetitionApp.Core.Models;
using PetitionApp.Core.Services;
using System.IdentityModel.Tokens.Jwt;

namespace PetitionApp.API.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class VoiceController: ControllerBase
    {
        private readonly IVoiceService _voiceService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly UserManager<User> _userManager;

        public VoiceController(IVoiceService voiceService, IMapper mapper, ILogger<VoiceController> logger, UserManager<User> userManager)
        {
            _voiceService = voiceService;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }

        /// <summary>
        /// Add voice to petition
        /// </summary>
        /// <param name="voiceDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Voice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddVoice([FromBody] AddRemoveVoiceDTO voiceDTO)
        {

            var userid = User.Claims.FirstOrDefault(c => c.Properties.Any(p => p.Value == JwtRegisteredClaimNames.Sub))?.Value;
            if (!Guid.TryParse(userid, out var userGuid))
            {
                return BadRequest("Ошибка при получении userId");
            }

            VoiceDTO voice = new VoiceDTO() { UserId = userGuid, PetitionId = voiceDTO.PetitionId };

            _logger.LogInformation($"Start adding voice: petitionId: {voice.PetitionId}, userId: {voice.UserId}");

            UserPetitions userPetition = _mapper.Map<UserPetitions>(voice);

            try
            {
                await _voiceService.AddVoice(userPetition);
                _logger.LogInformation($"Voice added: petitionId: {voice.PetitionId}, userId: {voice.UserId}");
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Error while adding voice: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete voice (if exist) from petition
        /// </summary>
        /// <param name="voiceDTO"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Voices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteVoice([FromBody] AddRemoveVoiceDTO voiceDTO)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Properties.Any(p => p.Value == JwtRegisteredClaimNames.Sub))?.Value;
            if (!Guid.TryParse(userid, out var userGuid))
            {
                return BadRequest("Ошибка при получении userId");
            }

            VoiceDTO voice = new VoiceDTO() { UserId = userGuid, PetitionId = voiceDTO.PetitionId };

            _logger.LogInformation($"Start adding voice: petitionId: {voice.PetitionId}, userId: {voice.UserId}");

            UserPetitions userPetition = _mapper.Map<UserPetitions>(voice);

            try
            {
                await _voiceService.RemoveVoice(userPetition);
                _logger.LogInformation($"Voice deleted: petitionId: {voice.PetitionId}, userId: {voice.UserId}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error while deleting voice: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// True if current user voiced for the petition
        /// </summary>
        /// <param name="petitionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Voices/IsVoiced")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> IsVoiced(int petitionId)
        {

            var userid = User.Claims.FirstOrDefault(c => c.Properties.Any(p => p.Value == JwtRegisteredClaimNames.Sub))?.Value;
            if (!Guid.TryParse(userid, out var userGuid))
            {
                return BadRequest("Ошибка при получении userId");
            }

            _logger.LogInformation($"Checking isVoiced: petitionId: {petitionId}, userId: {userGuid}");
            try
            {
              return Ok(await _voiceService.IsVoiced(new UserPetitions() { PetitionId = petitionId, UserId =  userGuid }));
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while checking isVoiced: {ex.Message}");
                return BadRequest(ex.Message);

            }
        }

    }
}
