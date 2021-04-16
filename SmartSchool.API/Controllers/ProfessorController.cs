using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.DTO;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/professor")]
    public class ProfessorController : ControllerBase
    {
        public readonly IRepository _repo;
         private readonly IMapper _mapper;
        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repo.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDTO>>(professores));
        }

        // api/aluno/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("O professor não foi encontrado!");

             var professorDTO = _mapper.Map<ProfessorDTO>(professor);

            return Ok(professorDTO);
        }

        // api/professor/
        [HttpPost]
        public IActionResult Post(ProfessorResgistrarDTO professorResgistrarDTO)
        {
           var professor = _mapper.Map<Professor>(professorResgistrarDTO);

            _repo.Add(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{professorResgistrarDTO.Id}", _mapper.Map<AlunoDTO>(professor));
            }

            return BadRequest("Professor não cadastrado!");
        }

        // api/professor/
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorResgistrarDTO professorResgistrarDTO)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("professor não encontrado!");

            _mapper.Map(professorResgistrarDTO, professor);
            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{professorResgistrarDTO.Id}", _mapper.Map<AlunoDTO>(professor));
            }

            return BadRequest("Professor não atualizado!");
        }

        // api/professor/
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorResgistrarDTO professorResgistrarDTO)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("professor não encontrado!");

            _repo.Update(professor);
            _mapper.Map(professorResgistrarDTO, professor);
            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{professorResgistrarDTO.Id}", _mapper.Map<AlunoDTO>(professor));
            }

            return BadRequest("Professor não atualizado!");
        }

        // api/professor/
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("professor não encontrado!");

            _repo.Delete(professor);
            if (_repo.SaveChanges())
            {
                return Ok("Dados excluídos com sucesso!");
            }

            return BadRequest("Professor não deletado!");
        }
    }
}