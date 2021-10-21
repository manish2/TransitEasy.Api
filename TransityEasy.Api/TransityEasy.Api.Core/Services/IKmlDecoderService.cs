using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransityEasy.Api.Core.Services
{
    public interface IKmlDecoderService
    {
        string DecodeZippedKmlFile();
    }
}
