using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciadorCursos.Data;
using GerenciadorCursos.Domain;
using Microsoft.AspNetCore.Authorization;
using GerenciadorCursos.ValueObjects;

namespace GerenciadorCursos.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CursosController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Cursos
        [AllowAnonymous]
        [HttpGet("ListaCursos")]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursos()
        {
            return await _context.Cursos.ToListAsync();
        }

        [AllowAnonymous]
        [HttpPost("CursosPorStatus")]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursosPorStatus(StatusCurso status)
        {
            //Forma inicial. Passível de análise de perfomance! 

            /* List<Curso> listaCursos = new List<Curso>();
            await _context.Cursos.ForEachAsync(c =>{
                if(c.Status == status)
                    listaCursos.Add(c);  
            }); */
            
            //Forma que eu achei mais performático.
            return await _context.Cursos.AsNoTracking().Where(c => c.Status == status).ToListAsync();
        }

        // Modificar Curso
        [Authorize]
        [HttpPut("AtualizarStatus")]
        public async Task<IActionResult> PutStatusCurso(int id, StatusCurso status)
        {
            
            if (!CursoExists(id))
            {
                return BadRequest();
            }

            _context.Cursos.Find(id).Status = status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cursos
        [Authorize(Roles = "Gerente")]
        [HttpPost]
        public async Task<ActionResult<Curso>> PostCurso(Curso curso)
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurso", new { id = curso.Id }, curso);
        }

        // DELETE: api/Cursos/5
        [Authorize(Roles = "Gerente")]
        [HttpDelete("DeletarCurso")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CursoExists(int id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }
    }
}
