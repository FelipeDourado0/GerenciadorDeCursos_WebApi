using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorCursos.Data;
using GerenciadorCursos.Domain;
using GerenciadorCursos.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCursos.Repository
{
    public class CursosRepository : ICursosRepository
    {
        private readonly ApplicationContext _context;
        
        public CursosRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Curso>>> ListarCurososAsync()
        {
            return await _context.Cursos.ToListAsync();
        }
        
        public async Task<ActionResult<IEnumerable<Curso>>> ListarCursosPorStatusAsync(StatusCurso status)
        {
            return await _context.Cursos.AsNoTracking().Where(c => c.Status == status).ToListAsync();
        }

        public async Task<ActionResult<string>> AtualizarStatusCursoAsync(int id, StatusCurso status)
        {
            if (!CursoExists(id))
            {    
                return null;
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
                    return null;
                }
                else
                {
                    throw;
                }
            }
            var mensagemSucesso = "Status Atualizado!";
            return mensagemSucesso;
        }

        public async Task<ActionResult<string>> DeletarCursoAsync(int id)
        {
            var mensagemSucesso = "Curso Deletado!";
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return null;
            }

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return mensagemSucesso;
        }

        public async Task<ActionResult> CadastrarCursoAsync(Curso curso)
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return null;
        }
        private bool CursoExists(int id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }

    }
}