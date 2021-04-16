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
    [Route("api/aluno")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;
        public AlunoController(IRepository repo, IMapper mapper)
        {            
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);            
            return Ok(_mapper.Map<IEnumerable<AlunoDTO>>(alunos));
        }

        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {               
            return Ok(new AlunoRegistrarDTO());
        }

        // api/aluno/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O aluno não foi encontrado!");

            var alunoDTO = _mapper.Map<AlunoDTO>(aluno);

            return Ok(alunoDTO);
        }

        // api/aluno/
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDTO alunoDTO)
        {
            var aluno = _mapper.Map<Aluno>(alunoDTO);
            
            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{alunoDTO.Id}", _mapper.Map<AlunoDTO>(aluno));
            }

            return BadRequest("Aluno não cadastrado!");
        }

        // api/aluno/
        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDTO alunoDTO)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado!");

            _mapper.Map(alunoDTO, aluno);
            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{alunoDTO.Id}", _mapper.Map<AlunoDTO>(aluno));
            }

            return BadRequest("Aluno não atualizado!");
        }

        // api/aluno/
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDTO alunoDTO)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado!");

            _mapper.Map(alunoDTO, aluno);
            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{alunoDTO.Id}", _mapper.Map<AlunoDTO>(aluno));
            }

            return BadRequest("Aluno não atualizado!");
        }

        // api/aluno/
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado!");

            _repo.Delete(aluno);
            if (_repo.SaveChanges())
            {
                return Ok("Aluno deletado!");
            }

            return BadRequest("Aluno não deletado!");
        }
    }
}