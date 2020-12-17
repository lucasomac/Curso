using Curso.api.Business.Entities;

namespace Curso.api.Business.Repositories
{
    public interface IUsuarioRepository
    {
        public void Adicionar(Usuario usuario);
        public void Commit();
        public Usuario ObterUsuario(string login);
    }
}