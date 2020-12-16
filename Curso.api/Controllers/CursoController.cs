using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Curso.api.models.Cursos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Curso.api.Controllers
{
    [Route("api/v1/cursos")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        /// <summary>
        /// Este serviço permite cadastrar curso para o usuário autenticado.
        /// </summary>
        /// <returns>Retorna status 201 e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao Cadastrar um curso")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput)
        {
            var codidoUsuario = int.Parse((User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier))?.Value!);
            return Created("", cursoViewModelInput);
        }

        /// <summary>
        /// Este serviço permite cadastrar curso para o usuário autenticado.
        /// </summary>
        /// <returns>Retorna status 201 e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao obter dados de um curso")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var cursos = new List<CursoViewModelOutput>();
            // var codidoUsuario = int.Parse((User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier))?.Value);
            cursos.Add(new CursoViewModelOutput()
            {
                Login = "1",
                Descricao = "Teste",
                Nome = "Teste"
            });
            return Ok(cursos);
        }
    }
}