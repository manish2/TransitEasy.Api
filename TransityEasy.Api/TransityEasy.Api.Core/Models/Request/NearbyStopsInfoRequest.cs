namespace TransityEasy.Api.Core.Models.Request
{
    public class NearbyStopsInfoRequest
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Radius { get; set; }
    }
}
