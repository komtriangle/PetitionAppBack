using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetitionApp.API.DTO;
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

        public PetitionController(IPetitionService petitionService, IMapper mapper)
        {
            _petitionService = petitionService;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("CreatePetition")]
        public async Task<IActionResult> CreatePetiiton([FromBody] CreatePetitionDTO createPetitionDTO)
        {
            //Validate 
           
            var tags = createPetitionDTO.Tags.Select(t => new Tag() { Name = t}).ToList();

            var userid = User.Claims.FirstOrDefault(c => c.Properties.Any(p => p.Value == JwtRegisteredClaimNames.Sub))?.Value;

            var petition = _mapper.Map<Petition>(createPetitionDTO);
            petition.AuthorId =  Guid.Parse(userid);
            await _petitionService.CreateAsync(petition, tags);

            var petitionDTO = _mapper.Map<PetitionDTO>(petition);
            return Ok(petitionDTO);
        }

        [HttpGet]
        [Route("Petitions")]
        public IActionResult Petitions(int count)
        {
            var a = _petitionService.GetTopPetitions(count);
            var petitionDTOs = _petitionService.GetTopPetitions(count).Select(p => _mapper.Map<PetitionDTO>(p));
            return Ok(petitionDTOs);
        }


        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok("Success");
        }
    }
}
