using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/aluno")]
    public class AlunoController : ControllerBase
    {   
        public readonly IRepository _repo;
        public AlunoController(IRepository repo)
        {
            _repo = repo;            
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);
            return Ok(alunos);
        }

        // api/aluno/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O aluno não foi encontrado!");

            return Ok(aluno);
        }
       
        // api/aluno/
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add(aluno);
            if(_repo.SaveChanges())
            {
                return Ok(aluno);
            }
            
            return BadRequest("Aluno não cadastrado!");
        }

        // api/aluno/
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoById(id);
            if (alu == null) return BadRequest("Aluno não encontrado!");

           _repo.Update(aluno);
            if(_repo.SaveChanges())
            {
                return Ok(aluno);
            }
            
            return BadRequest("Aluno não atualizado!");
        }

        // api/aluno/
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoById(id);
            if (alu == null) return BadRequest("Aluno não encontrado!");

           _repo.Update(aluno);
            if(_repo.SaveChanges())
            {
                return Ok(aluno);
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
            if(_repo.SaveChanges())
            {
                return Ok("Aluno deletado!");
            }
            
            return BadRequest("Aluno não deletado!");
        }
    }
}