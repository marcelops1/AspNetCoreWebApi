using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Data.Repositories;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAllAlunoById(id, false);
            if (aluno == null) return NoContent();

            return Ok(aluno);
        }


        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add(aluno);
            if (_repo.SaveChanges()) return Ok(aluno);

            return BadRequest("Aluno não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _repo.GetAllAlunoById(id);
            if (alu == null) return NoContent();

            _repo.Update(aluno);
            if (_repo.SaveChanges()) return Ok(aluno);

            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repo.GetAllAlunoById(id);
            if (alu == null) return NoContent();

            _repo.Update(aluno);
            if (_repo.SaveChanges()) return Ok(aluno);

            return BadRequest("Aluno não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var alu = _repo.GetAllAlunoById(id);
            if (alu == null) return NoContent();

            _repo.Remove(alu);
            if (_repo.SaveChanges()) return Ok("Aluno removido");

            return BadRequest("Aluno não removido");
        }
    }
}