using BlinkCash.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlinkCash.Savings.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        public string ErrorString => "An error occured";
        [ApiExplorerSettings(IgnoreApi = true)]
        public Tuple<bool, ExecutionResponse<string>> GetModelStateError(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                var a = ModelState.Values.SelectMany(v => v.Errors);
                var reqErrors = a.Select(x => x.ErrorMessage);
                var reqErrorsString = string.Join(",", reqErrors);

                var executionResponse = new ExecutionResponse<string>
                {
                    Message = reqErrorsString
                };

                return new Tuple<bool, ExecutionResponse<string>>(false, executionResponse);
            }
            else
            {
                return new Tuple<bool, ExecutionResponse<string>>(true, null);
            }
        }

    }
}
