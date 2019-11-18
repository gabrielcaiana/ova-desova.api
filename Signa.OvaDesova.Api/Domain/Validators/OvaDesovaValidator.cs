using FluentValidation;
using Signa.OvaDesova.Api.Domain.Models;

namespace Signa.Default.Template.Domain.Validators
{
    class PessoaValidator : AbstractValidator<OvaDesovaModel>
    {
        public PessoaValidator()
        {
            RuleFor(p => p.PessoaNome).NotNull().WithMessage("Preencha Nome");
            RuleFor(p => p.PessoaNomeFantasia).NotNull().WithMessage("Preencha Nome Fantasia");
            RuleFor(p => p.PessoaEmail).NotNull().WithMessage("Preencha Email");
            RuleFor(p => p.PessoaCnpjCpf).NotNull().WithMessage("Preencha Cpf/Cnpj");

            var tamanhoNome = 255;
            RuleFor(p => p.PessoaNome).MaximumLength(255).WithMessage($"Nome não pode ter mais de {tamanhoNome.ToString()} caracteres");

            var tamanhoNomeFantasia = 255;
            RuleFor(p => p.PessoaNomeFantasia).MaximumLength(255).WithMessage($"Nome não pode ter mais de {tamanhoNomeFantasia.ToString()} caracteres");

            var tamanhoCpfCnpj = 19;
            RuleFor(p => p.PessoaCnpjCpf).MaximumLength(tamanhoCpfCnpj).WithMessage($"Cnpj/Cpf não pode ter mais de {tamanhoCpfCnpj.ToString()} caracteres");

            RuleFor(p => p.PessoaEmail).EmailAddress().WithMessage("Preencha Email com um endereço de e-mail válido");
        }
    }
}