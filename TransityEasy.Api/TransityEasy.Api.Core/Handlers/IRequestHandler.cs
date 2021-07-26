﻿using System.Threading.Tasks;

namespace TransityEasy.Api.Core.Handlers
{
    public interface IRequestHandler<TRequest, TResult>
    {
        Task<TResult> HandleRequest(TRequest request); 
    }
}
