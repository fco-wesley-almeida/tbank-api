using System;

namespace Core.Exceptions
{
    public class BadRequestException: SystemDefaultException
    {
        public override string GetMessage()
        {
            return "Informe os dados corretamente";
        }

        public override dynamic GetData()
        {
            return "";
        }

        public override int GetStatusCode()
        {
            return 400;
        }
    }
}