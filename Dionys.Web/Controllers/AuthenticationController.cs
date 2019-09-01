using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dionys.Web.App.Jwt;
using Dionys.Web.Models.ViewModels.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Dionys.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IJwtSigningEncodingKey _signingEncodingKey;

        public AuthenticationController(IJwtSigningEncodingKey signingEncodingKey)
        {
            _signingEncodingKey = signingEncodingKey;
        }

        [AllowAnonymous]
        public ActionResult<string> Post(AuthenticationRequest authRequest)
        {
            // 1. Проверяем данные пользователя из запроса.
            // ...

            // 2. Создаем утверждения для токена.
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, authRequest.Name)
            };

            // 3. Генерируем JWT.
            var token = new JwtSecurityToken(
                issuer: "DemoApp",
                audience: "DemoAppClient",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: new SigningCredentials(
                    _signingEncodingKey.GetKey(),
                    _signingEncodingKey.SigningAlgorithm)
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}
