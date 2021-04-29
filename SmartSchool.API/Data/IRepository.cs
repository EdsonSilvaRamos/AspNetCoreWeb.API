using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSchool.API.Helpers;
using SmartSchool.API.Models;

namespace SmartSchool.API.Data
{
    public interface IRepository
    {
         void Add<T>(T entity) where T : class;

         void Update<T>(T entity) where T : class;
         
         void Delete<T>(T entity) where T : class;

         bool SaveChanges();

        //Alunos
         Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false);
         Aluno[] GetAllAlunos(bool includeProfessor = false);

         Aluno[] GetAllAlunosByDiciplinaId(int disciplinaId, bool includeProfessor = false);

         Aluno GetAlunoById(int alunoId, bool includeProfessor = false);

         //Professores
         Professor[] GetAllProfessores(bool includeAluno = false);

         Professor[] GetAllProfessoresByDiciplinaId(int disciplinaId, bool includeAluno = false);

         Professor GetProfessorById(int professorId, bool includeAluno = false);

    }
}