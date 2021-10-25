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
        private readonly SmartContext _context;
        private readonly IRepository _repo;
        public ProfessorController(SmartContext context, IRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
            if (professor == null) return NoContent();
            {
                return Ok(professor);
            }
        }
        [HttpGet("ByName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var professor = _context.Professores.FirstOrDefault(p => p.Nome.Contains(nome));
            if (professor == null) return NoContent();

            return Ok(professor);

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
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (prof == null) return NoContent();
            _context.Update(professor);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (prof == null) return NoContent();
            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
            if (professor == null) return NoContent();
            _context.Remove(professor);
            _context.SaveChanges();
            return Ok();
        }
    }
}