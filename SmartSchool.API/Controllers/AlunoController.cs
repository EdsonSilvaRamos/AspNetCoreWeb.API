using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/aluno")]
    public class AlunoController : ControllerBase
    {
        public AlunoController(){}   

          public List<Aluno> Alunos = new List<Aluno>{
            new Aluno{ Id = 1, Nome = "Bravo", Sobrenome = "Bravesco", Telefone = "123456789"},
            new Aluno{ Id = 2, Nome = "Nuyu", Sobrenome = "MadKirby", Telefone = "9876543219"},
            new Aluno{ Id = 3, Nome = "Barão", Sobrenome = "Barones", Telefone = "543216789"},
        };         
       
       [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }

        // api/aluno/byId/1
         [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {   
            var aluno = Alunos.FirstOrDefault(a => a.Id == id);
            if(aluno == null) return BadRequest("O aluno não foi encontrado!");
            
            return Ok(aluno);
        }

         // api/aluno/1
         [HttpGet("byName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {   
            var aluno = Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));
            if(aluno == null) return BadRequest("O aluno não foi encontrado!");
            
            return Ok(aluno);
        }

         // api/aluno/
         [HttpPost]
        public IActionResult Post(Aluno aluno)
        {                           
            return Ok(aluno);
        }

        // api/aluno/
         [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {                           
            return Ok(aluno);
        }

         // api/aluno/
         [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {                           
            return Ok(aluno);
        }

          // api/aluno/
         [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {                           
            return Ok("Dados excluídos com sucesso!");
        }

    }
}