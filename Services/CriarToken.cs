using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GerenciadorCursos.Domain;
using GerenciadorCursos.Settings;
using GerenciadorCursos.ValueObjects;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GerenciadorCursos.Services
{
   public class CriarToken
   {
      private readonly ConfiguracoesJWT configuracoesJWT;
      public CriarToken(IOptions<ConfiguracoesJWT> opcoes)
      {
         configuracoesJWT = opcoes.Value;
      }
      public string GerarToken(Usuario usuario)
      {
         
         var handler = new JwtSecurityTokenHandler();
         string role;
         if(usuario.Cargo == CargoUsuario.Gerente){
            role = "Gerente";
         }else{
            role = "Secretaria";
         } 

         IList<Claim> AdmClaims = new List<Claim>();
         AdmClaims.Add(new Claim(ClaimTypes.Role, role));
         AdmClaims.Add(new Claim("nome", usuario.Nome));       

         var tokenDescriptor = new SecurityTokenDescriptor
         {
            //SEMPRE QUE MUDAR A DESCRIÇÃO, VALIDAR ELA NO SETUP
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuracoesJWT.Segredo)),SecurityAlgorithms.HmacSha256Signature),
            Audience = "https://localhost:5001",
            Issuer = "User",
            Subject = new ClaimsIdentity(AdmClaims)
         };

         SecurityToken token = handler.CreateToken(tokenDescriptor);//Retorna o security Token

         return handler.WriteToken(token);
      }
   }
}