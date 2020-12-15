using Microsoft.AspNetCore.Mvc;

namespace Curso.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET
        public IActionResult Logar()
        {
            return View();
        }
    }
}