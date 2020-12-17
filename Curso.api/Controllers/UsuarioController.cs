using Curso.api.Business.Entities;
using Curso.api.Business.Repositories;
using Curso.api.Filters;
using Curso.api.models;
using Curso.api.models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace Curso.api.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthenticationService _authenticationService;

        public UsuarioController(IUsuarioRepository usuarioRepository, IConfiguration configuration,
            IAuthenticationService authenticationService)
        {
            _usuarioRepository = usuarioRepository;
            _authenticationService = authenticationService;
        }


        /// <summary>
        /// Este serviço permite autenticar um usuário cadastrado e ativo.
        /// </summary>
        /// <param name="loginViewModelInput"> View Model do login</param>
        /// <returns>Retorna status ok, dados do usuario e o token em caso de sucesso</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios não informados",
            Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            var usuario = _usuarioRepository.ObterUsuario(loginViewModelInput.Login);
            if (usuario == null)
            {
                return BadRequest("Houve um erro ao tentar acessar.");
            }

            // if (usuario.Senha != loginViewModelInput.Senha.GerarSenhaCriptografada())
            // {
            //     return BadRequest("Houve um erro ao tentar acessar.");
            // }

            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                Codigo = usuario.Codigo,
                Login = loginViewModelInput.Login,
                Email = usuario.Email
            };

            var token = _authenticationService.GerarToken(usuarioViewModelOutput);
            return Ok(new
            {
                Token = token,
                Usuario = usuarioViewModelOutput
            });
        }

        /// <summary>
        /// Este serviço permite registrar um usuário.
        /// </summary>
        /// <param name="registrarViewModelInput"> View Model do registrar</param>
        /// <returns>Retorna status ok, dados do usuario e o token em caso de sucesso</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao registrar", Type = typeof(RegistrarViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios não informados",
            Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(RegistrarViewModelInput registrarViewModelInput)
        {
            // var migracoesPendentes = context.Database.GetPendingMigrations();
            // if (migracoesPendentes.Any())
            // {
            //     context.Database.Migrate();
            // }

            var usuario = new Usuario();
            usuario.Login = registrarViewModelInput.Login;
            usuario.Email = registrarViewModelInput.Email;
            usuario.Senha = registrarViewModelInput.Senha;
            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();

            return Created("", registrarViewModelInput);
        }
    }
}