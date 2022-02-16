using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Services.Contas;
using Core;
using Core.Domains.Contas.Dtos;
using Core.Domains.Contas.Services;
using Core.Domains.Transacoes.Dtos;
using Core.Domains.Transacoes.Services;
using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
    [ApiController,Route("contas")]
    public class ContasController : Controller
    {
        private readonly ILogger<ContasController> _logger;
        private readonly IContaCadastroPessoaFisicaService _cadastroPessoaFisicaService;
        private readonly IContaCadastroPessoaJuridicaService _contaCadastroPessoaJuridicaService;
        private readonly ISolicitacaoTransacaoReceitaService _solicitacaoTransacaoReceitaService;
        private readonly TBankDbContext _tbankDbContext;

        public ContasController(
            ILogger<ContasController> logger,
            IContaCadastroPessoaFisicaService cadastroPessoaFisicaService,
            IContaCadastroPessoaJuridicaService contaCadastroPessoaJuridicaService, ISolicitacaoTransacaoReceitaService solicitacaoTransacaoReceitaService, TBankDbContext tbankDbContext)
        {
            _logger = logger;
            _cadastroPessoaFisicaService = cadastroPessoaFisicaService;
            _contaCadastroPessoaJuridicaService = contaCadastroPessoaJuridicaService;
            _solicitacaoTransacaoReceitaService = solicitacaoTransacaoReceitaService;
            _tbankDbContext = tbankDbContext;
        }

        [HttpPost("fisica")]
        public async Task<ActionResult<BaseResponse<long>>> CriarContaFisica(ContaPessoaFisicaCadastroDto request)
        {
            const string message = "Seu cadastro teve êxito.";
            return await SecureResponse(message, () => _cadastroPessoaFisicaService.Cadastrar(request));
        }
        [HttpPost("juridica")]
        public async Task<ActionResult<BaseResponse<long>>> CriarContaJuridica(ContaPessoaJuridicaCadastroDto request)
        {
            const string message = "Seu cadastro teve êxito.";
            return await SecureResponse(message, () => _contaCadastroPessoaJuridicaService.Cadastrar(request));
        }

         [HttpPost("depositar")]
         public async Task<ActionResult<BaseResponse<long>>> CriarContaJuridica(SolicitacaoTransacaoReceitaDto request)
         {
             const string message = "Seu cadastro teve êxito.";
             return await SecureResponse(message, () => _solicitacaoTransacaoReceitaService.Solicitar(request));
         }

          [HttpGet("{contaId}")]
          public async Task<ActionResult<Conta>> GetContaDetails(long contaId)
          {
             return _tbankDbContext.Conta.Find((int)contaId);
          }
        [HttpGet("{contaId}/faturas")]
        public async Task<ActionResult<List<Fatura>>> GetContaFaturas(long contaId)
        {
            return _tbankDbContext.Faturas.Where(f => f.ContaId == contaId).ToList();
        }
    }
}