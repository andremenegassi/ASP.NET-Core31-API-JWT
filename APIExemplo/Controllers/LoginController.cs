using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace APIExemplo.Controllers
{
    [Authorize("ValidaUsuario")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppSettings _appSettings;

        public LoginController(AppSettings appSettings)
        {
            _appSettings = appSettings;

        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Logar([FromBody] System.Text.Json.JsonElement dados, [FromServices] SigningConfiguration signingConfiguration)
        {
            string usuario = dados.GetProperty("usuario").ToString();
            string senha = dados.GetProperty("senha").ToString();

            //obteve usuário.. Nome, email....
            string nome = "Andre Menegassi";
            string email = "aaa@aaa.com";
            string id = "123456";

            if (usuario == "adm" && senha == "123")
            {
                var ig = new GenericIdentity(id, "id");
                var isecEMail = new Claim(JwtRegisteredClaimNames.Email, email);
                var isecNome = new Claim(JwtRegisteredClaimNames.GivenName, nome);

                var identidade = new ClaimsIdentity(ig, new Claim[] { isecEMail, isecNome });

                //var provider = new RSACryptoServiceProvider(2048);
                //SecurityKey chave = new RsaSecurityKey(provider.ExportParameters(true));

                var handler = new JwtSecurityTokenHandler();
                var dadosToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
                {
                    Audience = _appSettings.Audencie,
                    Issuer = _appSettings.Issuer,
                    NotBefore = DateTime.Now,
                    Expires = DateTime.Now.AddDays(_appSettings.Days),
                    Subject = identidade,
                    SigningCredentials = signingConfiguration.SigningCredentials
                });

                string token = handler.WriteToken(dadosToken);

                return Ok(new
                {
                    operacao = true,
                    token
                });

            }
            else return NotFound(new
            {
                operacao = false,
            });

        }

    }
}