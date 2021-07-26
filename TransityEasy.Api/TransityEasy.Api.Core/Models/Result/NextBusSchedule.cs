using System;
namespace TransityEasy.Api.Core.Models.Result
{
    public class NextBusSchedule
    {
        public bool IsTripCancelled { get; set; }
        public bool IsStopCancelled { get; set; }
        public int CountdownInMin { get; set; }
        public NextBusScheduleStatus ScheduleStatus { get; set; }
        public string Destination { get; set; }
    }
}
