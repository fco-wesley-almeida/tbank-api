using Core;
using Core.Domains.Contas.Dtos;
using Core.Domains.Transacoes.Dtos;
using FluentValidation;

namespace Business.Validators
{
    public class SolicitacaoTransacaoReceitaValidator: AbstractValidator<SolicitacaoTransacaoReceitaDto>
    {
        public SolicitacaoTransacaoReceitaValidator()
        {
            RuleFor(c => c.ContaId)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
            ;
            RuleFor(c => c.Descricao)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .MaximumLength(100)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(1, 100))
            ;
            RuleFor(c => c.Valor)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
            ;
        }
    }
}