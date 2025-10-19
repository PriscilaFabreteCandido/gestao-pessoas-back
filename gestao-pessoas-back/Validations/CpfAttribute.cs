using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace gestao_pessoas_back.Validations
{
    public class CpfAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return ValidationResult.Success; // Required attribute vai cuidar disso

            var cpf = value.ToString();
            var cpfLimpo = LimparCpf(cpf);

            if (!ValidarCpf(cpfLimpo))
                return new ValidationResult("CPF inválido");

            return ValidationResult.Success;
        }

        private static string LimparCpf(string cpf)
        {
            return Regex.Replace(cpf, @"[^\d]", "");
        }

        private static bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cpf.Distinct().Count() == 1)
                return false;

            // Validação do primeiro dígito verificador
            var soma = 0;
            for (var i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * (10 - i);

            var resto = soma % 11;
            var digito1 = resto < 2 ? 0 : 11 - resto;

            if (digito1 != int.Parse(cpf[9].ToString()))
                return false;

            // Validação do segundo dígito verificador
            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * (11 - i);

            resto = soma % 11;
            var digito2 = resto < 2 ? 0 : 11 - resto;

            return digito2 == int.Parse(cpf[10].ToString());
        }
    }
}