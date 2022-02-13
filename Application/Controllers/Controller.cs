using System;
using System.Threading.Tasks;
using Core;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class Controller : ControllerBase
    {
        protected async Task<ObjectResult> TreatException(SystemDefaultException e)
        {
            BaseResponse<dynamic> response = BaseResponse<dynamic>
                .Builder()
                .SetMessage(e.GetMessage())
                .SetData(e.GetData())
            ;
            return StatusCode(e.GetStatusCode(), response);
        }
         protected async Task<ObjectResult> UnkwnownException(Exception e)
         {
             Console.WriteLine(e.StackTrace);
             BaseResponse<dynamic> response = BaseResponse<dynamic>
                 .Builder()
                 .SetMessage("Ocorreu um erro no processamento da sua solicitação.")
                 .SetData("")
             ;
             return StatusCode(500, response);
         }

         protected async Task<ActionResult<BaseResponse<T>>> SecureResponse<T>(string message, Func<T> callback)
         {
             try
             {
                 T serviceResult = callback();
                 BaseResponse<T> response = BaseResponse<T>.Builder()
                     .SetMessage(message)
                     .SetData(serviceResult)
                 ;
                 return Ok(response);
             }
             catch (SystemDefaultException e)
             {
                 return await TreatException(e);
             }
             catch (Exception e)
             {
                 return await UnkwnownException(e);
             }
         }
    }
}