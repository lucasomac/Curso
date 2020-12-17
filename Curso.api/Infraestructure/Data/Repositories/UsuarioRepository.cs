using System.Linq;
using Curso.api.Business.Entities;
using Curso.api.Business.Repositories;
using Curso.api.models.Usuarios;

namespace Curso.api.Infraestructure.Data.Repositories
{
    /// <inheritdoc />
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CursoDbContext _context;

        public UsuarioRepository(CursoDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public Usuario ObterUsuario(string login)
        {
            return _context.Usuario.FirstOrDefault(u => u.Login == login);
        }
    }
}