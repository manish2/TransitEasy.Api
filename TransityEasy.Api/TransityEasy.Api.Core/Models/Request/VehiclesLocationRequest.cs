using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransityEasy.Api.Core.Models.Request
{
    public class VehiclesLocationRequest
    {
        public int? RouteNo { get; set; }
        public int? StopNo { get; set; }
    }
}
