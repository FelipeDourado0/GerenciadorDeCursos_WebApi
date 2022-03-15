using System.Threading.Tasks;
using GerenciadorCursos.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCursos.Repository
{
    public interface ILoginRepository
    {
        public Task<object> AutenticarLoginAsync( int id);
    }
}