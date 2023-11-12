﻿using ApiDemoFilms.Model;
using Films.DAL.Interfaces;
using Films.DAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemoFilms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController: Controller
    {
        private readonly IDirectorRepository _directorRepository;

        public DirectorController(IDirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
        }

        [HttpGet("GetIdDirectors/{id}")]
        [ProducesResponseType(200, Type = typeof(Director))]
        public async Task<IActionResult> GetIdDirectorsAsync(int id)
        {
            var director = await _directorRepository.GetIdDirectorsAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(director);
        }

        [HttpGet("GetDirectors")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Director>))]
        public async Task<IActionResult> GetDirectorsAsync()
        {
            var directors = await _directorRepository.GetDirectorsAsync();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(directors);
        }

    }
}
