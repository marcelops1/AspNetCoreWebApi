using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Data.Repositories;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        public ProfessorController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllProfessores(true);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetAllProfessorById(id, false);
            if (professor == null) return NoContent();
            {
                return Ok(professor);
            }
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repo.Add(professor);
            if (_repo.SaveChanges()) return Ok(professor);

            return BadRequest("Professor não cadastrado");
        }

        [HttpGet("id")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _repo.GetAllProfessorById(id, false);
            if (prof == null) return NoContent();
            _repo.Update(professor);
            if (_repo.SaveChanges()) return Ok();

            return BadRequest("Professor não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _repo.GetAllProfessorById(id, false);
            if (prof == null) return NoContent();
            _repo.Update(professor);
            if (_repo.SaveChanges()) return Ok();

            return BadRequest("Professor não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetAllProfessorById(id, false);
            if (professor == null) return NoContent();

            _repo.Remove(professor);
            if (_repo.SaveChanges())
            {
                return Ok("Professor deletado");
            }

            return Ok();
        }
    }
}