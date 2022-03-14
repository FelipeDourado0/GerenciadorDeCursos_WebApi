using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorCursos.ValueObjects;

namespace GerenciadorCursos.Domain
{
    public class Curso
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int HorasDeDuracao { get; set; }
        public StatusCurso Status { get; set; }
    }
}