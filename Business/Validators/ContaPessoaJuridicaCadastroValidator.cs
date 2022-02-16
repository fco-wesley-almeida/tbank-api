using Core;
using Core.Domains.Contas.Dtos;
using FluentValidation;

namespace Business.Validators
{
    public class ContaPessoaJuridicaCadastroValidator: AbstractValidator<ContaPessoaJuridicaCadastroDto>
    {
        public ContaPessoaJuridicaCadastroValidator()
        {
            RuleFor(c => c.Cnpj)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Must(CustomValidations.ValidateCnpj)
                .WithMessage("CNPJ inválido.")
            ;
            RuleFor(c => c.NomeFantasia)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .MaximumLength(100)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(1, 100))
            ;
            RuleFor(c => c.RazaoSocial)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .MaximumLength(100)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(1, 100))
            ;
            RuleFor(c => c.Senha)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .MaximumLength(100)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(1, 100))
            ;
            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .EmailAddress()
                .WithMessage(DefaultErrorMessages.InvalidEmail)
                .MaximumLength(100)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(1, 100))
            ;
            RuleFor(c => c.Telefone)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .MaximumLength(30)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(1, 30))
            ;
            RuleFor(c => c.Cep)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Must(CustomValidations.ValidateCep)
                .WithMessage(DefaultErrorMessages.InvalidCep)
            ;
            RuleFor(c => c.Estado)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .MaximumLength(100)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(1, 100))
            ;
            RuleFor(c => c.Cidade)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .MaximumLength(100)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(1, 100))
            ;
            RuleFor(c => c.Distrito)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .MaximumLength(100)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(1, 100))
            ;
            RuleFor(c => c.Logradouro)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .MaximumLength(100)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(1, 100))
            ;
            RuleFor(c => c.Numero)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .MaximumLength(50)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(1, 50))
            ;
            RuleFor(c => c.Referencias)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .MaximumLength(100)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(1, 100))
            ;
        }
    }
}