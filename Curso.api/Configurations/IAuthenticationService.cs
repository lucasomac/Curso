using Curso.api.models.Usuarios;

namespace Curso.api.Controllers
{
    public interface IAuthenticationService
    {
        string GerarToken(UsuarioViewModelOutput usuarioViewModelOutput);
    }
}