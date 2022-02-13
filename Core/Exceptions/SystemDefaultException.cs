using System;
using Microsoft.Extensions.Logging;

namespace Core.Exceptions
{
    public abstract class SystemDefaultException: Exception
    {
        public abstract string GetMessage();
        public abstract dynamic GetData();
        public abstract int GetStatusCode();
    }
}