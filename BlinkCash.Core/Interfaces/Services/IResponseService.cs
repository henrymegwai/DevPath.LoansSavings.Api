using BlinkCash.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlinkCash.Core.Interfaces.Services
{
    public interface IResponseService
    {
        ExecutionResponse<T> ExecutionResponse<T>(string message, T data = null, bool status = false) where T : class;
        ExecutionResponse<List<T>> ExecutionResponseList<T>(string message, List<T> data = null, bool status = false) where T : class;
        ExecutionResponse<T[]> ExecutionResponseList<T>(string message, T[] data = null, bool status = false) where T : class;
    }
}
