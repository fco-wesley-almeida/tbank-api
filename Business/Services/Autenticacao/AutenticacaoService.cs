using Core.CoreServices;
using Core.Data;
using Core.Domains.Autenticacao.Dtos;
using Core.Domains.Autenticacao.Services;
using Core.Exceptions;

namespace Business.Services.Autenticacao
{
    public class AutenticacaoService: IAutenticacaoService
    {
        private ILoginDb _loginDb;
        private UsuarioParaAutenticarDto _usuario;
        private LoginRequestDto _request;
        private IPasswordEncoder _passwordEncoder;
        private IJwtTokenGenerator _jwtTokenGenerator;

        public AutenticacaoService(ILoginDb loginDb, IPasswordEncoder passwordEncoder, IJwtTokenGenerator jwtTokenGenerator)
        {
            _loginDb = loginDb;
            _passwordEncoder = passwordEncoder;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public LoginResponseDto Login(LoginRequestDto request)
        {
            _request = request;
            FindUsuario();
            EvaluatePassword();
            return new LoginResponseDto
            {
                Token = GenerateToken(),
                UserId = _usuario.UsuarioId
            };
        }

        private void FindUsuario()
        {
            _usuario = _loginDb.FindUsuarioParaAutenticar(_request);
        }
        private void EvaluatePassword()
        {
            bool userHasFound = _usuario.UsuarioId > 0;
            if (!userHasFound || !PasswordIsValid())
            {
                throw new UnauthorizedException();
            }
        }

        private bool PasswordIsValid()
        {
            return _usuario.Senha == _passwordEncoder.Encode(_request.Senha);
        }

        private string GenerateToken()
        {
            return _jwtTokenGenerator.GenerateJwtToken(_usuario.UsuarioId.ToString(), "auth");
        }
    }
}