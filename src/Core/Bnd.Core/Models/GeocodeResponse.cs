using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.Core.Models
{
    public class GeocodeResponse
    {
        public string? SearchAddress { get; set; }
        public GeoStatusCode Status { get; set; }
        public GeoAddressAccuracy Accuracy { get; set; }
        public Coordinate? Coordinate { get; set; }
    }
    public enum GeoStatusCode
    {
        Success = 200,
        BadRequest = 400,
        ServerError = 500,
        MissingQuery = 601,
        MissingAddress = 601,
        UnknownAddress = 602,
        UnavailableAddress = 603,
        UnknownDirections = 604,
        BadKey = 610,
        TooManyQueries = 620
    }

    public enum GeoAddressAccuracy
    {
        UnknownLocation = 0,
        Country = 1,
        Region = 2,
        SubRegion = 3,
        Town = 4,
        PostCode = 5,
        Street = 6,
        Intersection = 7,
        Address = 8,
        Premise = 9
    }

    public class Coordinate
    {
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
    }
}
