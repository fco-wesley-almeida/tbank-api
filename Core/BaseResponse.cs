using System.Threading.Tasks;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Core
{
    public class BaseResponse<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }

        public static BaseResponse<T> Builder()
        {
            return new BaseResponse<T>();
        }

        public BaseResponse<T> SetMessage(string message)
        {
            Message = message;
            return this;
        }

        public BaseResponse<T> SetData(T data)
        {
            Data = data;
            return this;
        }
    }
}