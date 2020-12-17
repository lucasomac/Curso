using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Curso.api.Business.Repositories;

namespace Curso.api.Infraestructure.Data.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly CursoDbContext _context;

        public CursoRepository(CursoDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Business.Entities.Curso curso)
        {
            _context.Curso.Add(curso);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IList<Business.Entities.Curso> ObterPorUsuario(int codigoUsuario)
        {
            return _context.Curso.Include(i => i.Usuario).Where(w => w.CodigoUsuario == codigoUsuario).ToList();
        }
    }
}