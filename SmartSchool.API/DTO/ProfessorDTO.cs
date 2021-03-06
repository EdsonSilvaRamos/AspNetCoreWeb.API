using System;

namespace SmartSchool.API.DTO
{
    public class ProfessorDTO
    {
        public int Id { get; set; }
        public int Registro { get; set; }
        public string Nome { get; set; }
        public string  Telefone { get; set; }
        public DateTime DataInicio { get; set; } 
        public bool Ativo { get; set; }
    }
}