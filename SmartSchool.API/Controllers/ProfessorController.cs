using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/professor")]
    public class ProfessorController : ControllerBase
    {        
         public readonly IRepository _repo;
        public ProfessorController(IRepository repo) {                 
            _repo = repo;            
        }  
       
       [HttpGet]
        public IActionResult Get()
        {
            var professores = _repo.GetAllProfessores(true);
            return Ok(professores);
        }

        // api/aluno/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("O professor não foi encontrado!");

            return Ok(professor);
        }        

        // api/professor/
        [HttpPost]
        public IActionResult Post(Professor professor)
        {
             _repo.Add(professor);
            if(_repo.SaveChanges())
            {
                return Ok(professor);
            }
            
            return BadRequest("Professor não cadastrado!");
        }

        // api/professor/
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _repo.GetProfessorById(id);
            if(prof == null) return BadRequest("professor não encontrado!");

            _repo.Update(professor);
            if(_repo.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não atualizado!");
        }

        // api/professor/
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _repo.GetProfessorById(id);
            if(prof == null) return BadRequest("professor não encontrado!");

            _repo.Update(professor);
            if(_repo.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não atualizado!");
        }

        // api/professor/
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorById(id);
            if(professor == null) return BadRequest("professor não encontrado!");

            _repo.Delete(professor);
            if (_repo.SaveChanges())
            {
                return Ok("Dados excluídos com sucesso!");                
            }  

            return BadRequest("Professor não deletado!");
        }
    }
}