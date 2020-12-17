using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Curso.api.Business.Repositories;
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
        private readonly ICursoRepository _cursoRepository;

        public CursoController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

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
            Business.Entities.Curso curso = new Business.Entities.Curso();
            curso.Nome = cursoViewModelInput.Nome;
            curso.Descricao = cursoViewModelInput.Descricao;
            var codidoUsuario = int.Parse((User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier))?.Value!);
            curso.CodigoUsuario = codidoUsuario;
            _cursoRepository.Adicionar(curso);
            _cursoRepository.Commit();
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
            var codidoUsuario = int.Parse((User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier))?.Value);
            var cursos = _cursoRepository.ObterPorUsuario(codidoUsuario).Select(s => new CursoViewModelOutput()
            {
                Nome = s.Nome,
                Descricao = s.Descricao,
                Login = s.Usuario.Login
            });

            return Ok(cursos);
        }
    }
}