﻿using Microsoft.AspNetCore.Mvc;
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
    }
}
