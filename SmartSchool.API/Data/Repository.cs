using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Helpers;
using SmartSchool.API.Models;

namespace SmartSchool.API.Data
{
    public class Repository : IRepository
    {
        public SmartContext _context;
        public Repository(SmartContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public async Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                                .ThenInclude(ad => ad.Disciplina)
                                .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            if(!string.IsNullOrEmpty(pageParams.Nome))
            {
                query = query.Where(
                    aluno => aluno.Nome.ToUpper().Contains(pageParams.Nome.ToUpper()) ||
                             aluno.SobreNome.ToUpper().Contains(pageParams.Nome.ToUpper())
                );
            }

            if(pageParams.Ativo != null)
            {
                if(pageParams.Ativo == true)
                {
                    query = query.Where(aluno => aluno.Ativo == true);
                }
                else
                {
                    query = query.Where(aluno => aluno.Ativo == false);
                }  
            } 

            if(pageParams.Matricula > 0)
            {
                query = query.Where(aluno => aluno.Matricula == pageParams.Matricula);
            }

            //return await query.ToArrayAsync();
            return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                                .ThenInclude(ad => ad.Disciplina)
                                .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDiciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                                .ThenInclude(ad => ad.Disciplina)
                                .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                            .OrderBy(a => a.Id)
                            .Where(aluno => aluno.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId));

            return query.ToArray();
        }

        public Aluno GetAlunoById(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                                .ThenInclude(ad => ad.Disciplina)
                                .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                            .OrderBy(a => a.Id)
                            .Where(aluno => aluno.Id == alunoId);

            return query.FirstOrDefault();
        }

        public Professor[] GetAllProfessores(bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(p => p.Disciplinas)
                                .ThenInclude(d => d.AlunosDisciplinas)
                                .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return query.ToArray();
        }

        public Professor[] GetAllProfessoresByDiciplinaId(int disciplinaId, bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(p => p.Disciplinas)
                                .ThenInclude(d => d.AlunosDisciplinas)
                                .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()
                            .OrderBy(aluno => aluno.Id)
                            .Where(aluno => aluno.Disciplinas.Any(
                                d => d.AlunosDisciplinas.Any(
                                    ad => ad.DisciplinaId == disciplinaId
                                )
                            ));

            return query.ToArray();
        }

        public Professor GetProfessorById(int professorId, bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(p => p.Disciplinas)
                                .ThenInclude(d => d.AlunosDisciplinas)
                                .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()
                            .OrderBy(p => p.Id)
                            .Where(professor => professor.Id == professorId);

            return query.FirstOrDefault();
        }
    }
}