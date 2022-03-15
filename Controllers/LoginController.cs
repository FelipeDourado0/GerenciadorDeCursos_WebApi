using System.Threading.Tasks;
using GerenciadorCursos.Domain;
using GerenciadorCursos.Repository;
using Microsoft.AspNetCore.Mvc;


namespace GerenciadorCursos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _repository;
        
        public LoginController(ILoginRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route(("login"))]
        public async Task<ActionResult<object>> AutenticarLogin( int id)
        { 
            var retorno = await _repository.AutenticarLoginAsync(id);
            
            return Ok(retorno);
        }

   
        [HttpPost("CadastrarUsuario")]
        public async Task<IActionResult> CadastrarUsuarioAsync(Usuario usuario)
        {
            var retorno = await _repository.CadastrarUsuarioAsync(usuario);
            
            return Ok();
        }

    }
}