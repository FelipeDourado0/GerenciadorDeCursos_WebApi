using System.Threading.Tasks;
using GerenciadorCursos.Data;
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

/*         [HttpDelete]
        public async Task<IActionResult> DeletarTodos()
        {
            var db = new ApplicationContext();
            for(int i = 1; i <= 6; i++){
                var remover = await db.Usuarios.FindAsync(i);
                db.Usuarios.Remove(remover);
                await db.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> CadastrarUsuario(Usuario usuario)
        {
            var db = new ApplicationContext();
            db.Add(usuario);
            await db.SaveChangesAsync();
            
            return Ok();
        } */

    }
}