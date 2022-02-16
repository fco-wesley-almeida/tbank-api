using System.Threading.Tasks;
using Core;
using Core.Domains.Transacoes.Dtos;
using Core.Domains.Transacoes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
    [ApiController,Route("transacoes")]
    public class TransacoesController : Controller
    {
        private readonly ILogger<TransacoesController> _logger;
        private readonly ISolicitacaoTransacaoDebitoService _solicitacaoTransacaoDebitoService;
        private readonly ISolicitacaoTransacaoCreditoService _solicitacaoTransacaoCreditoService;

        public TransacoesController(
            ILogger<TransacoesController> logger,
            ISolicitacaoTransacaoDebitoService solicitacaoTransacaoDebitoService, ISolicitacaoTransacaoCreditoService solicitacaoTransacaoCreditoService)
        {
            _logger = logger;
            _solicitacaoTransacaoDebitoService = solicitacaoTransacaoDebitoService;
            _solicitacaoTransacaoCreditoService = solicitacaoTransacaoCreditoService;
        }

        [HttpPost("debito")]
        public async Task<ActionResult<BaseResponse<long>>> SolicitarDebitoConta(SolicitacaoTransacaoDebitoDto request)
        {
            const string message = "Sua transação foi executada com sucesso.";
            return await SecureResponse(message, () => _solicitacaoTransacaoDebitoService.Solicitar(request));
        }
        [HttpPost("credito")]
        public async Task<ActionResult<BaseResponse<long>>> SolicitarCreditoConta(SolicitacaoTransacaoCreditoDto request)
        {
            const string message = "Sua transação foi executada com sucesso.";
            return await SecureResponse(message, () => _solicitacaoTransacaoCreditoService.Solicitar(request));
        }
    }
}