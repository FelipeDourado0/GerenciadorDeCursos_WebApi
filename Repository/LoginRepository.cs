using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorCursos.Data;
using GerenciadorCursos.Services;
using GerenciadorCursos.Settings;
using Microsoft.Extensions.Options;

namespace GerenciadorCursos.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationContext _context;

        private readonly IOptions<ConfiguracoesJWT> _opcoes;
        public LoginRepository(ApplicationContext context, IOptions<ConfiguracoesJWT> opcoes)
        {
            _context = context;
            _opcoes = opcoes;
        }
        public async Task<object> AutenticarLoginAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return new { Message = "Usuário não encontrado." };


            CriarToken criarToken = new CriarToken(_opcoes);

            var token = criarToken.GerarToken(usuario);

            var retorno = new
            {
                Token = token,
            };

            return retorno;
        }
    }
}