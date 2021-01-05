using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/professor")]
    public class ProfessorController : ControllerBase
    {
        public ProfessorController(){}   
       
       [HttpGet]
        public IActionResult Get()
        {
            return Ok("Professores: ");
        }
    }
}