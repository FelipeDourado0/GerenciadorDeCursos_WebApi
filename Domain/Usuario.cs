using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorCursos.ValueObjects;

namespace GerenciadorCursos.Domain
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public CargoUsuario Cargo { get; set; }
    }
}