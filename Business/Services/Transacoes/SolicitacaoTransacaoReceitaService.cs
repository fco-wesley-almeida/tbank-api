using System;
using System.Collections.Generic;
using Core.Data;
using Core.Domains.Transacoes.Dtos;
using Core.Domains.Transacoes.Services;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using FluentValidation;
using FluentValidation.Results;

namespace Business.Services.Transacoes
{
    public class SolicitacaoTransacaoReceitaService: ISolicitacaoTransacaoReceitaService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IValidator<SolicitacaoTransacaoReceitaDto> _validator;

        private SolicitacaoTransacaoReceitaDto _request;
        private Transacao _transacao;
        private TransacaoReceita _transacaoReceita;
        public SolicitacaoTransacaoReceitaService(ITransacaoRepository transacaoRepository, IValidator<SolicitacaoTransacaoReceitaDto> validator, IContaSaldoDb contaSaldoDb)
        {
            _transacaoRepository = transacaoRepository;
            _validator = validator;
        }

        public long Solicitar(SolicitacaoTransacaoReceitaDto request)
        {
            _request = request;
            ValidateFormat();
            MapEntities();
            Persist();
            return _transacao.Id;
        }

        private void ValidateFormat()
        {
            ValidationResult validationResult = _validator.Validate(_request);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult);
            }
        }

        private void MapEntities()
        {
            MapTransacao();
        }

        private void MapTransacao()
        {
            _transacao = new Transacao();
            _transacao.ContaId = (int)_request.ContaId;
            _transacao.Codigo = Guid.NewGuid().ToString();
            _transacao.Data = DateTime.Now;
            _transacao.Time = DateTime.Now.TimeOfDay;
            _transacao.Valor = (decimal) _request.Valor;
            _transacao.Descricao = _request.Descricao;
            _transacao.TransacaoReceita = new List<TransacaoReceita> { MapTransacaoReceita() };
        }

        private TransacaoReceita MapTransacaoReceita()
        {
            _transacaoReceita = new TransacaoReceita();
            _transacaoReceita.Transacao = _transacao;
            return _transacaoReceita;
        }

        private void Persist()
        {
            _transacaoRepository.Create(_transacao);
        }
    }
}