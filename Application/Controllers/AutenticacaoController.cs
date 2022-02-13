using System;
using System.Threading.Tasks;
using Core;
using Core.Domains.Autenticacao.Dtos;
using Core.Domains.Autenticacao.Services;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
    [ApiController,Route("autenticacao")]
    public class AutenticacaoController : Controller
    {
        private readonly ILogger<AutenticacaoController> _logger;
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(ILogger<AutenticacaoController> logger, IAutenticacaoService autenticacaoService)
        {
            _logger = logger;
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseResponse<LoginResponseDto>>> Login(LoginRequestDto request)
        {
            return await SecureResponse("Acesso autorizado", () => _autenticacaoService.Login(request));
        }
    }
}