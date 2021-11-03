using SharpKml.Engine;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using TransityEasy.Api.Core.Models.Result;

namespace TransityEasy.Api.Core.Services
{
    public class KmlDecoderService : IKmlDecoderService
    {
        private readonly IHttpClientFactory _httpClientFactory; 
        public KmlDecoderService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory; 
        }
        public async Task<IEnumerable<IEnumerable<RouteMapLatLng>>> DecodeKMZFromURL(string url)
        {
            using var client = _httpClientFactory.CreateClient();
            using var response = await client.GetAsync(url);
            using var stream = await response.Content.ReadAsStreamAsync();
            var file = KmzFile.Open(stream);
            var doc = file.ReadKml();
            var document = XDocument.Parse(doc);
            var reader = document.CreateReader();
            XmlNamespaceManager xmlnsManager = new(reader.NameTable);
            xmlnsManager.AddNamespace("opengis", "http://www.opengis.net/kml/2.2");
            var lineStrings = document.XPathSelectElements("//opengis:LineString", xmlnsManager);
            var result = new List<List<RouteMapLatLng>>(); 
            foreach(var lineString in lineStrings)
            {
                var pointList = new List<RouteMapLatLng>();
                var coordinates = lineString.DescendantNodes().Where(node => node.NodeType == XmlNodeType.Text).Select(n => n.ToString()).FirstOrDefault();
                var coordinatePairs = coordinates.Split(" ");
                foreach(var coordinatePair in coordinatePairs)
                {
                    var coordinateSplit = coordinatePair.Split(",");
                    var longitude = double.Parse(coordinateSplit[0]);
                    var latitude = double.Parse(coordinateSplit[1]);
                    pointList.Add(new RouteMapLatLng { Latitude = latitude, Longitude = longitude});
                }
                result.Add(pointList); 
            }
            return result; 
        }
    }
}
