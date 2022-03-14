using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorCursos.Data;
using GerenciadorCursos.Domain;
using GerenciadorCursos.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace GerenciadorCursos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        [HttpPost]
        [Route(("login"))]
        public ActionResult<Usuario> Authenticate( int id)
        {
            var db = new ApplicationContext();
            var usuario = db.Usuarios.Find(id);

            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });


            CriarToken criarToken = new CriarToken();

            var token = criarToken.GerarToken(usuario);

            var retorno = new
            {
                Token = token,
            };

            return Ok(retorno);
        }

        [HttpDelete]
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

    }
}