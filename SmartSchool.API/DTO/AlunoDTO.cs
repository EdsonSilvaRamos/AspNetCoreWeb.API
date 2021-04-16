using System;

namespace SmartSchool.API.DTO
{
    public class AlunoDTO
    {
        public int Id { get; set; }

        public int Matricula { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public DateTime DataInicio { get; set; }

        public string Telefone { get; set; }
        public bool Ativo { get; set; }
    }
}