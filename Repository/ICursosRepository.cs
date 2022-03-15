using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorCursos.Domain;
using GerenciadorCursos.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCursos.Repository
{
    public interface ICursosRepository
    {
        //Qualquer Usuário pode fazer;
        Task<ActionResult<IEnumerable<Curso>>> ListarCurososAsync();
        //Qualquer Usuário pode fazer.
        Task<ActionResult<IEnumerable<Curso>>> ListarCursosPorStatusAsync(StatusCurso status);
        //Apenas Gerente e Secretaria podem fazer.
        Task<ActionResult<string>> AtualizarStatusCursoAsync(int id, StatusCurso status);
        //Apenas Gerente pode fazer.
        Task<ActionResult<string>> DeletarCursoAsync(int id);

        Task<ActionResult> CadastrarCursoAsync(Curso curso);
    }
}