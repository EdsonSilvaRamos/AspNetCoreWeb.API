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
         private readonly SmartContext _context;
        public ProfessorController(SmartContext context) {                 
            _context = context;            
        }  
       
       [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }

        // api/aluno/byId/1
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _context.Professores.FirstOrDefault(a => a.Id == id);
            if (professor == null) return BadRequest("O professor não foi encontrado!");

            return Ok(professor);
        }

        // api/professor/1
        [HttpGet("byName")]
        public IActionResult GetByName(string nome)
        {
            var professor = _context.Professores.FirstOrDefault(a => a.Nome.Contains(nome));
            if (professor == null) return BadRequest("O professor não foi encontrado!");

            return Ok(professor);
        }

        // api/professor/
        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        // api/professor/
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var alu = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(alu == null) return BadRequest("professor não encontrado");

            _context.Update(professor);
            _context.SaveChanges();

            return Ok(professor);
        }

        // api/professor/
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var alu = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(alu == null) return BadRequest("professor não encontrado");

            _context.Update(professor);
            _context.SaveChanges();

            return Ok(professor);
        }

        // api/professor/
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _context.Professores.FirstOrDefault(a => a.Id == id);
            if(professor == null) return BadRequest("professor não encontrado");

            _context.Remove(professor);
            _context.SaveChanges();

            return Ok("Dados excluídos com sucesso!");
        }

    }
}