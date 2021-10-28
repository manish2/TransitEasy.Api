using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransityEasy.Api.Core.Models.Result
{
    public class RoutesInfo
    {
        public int RouteId { get; set; }
        public string AgencyId { get; set; }
        public string RouteShortName { get; set; }
        public string RouteLongName { get; set; }
        public string RouteDesc { get; set; }
    }
}
