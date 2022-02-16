﻿using System;
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

        public ContasController(ILogger<ContasController> logger, IContaCadastroPessoaFisicaService cadastroPessoaFisicaService)
        {
            _logger = logger;
            _cadastroPessoaFisicaService = cadastroPessoaFisicaService;
        }

        [HttpPost("fisica")]
        public async Task<ActionResult<BaseResponse<long>>> CriarContaFisica(ContaPessoaFisicaCadastroDto request)
        {
            const string message = "Seu cadastro teve êxito";
            return await SecureResponse(message, () => _cadastroPessoaFisicaService.Cadastrar(request));
        }
        [HttpPost("juridica")]
        public async Task<ActionResult<string>> CriarContaJuridica()
        {
            return Ok("1");
        }
    }
}