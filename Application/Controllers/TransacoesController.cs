using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Services.Transacoes;
using Core;
using Core.Domains.Transacoes.Dtos;
using Core.Domains.Transacoes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
    [ApiController,Route("transacoes")]
    public class TransacoesController : Controller
    {
        private readonly ILogger<TransacoesController> _logger;
        private readonly ISolicitacaoTransacaoDebitoService _solicitacaoTransacaoDebitoService;

        public TransacoesController(
            ILogger<TransacoesController> logger,
            ISolicitacaoTransacaoDebitoService solicitacaoTransacaoDebitoService
        )
        {
            _logger = logger;
            _solicitacaoTransacaoDebitoService = solicitacaoTransacaoDebitoService;
        }

        [HttpPost("debito")]
        public async Task<ActionResult<BaseResponse<long>>> SolicitarDebitoConta(SolicitacaoTransacaoDebitoDto request)
        {
            const string message = "Sua transação foi executada com sucesso.";
            return await SecureResponse(message, () => _solicitacaoTransacaoDebitoService.Solicitar(request));
        }
    }
}