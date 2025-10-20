using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using gestao_pessoas_back.Model;
using gestao_pessoas_back.Settings;
using Microsoft.Extensions.Options;

namespace gestao_pessoas_back.Services
{
    public class JwtService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<JwtService> _logger;

        public JwtService(IOptions<JwtSettings> jwtSettings, ILogger<JwtService> logger)
        {
            _jwtSettings = jwtSettings.Value;
            _logger = logger;

            if (string.IsNullOrEmpty(_jwtSettings.SecretKey))
                throw new ArgumentNullException(nameof(_jwtSettings.SecretKey), "Chave secreta não configurada");

            if (_jwtSettings.SecretKey.Length < 32)
                _logger.LogWarning("Chave secreta muito curta. Recomendado mínimo 32 caracteres");
        }

        public string GerarToken(UsuarioModel usuario)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

               
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Role.ToString()),
                    new Claim("id", usuario.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.ExpirationSeconds),
                    Issuer = _jwtSettings.Issuer,
                    Audience = _jwtSettings.Audience,
                   
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256) 
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

               
                var tokenString = tokenHandler.WriteToken(token);

                _logger.LogInformation("Token JWT gerado com sucesso para o usuário: {Email} com role: {Role}", usuario.Email, usuario.Role);
                _logger.LogDebug("Tamanho da chave: {KeySize} bits", key.Length * 8);

                return tokenString;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar token JWT para o usuário: {Email}", usuario.Email);
                throw;
            }
        }

        public ClaimsPrincipal? ValidarToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                _logger.LogDebug("Token JWT validado com sucesso");
                return principal;
            }
            catch (SecurityTokenException ex)
            {
                _logger.LogWarning(ex, "Token JWT inválido");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao validar token JWT");
                return null;
            }
        }

        public int GetExpirationSeconds()
        {
            return _jwtSettings.ExpirationSeconds;
        }
        public int GetExpiration()
        {
            return _jwtSettings.ExpirationSeconds;
        }
        public int GetExpirationMinutes()
        {
            return _jwtSettings.ExpirationSeconds / 60;
        }

        public Guid? ObterUserIdDoContexto(HttpContext context)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                           ?? context.User.FindFirst("id")?.Value;

            if (Guid.TryParse(userIdClaim, out Guid userId))
            {
                return userId;
            }
            return null;
        }

        public string? ObterEmailDoContexto(HttpContext context)
        {
            return context.User.FindFirst(ClaimTypes.Email)?.Value;
        }

        public string? ObterRoleDoContexto(HttpContext context)
        {
            return context.User.FindFirst(ClaimTypes.Role)?.Value;
        }

        public string? ObterEmailDoToken(string token)
        {
            var principal = ValidarToken(token);
            return principal?.FindFirst(ClaimTypes.Email)?.Value;
        }

        public Guid? ObterUserIdDoToken(string token)
        {
            var principal = ValidarToken(token);
            var userIdClaim = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                           ?? principal?.FindFirst("id")?.Value;

            if (Guid.TryParse(userIdClaim, out Guid userId))
            {
                return userId;
            }
            return null;
        }

        public string? ObterRoleDoToken(string token)
        {
            var principal = ValidarToken(token);
            return principal?.FindFirst(ClaimTypes.Role)?.Value;
        }

        public bool TokenEstaExpirado(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                return jwtToken.ValidTo < DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar expiração do token");
                return true;
            }
        }
    }
}