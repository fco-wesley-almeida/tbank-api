
using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Core.Exceptions
{
    public class BadRequestException: SystemDefaultException
    {
        private ValidationResult _validationResult;
        private string _message;
        private dynamic _data;

        public BadRequestException(string message)
        {
            _message = message;
        }
        public BadRequestException(ValidationResult validationResult)
        {
            _validationResult = validationResult;
            _message = "Informe os dados corretamente.";
        }


        public BadRequestException(Tuple<string, string> error)
        {
            _message = "Informe os dados corretamente";
            _data = error;
        }

        public override string GetMessage()
        {
            return _message;
        }

        public override dynamic GetData()
        {
            if (_validationResult is null)
            {
                return _data ?? "";
            }
            Dictionary<string, string> errors = new Dictionary<string, string>();
            foreach (ValidationFailure validationFailure in _validationResult.Errors)
            {
                if (!errors.Keys.Contains(validationFailure.PropertyName))
                {
                    errors.Add(validationFailure.PropertyName.ToLower(), validationFailure.ErrorMessage);
                }
            }
            return errors;
        }

        public override int GetStatusCode()
        {
            return 400;
        }
    }
}