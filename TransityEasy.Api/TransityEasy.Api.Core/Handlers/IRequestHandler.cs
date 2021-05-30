using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransityEasy.Api.Core.Handlers
{
    public interface IRequestHandler<TRequest, TResult>
    {
        Task<TResult> HandleRequest(TRequest request); 
    }
}
