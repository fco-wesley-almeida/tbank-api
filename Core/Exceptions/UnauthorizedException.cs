using System;

namespace Core.Exceptions
{
    public class UnauthorizedException: SystemDefaultException
    {
        public override string GetMessage()
        {
            return "Acesso não autorizado";
        }

        public override dynamic GetData()
        {
            return "";
        }

        public override int GetStatusCode()
        {
            return 401;
        }
    }
}