using AutoMapper;
using gestao_pessoas_back.Exceptions;
using gestao_pessoas_back.Model;
using gestao_pessoas_back.Reponse;
using gestao_pessoas_back.Repository.Interfaces;
using gestao_pessoas_back.Requests.Usuario;
using gestao_pessoas_back.Service.Interfaces;
using gestao_pessoas_back.Utils;

namespace gestao_pessoas_back.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly JwtService _jwtService;
        private readonly ILogger<UsuarioService> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            JwtService jwtService,
            ILogger<UsuarioService> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _usuarioRepository = usuarioRepository;
            _jwtService = jwtService;
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponse?> AutenticarUsuario(LoginRequest request)
        {
            try
            {
                var usuario = await _usuarioRepository.ObterPorEmailAsync(request.Email);

                if (usuario == null)
                {
                    _logger.LogWarning("Tentativa de login com email não encontrado: {Email}", request.Email);
                    throw new InvalidOperationException("Não existe um usuário cadastrado com este email.");
                }

                if (!PasswordHasher.Verify(request.Senha, usuario.SenhaHash))
                    throw new BusinessException("Credenciais inválidas.");

                var token = _jwtService.GerarToken(usuario);

                _logger.LogInformation("Login realizado com sucesso para o usuário: {Email} com role: {Role}", usuario.Email, usuario.Role);

                return new LoginResponse
                {
                    tokenType = "Bearer",
                    AccessToken = token,
                    ExpiresIn = _jwtService.GetExpiration() 
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao autenticar usuário: {Email}", request.Email);
                throw ;
            }
        }

        public async Task<UsuarioResponse> CriarUsuario(CriarUsuarioRequest request)
        {
            try
            {
                if (await _usuarioRepository.VerificarEmailExisteAsync(request.Email))
                {
                    throw new InvalidOperationException("Já existe um usuário cadastrado com este email.");
                }

                var usuario = _mapper.Map<UsuarioModel>(request);
                usuario.SenhaHash = PasswordHasher.Hash(request.Senha);

                var usuarioCriado = await _usuarioRepository.CriarAsync(usuario);
                var usuarioResponse = _mapper.Map<UsuarioResponse>(usuarioCriado);

                _logger.LogInformation("Usuário criado com sucesso: {Email} com role: {Role}", usuarioCriado.Email, usuarioCriado.Role);
                return usuarioResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar usuário: {Email}", request.Email);
                throw;
            }
        }

        public async Task<UsuarioResponse?> ObterInformacoesUsuario()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null)
                {
                    _logger.LogWarning("HttpContext não disponível");
                    return null;
                }

                var userId = _jwtService.ObterUserIdDoContexto(httpContext);
                if (!userId.HasValue)
                {
                    _logger.LogWarning("Não foi possível obter o ID do usuário do token");
                    return null;
                }

                var usuario = await _usuarioRepository.ObterPorIdAsync(userId.Value);
                if (usuario == null)
                {
                    _logger.LogWarning("Usuário não encontrado para o ID: {UserId}", userId);
                    return null;
                }

                var usuarioResponse = _mapper.Map<UsuarioResponse>(usuario);
                _logger.LogInformation("Informações do usuário obtidas com sucesso: {Email}", usuario.Email);
                return usuarioResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter informações do usuário");
                throw;
            }
        }

      

       
    }
}