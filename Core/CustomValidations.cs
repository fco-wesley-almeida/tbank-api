using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core
{
    public class CustomValidations
    {
        private static Regex GetCpfRegex()
        {
            return new Regex(@"^(\d{3})\.(\d{3})\.(\d{3})-(\d)(\d)$");
        }

        private static Regex GetCnpjRegex()
        {
            return new Regex(@"^(\d{2})\.(\d{3})\.(\d{3})\/(\d{4})-(\d)(\d)$");
        }

        private static Regex GetDateRegex()
        {
            return new Regex(@"^(\d{4})-(\d{2})-(\d{2}$)");
        }

        private static Regex GetTimeRegex()
        {
            return new Regex(@"^([01][0-9]|2[0-3]):[0-5][0-9]$");
        }

        private static Regex GetCepRegex()
        {
            return new Regex(@"^\d{5}-\d{3}$");
        }

        private static Regex GetRepeatedDigitsRegex()
        {
            return new Regex(@"^(.)\1+$");
        }

        public static bool ValidateCep(string cep)
        {
            return cep is null || GetCepRegex().IsMatch(cep);
        }

        public static bool ValidateDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return true;
            }
            try
            {
                Convert.ToDateTime(date);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ValidateCpf(string cpf)
        {
            int firstVerifierDigit;
            int secondVerifierDigit;
            int i;
            int j;
            int remainder;
            int sum;
            string cpfWithoutMask;
            int[] digits;
            Regex cpfRegex;

            cpfRegex = GetCpfRegex();

            if (!cpfRegex.IsMatch(cpf))
            {
                return false;
            }

            cpfWithoutMask = new Regex(@"[.-]").Replace(cpf, "");
            if (GetRepeatedDigitsRegex().IsMatch(cpfWithoutMask))
            {
                return false;
            }

            firstVerifierDigit = Convert.ToInt32(cpfRegex.Replace(cpf, "$4"));
            secondVerifierDigit = Convert.ToInt32(cpfRegex.Replace(cpf, "$5"));
            digits = cpfRegex
                .Replace(cpf, "$1$2$3$4")
                .ToCharArray()
                .Select(c => Convert.ToInt32(c.ToString()))
                .ToArray();

            for (i = 10, j = 0, sum = 0; i >= 2; i--)
            {
                sum += i * digits[j++];
            }

            remainder = sum * 10 % 11;

            if (remainder >= 10)
            {
                remainder = 0;
            }

            if (remainder != firstVerifierDigit)
            {
                return false;
            }

            for (i = 11, j = 0, sum = 0; i >= 2; i--)
            {
                sum += i * digits[j++];
            }

            remainder = sum * 10 % 11;
            if (remainder >= 10)
            {
                remainder = 0;
            }

            return remainder == secondVerifierDigit;
        }

        public static bool ValidateCnpj(string cnpj)
        {
            Regex cnpjRegex;
            int[] digits;
            string cnpjWithoutMask;
            int firstVerifierDigit;
            int secondVerifierDigit;
            int[] refDigitCalc;
            int sum;
            int i;
            int remainder;
            int verifierDigit;

            cnpjRegex = GetCnpjRegex();
            if (!cnpjRegex.IsMatch(cnpj))
            {
                return false;
            }

            firstVerifierDigit = Convert.ToInt32(cnpjRegex.Replace(cnpj, "$5"));

            secondVerifierDigit = Convert.ToInt32(cnpjRegex.Replace(cnpj, "$6"));
            cnpjWithoutMask = new Regex(@"[-./]").Replace(cnpj, "");
            digits = cnpjWithoutMask
                .ToCharArray()
                .Select(c => Convert.ToInt32(c.ToString()))
                .ToArray();

            refDigitCalc = new[] {5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};
            for (i = 0, sum = 0; i < 12; i++)
            {
                sum += refDigitCalc[i] * digits[i];
            }

            remainder = sum % 11;
            verifierDigit = remainder < 2 ? 0 : 11 - remainder;
            if (verifierDigit != firstVerifierDigit)
            {
                return false;
            }

            refDigitCalc = new[] {6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};
            for (i = 0, sum = 0; i < 13; i++)
            {
                sum += refDigitCalc[i] * digits[i];
            }

            remainder = sum % 11;
            verifierDigit = remainder < 2 ? 0 : 11 - remainder;
            return verifierDigit == secondVerifierDigit;
        }

        public static bool IsInLengthInterval(string s, int min, int max)
        {
            if (s == null)
            {
                return true;
            }
            return s.Length <= max && s.Length >= min;
        }
    }
}