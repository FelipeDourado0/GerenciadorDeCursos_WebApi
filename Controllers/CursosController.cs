using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GerenciadorCursos.Data;
using GerenciadorCursos.Domain;
using Microsoft.AspNetCore.Authorization;
using GerenciadorCursos.ValueObjects;
using GerenciadorCursos.Repository;

namespace GerenciadorCursos.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly ICursosRepository _repository;

        public CursosController(ApplicationContext context, ICursosRepository repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet("ListarCursos")]
        public async Task<ActionResult<IEnumerable<Curso>>> ListarCurosos()
        {
            var retorno = await _repository.ListarCurososAsync();

            return Ok(retorno);
        }

        [AllowAnonymous]
        [HttpPost("CursosPorStatus")]
        public async Task<ActionResult<IEnumerable<Curso>>> ListarCursosPorStatus(StatusCurso status)
        {
            var cursos = await _repository.ListarCursosPorStatusAsync(status);

            return Ok(cursos);
        }

        [Authorize]
        [HttpPut("AtualizarStatus")]
        public async Task<ActionResult<object>> AtualizarStatusCurso(int id, StatusCurso status)
        {
            var retorno = await _repository.AtualizarStatusCursoAsync(id,status);
            if(retorno == null)
                return NoContent();

            return Ok(retorno);
        }

        [Authorize(Roles = "Gerente")]
        [HttpDelete("DeletarCurso")]
        public async Task<ActionResult<object>> DeletarCurso(int id)
        {
            var resultado = await _repository.DeletarCursoAsync(id);
            if(resultado == null)
                return NoContent();

            return Ok(resultado);
        }

        // CADASTRAR CURSO
/*         [Authorize(Roles = "Gerente")]
        [HttpPost("CadastrarCurso")]
        public async Task<ActionResult<Curso>> PostCurso(Curso curso)
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurso", new { id = curso.Id }, curso);
        } */

    }
}
