using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransityEasy.Api.Core.Models.ApiResponse
{
    public enum TranslinkApiErrorCodes
    {
        DatabaseConnectionError = 10002,
        InvalidStopNumber = 1001,
        StopNumberNotFound = 1002,
        UnknownStopCheckError = 1003,
        UnknownGetStopError = 1004,
        InvalidLatLong = 1011,
        NoStopsFound = 1012,
        RadiusTooLarge = 1014,
        InvalidRouteNumber = 1015
    }
}
