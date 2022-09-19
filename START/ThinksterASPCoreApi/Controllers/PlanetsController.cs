using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThinksterASPCoreApi.DatabaseEntities;
using ThinksterASPCoreApi.Repository;

namespace ThinksterASPCoreApi.Controllers
{
    [Route("api/[controller]")]
    public class PlanetsController : ControllerBase
    {
        private readonly ISpaceRepository _spaceRepository;

        public PlanetsController(ISpaceRepository spaceRepository)
        {
            _spaceRepository = spaceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(bool returnMoons = false)
        {
            try
            {
                List<Planet> result = await _spaceRepository.GetAllPlanetsAsync(returnMoons);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, bool returnMoons = false)
        {
            try
            {
                Planet result = await _spaceRepository.GetPlanetAsync(id, returnMoons);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }            
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Planet planet)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest("Model is missing required data!");
                }

                _spaceRepository.AddPlanet(planet);
                bool result = await _spaceRepository.SaveChangesAsync();
                if (result)
                {
                    return Created($"api/planets/{planet.Id}", planet);
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach the database");
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Planet newPlanetData)
        {
            try
            {
                if (id != newPlanetData.Id)
                {
                    return BadRequest("ID's do not match!");
                }

                var existingPlanet = await _spaceRepository.GetPlanetAsync(id, true);
                if (existingPlanet == null)
                {
                    return BadRequest("Could not find planet with id {id}");
                }

                existingPlanet.Mass = newPlanetData.Mass;
                existingPlanet.Name = newPlanetData.Name;
                for (int i = 0; i < existingPlanet.Moons.Count; i++)
                {
                    existingPlanet.Moons[i].Name = newPlanetData.Moons[i].Name;
                }

                await _spaceRepository.SaveChangesAsync();

                return NoContent();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not reach the database");
            }
            
        }
    }
}
