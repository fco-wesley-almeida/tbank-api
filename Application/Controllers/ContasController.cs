using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Services.Contas;
using Core.Domains.Contas.Dtos;
using Core.Domains.Contas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
    [ApiController,Route("contas")]
    public class ContasController : ControllerBase
    {
        private readonly ILogger<ContasController> _logger;
        private readonly IContaCadastroPessoaFisicaService _cadastroPessoaFisicaService;

        public ContasController(ILogger<ContasController> logger, IContaCadastroPessoaFisicaService cadastroPessoaFisicaService)
        {
            _logger = logger;
            _cadastroPessoaFisicaService = cadastroPessoaFisicaService;
        }

        [HttpPost("fisica")]
        public async Task<ActionResult<long>> CriarContaFisica()
        {
            long contaId = await _cadastroPessoaFisicaService.Cadastrar(new ContaPessoaFisicaCadastroDto());
            return contaId;
        }
        [HttpPost("juridica")]
        public async Task<ActionResult<string>> CriarContaJuridica()
        {
            return Ok("1");
        }
    }
}