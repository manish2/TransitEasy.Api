using System;
namespace TransityEasy.Api.Core.Models.Request
{
    public class NextBusesScheduleRequest
    {
        public int StopNumber { get; set; }
        public int NumNextBuses { get; set; }
    }
}
