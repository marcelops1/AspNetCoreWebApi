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

            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);
        }


        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);
            if (_repo.SaveChanges()) return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAllAlunoById(id);
            if (aluno == null) return NoContent();

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges()) return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAllAlunoById(id);
            if (aluno == null) return NoContent();

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges()) return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));


            return BadRequest("Aluno não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAllAlunoById(id);
            if (aluno == null) return NoContent();

            _repo.Remove(aluno);
            if (_repo.SaveChanges()) return Ok("Aluno removido");

            return BadRequest("Aluno não removido");
        }
    }
}