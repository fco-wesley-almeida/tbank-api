using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Services.Contas;
using Core;
using Core.Domains.Contas.Dtos;
using Core.Domains.Contas.Services;
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

        public ContasController(
            ILogger<ContasController> logger,
            IContaCadastroPessoaFisicaService cadastroPessoaFisicaService,
            IContaCadastroPessoaJuridicaService contaCadastroPessoaJuridicaService
        )
        {
            _logger = logger;
            _cadastroPessoaFisicaService = cadastroPessoaFisicaService;
            _contaCadastroPessoaJuridicaService = contaCadastroPessoaJuridicaService;
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
    }
}