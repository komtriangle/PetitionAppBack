﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetitionApp.API.DTO;
using PetitionApp.API.Validators;
using PetitionApp.Core.Models;
using PetitionApp.Core.Services;
using System.IdentityModel.Tokens.Jwt;

namespace PetitionApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PetitionController :ControllerBase
    {

        private readonly IPetitionService _petitionService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PetitionController(IPetitionService petitionService, IMapper mapper, ILogger<PetitionController> logger)
        {
            _petitionService = petitionService;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpPost]
        [Route("CreatePetition")]
        public async Task<IActionResult> CreatePetiiton([FromBody] CreatePetitionDTO createPetitionDTO)
        {
            _logger.LogInformation("Start creating new petition with name: {0}", createPetitionDTO?.Title);
            var validationResult = new CreatePetitionValidator().Validate(createPetitionDTO);

            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation error: {0}", validationResult.Errors);
                return BadRequest(string.Join("; ", validationResult.Errors));
            }
            var tags = createPetitionDTO.Tags.Select(t => new Tag() { Name = t}).ToList();

            var userid = User.Claims.FirstOrDefault(c => c.Properties.Any(p => p.Value == JwtRegisteredClaimNames.Sub))?.Value;

            var petition = _mapper.Map<Petition>(createPetitionDTO);
            petition.AuthorId =  Guid.Parse(userid);
            try
            {
                await _petitionService.CreateAsync(petition, tags);
                _logger.LogInformation("Petition with Id: {0} created", petition.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while creating petition: {0}", ex.Message);
                return BadRequest(ex.Message);
            }

            var petitionDTO = _mapper.Map<PetitionDTO>(petition);
            return Ok(petitionDTO);
        }

        [HttpGet]
        [Route("Petitions")]
        public IActionResult Petitions(int count)
        {
            try
            {
                _logger.LogInformation("Getting list of petition");
                var petitionDTOs = _petitionService.GetTopPetitions(count).Select(p => _mapper.Map<PetitionDTO>(p));
                return Ok(petitionDTOs);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error while getting list of petitions: {0}", ex.Message);
                return StatusCode(500, "Во время получения списка петиций произошла ошибка");
            }
        }

        [HttpDelete]
        [Route("DeletePetition")]
        public async Task<IActionResult> DeletePetition(int petitionId)
        {
            _logger.LogInformation("Start deleting petition with Id: {0}", petitionId);
            if (petitionId < 0)
            {
                _logger.LogError("Invalid value of petitionId: {0}", petitionId);
                return BadRequest("Id петиции должно быть больше или равно нулю");
            }
            try
            {
                await _petitionService.DeletePetition(petitionId);
                _logger.LogInformation("Petition with Id: {0} deleted", petitionId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while deleting petition with Id: {0}", petitionId);
                return BadRequest(ex.Message);
            }
            return Ok(petitionId); 
        }

    }
}
