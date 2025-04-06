using Bnd.DTO.Models;
using GeographicLib;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities.Net;
using SixLabors.ImageSharp.PixelFormats;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Umbraco.Cms.Infrastructure.Persistence.LocalDb;

namespace Bnd.Core.Helpers
{
    public static class DealerHelpers
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string apiKey = "AIzaSyCFLBd_t1D27YGQ8B0etBOwISwQlDqbPRs";
        //private static readonly string apiKey = "AIzaSyCQ3_KUKbjAwWkU0ztH7DYYOio2shYQzqg";
        public static async Task<LatLngDistanceDTO> GetDistanceInKMFromLatLngAsync(double currentLocationLat, double currentLocationLng, double? dealerLat, double? dealerLng)
        {
            var dealerLatLngDistance = new LatLngDistanceDTO();
            try
            {
                string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&key={2}", dealerLat, dealerLng, apiKey);

                // Get the XML response from Google Geocoding API asynchronously
                var response = await _httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                var responseStream = await response.Content.ReadAsStreamAsync();
                XDocument xdoc = XDocument.Load(responseStream);

                var stateName = xdoc.Descendants("address_component")
                                                 .Where(ac => ac.Elements("type")
                                                 .Any(t => t.Value == "administrative_area_level_1"))
                                                 .Select(ac => ac.Element("short_name")?.Value)
                                                 .FirstOrDefault();

                var postalCode = xdoc.Descendants("address_component")
                    .Where(ac => ac.Elements("type")
                    .Any(t => t.Value == "postal_code"))
                    .Select(ac => ac.Element("long_name")?.Value)
                    .FirstOrDefault();

                dealerLatLngDistance.Latitude = dealerLat;
                dealerLatLngDistance.Longitude = dealerLng;
                dealerLatLngDistance.State = stateName;
                dealerLatLngDistance.DealerPostCode = postalCode;

            }
            catch (Exception ex)
            {
                dealerLatLngDistance.DealerDistance = -1;
                // Optionally log the exception here
            }
            return dealerLatLngDistance;
        }


        public static async Task<LatLngDistanceDTO> GetPostcodeFromLatLngAsync(double currentLocationLat, double currentLocationLng)
        {
            var currentLatLng = new LatLngDistanceDTO();
            try
            {
                string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&key={2}", currentLocationLat, currentLocationLng, apiKey);

                // Get the XML response from Google Geocoding API asynchronously
                var response = await _httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                var responseStream = await response.Content.ReadAsStreamAsync();
                XDocument xdoc = XDocument.Load(responseStream);

                var stateName = xdoc.Descendants("address_component")
                                                 .Where(ac => ac.Elements("type")
                                                 .Any(t => t.Value == "administrative_area_level_1"))
                                                 .Select(ac => ac.Element("short_name")?.Value)
                                                 .FirstOrDefault();

                var postalCode = xdoc.Descendants("address_component")
                    .Where(ac => ac.Elements("type")
                    .Any(t => t.Value == "postal_code"))
                    .Select(ac => ac.Element("long_name")?.Value)
                    .FirstOrDefault();

                currentLatLng.Latitude = currentLocationLat;
                currentLatLng.Longitude = currentLocationLng;
                currentLatLng.State = stateName;
                currentLatLng.DealerPostCode = postalCode;
            }
            catch (Exception ex) { return null; }
            return currentLatLng;
        }


        public static double GetDistanceInKMFromLatLng(double currentLocationLat, double currentLocationLng, double dealerLat, double dealerLng)
        {
            /** Calculation into KM **/
            var geod = Geodesic.WGS84; // Use WGS84 geodesic model
            var re = geod.Inverse(currentLocationLat, currentLocationLng, dealerLat, dealerLng);
            var distance = Math.Round(re.Distance / 1000, 0);
            return distance;

        }

        public static async Task<LatLngDistanceDTO> GetDistanceInKMFromAddressAsync(double lat1, double lon1, string address)
        {
            var dealerLatLngDistance = await GetDistanceFromAddressAsync(address, lat1, lon1);
            return dealerLatLngDistance;
        }

        public static async Task<LatLngDistanceDTO> GetLatLngInKMFromAddressAsync(string address)
        {
            var dealerLatLngDistance = new LatLngDistanceDTO();

            if (!string.IsNullOrEmpty(address))
            {
                string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={1}&address={0},+Australia&sensor=false", Uri.EscapeDataString(address), apiKey);

                try
                {
                    var response = await _httpClient.GetAsync(requestUri);
                    response.EnsureSuccessStatusCode();
                    var responseStream = await response.Content.ReadAsStreamAsync();
                    XDocument xdoc = XDocument.Load(responseStream);

                    XElement result = xdoc.Element("GeocodeResponse")?.Element("result");

                    if (result != null)
                    {
                        XElement locationElement = result.Element("geometry")?.Element("location");
                        XElement lat = locationElement?.Element("lat");
                        XElement lng = locationElement?.Element("lng");

                        var dealerLat = XElementToDouble(lat);
                        var dealerLng = XElementToDouble(lng);

                        var stateName = xdoc.Descendants("address_component")
                                            .Where(ac => ac.Elements("type")
                                            .Any(t => t.Value == "administrative_area_level_1"))
                                            .Select(ac => ac.Element("short_name")?.Value)
                                            .FirstOrDefault();

                        var postalCode = xdoc.Descendants("address_component")
                                             .Where(ac => ac.Elements("type")
                                             .Any(t => t.Value == "postal_code"))
                                             .Select(ac => ac.Element("long_name")?.Value)
                                             .FirstOrDefault();

                        dealerLatLngDistance.Latitude = dealerLat;
                        dealerLatLngDistance.Longitude = dealerLng;
                        dealerLatLngDistance.State = stateName;
                        dealerLatLngDistance.DealerPostCode = postalCode;
                    }
                    else
                    {
                        dealerLatLngDistance.DealerDistance = -1;
                    }
                }
                catch (Exception ex)
                {
                    dealerLatLngDistance.DealerDistance = -1;
                    // Optionally log the exception here
                }
            }
            else
            {
                dealerLatLngDistance.DealerDistance = -1;
            }

            return dealerLatLngDistance;
        }

        public static LatLngDistanceDTO GetLatLngInKMFromAddress(string address)
        {
            var dealerLatLngDistance = new LatLngDistanceDTO();
            if (address != "")
            {
                string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={1}&address={0},+Australia&sensor=false", Uri.EscapeDataString(address), apiKey);
                try
                {
                    WebRequest request = WebRequest.Create(requestUri);
                    WebResponse response = request.GetResponse();
                    XDocument xdoc = XDocument.Load(response.GetResponseStream());

                    XElement result = xdoc.Element("GeocodeResponse").Element("result");
                    if (result != null)
                    {
                        XElement locationElement = result.Element("geometry").Element("location");
                        XElement lat = locationElement.Element("lat");
                        XElement lng = locationElement.Element("lng");

                        var dealerLat = XElementToDouble(lat);
                        var dealerLng = XElementToDouble(lng);

                        var stateName = xdoc.Descendants("address_component")
                                                  .Where(ac => ac.Elements("type")
                                                  .Any(t => t.Value == "administrative_area_level_1"))
                                                  .Select(ac => ac.Element("short_name").Value)
                                                  .FirstOrDefault();

                        var postalCode = xdoc.Descendants("address_component")
                           .Where(ac => ac.Elements("type")
                           .Any(t => t.Value == "postal_code"))
                           .Select(ac => ac.Element("long_name").Value)
                           .FirstOrDefault();


                        dealerLatLngDistance.Latitude = dealerLat;
                        dealerLatLngDistance.Longitude = dealerLng;
                        dealerLatLngDistance.State = stateName;
                        dealerLatLngDistance.DealerPostCode = postalCode;
                    }
                    else { dealerLatLngDistance.DealerDistance = -1; }

                }
                catch (Exception ex)
                {
                    dealerLatLngDistance.DealerDistance = -1;
                }
            }
            else { dealerLatLngDistance.DealerDistance = -1; }

            return dealerLatLngDistance;
        }


        public static async Task<LatLngDistanceDTO> UpdateLatLngFromAddressAsync(string address)
        {
            var dealerLatLngDistance = new LatLngDistanceDTO();

            if (!string.IsNullOrEmpty(address))
            {
                string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={1}&address={0},+Australia&sensor=false", Uri.EscapeDataString(address), apiKey);

                try
                {
                    var response = await _httpClient.GetAsync(requestUri);
                    response.EnsureSuccessStatusCode();
                    var responseStream = await response.Content.ReadAsStreamAsync();
                    XDocument xdoc = XDocument.Load(responseStream);

                    XElement result = xdoc.Element("GeocodeResponse")?.Element("result");

                    if (result != null)
                    {
                        XElement locationElement = result.Element("geometry")?.Element("location");
                        XElement lat = locationElement?.Element("lat");
                        XElement lng = locationElement?.Element("lng");

                        var dealerLat = XElementToDouble(lat);
                        var dealerLng = XElementToDouble(lng);

                        var stateName = xdoc.Descendants("address_component")
                                            .Where(ac => ac.Elements("type")
                                            .Any(t => t.Value == "administrative_area_level_1"))
                                            .Select(ac => ac.Element("short_name")?.Value)
                                            .FirstOrDefault();

                        var postalCode = xdoc.Descendants("address_component")
                                             .Where(ac => ac.Elements("type")
                                             .Any(t => t.Value == "postal_code"))
                                             .Select(ac => ac.Element("long_name")?.Value)
                                             .FirstOrDefault();

                        dealerLatLngDistance.Latitude = dealerLat;
                        dealerLatLngDistance.Longitude = dealerLng;
                        dealerLatLngDistance.State = stateName;
                        dealerLatLngDistance.DealerPostCode = postalCode;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                    // Optionally log the exception here
                }
            }
            else
            {
                return null;
            }

            return dealerLatLngDistance;
        }

        public static async Task<IEnumerable<DealerDto>> GetDistanceInfo(IEnumerable<DealerDto> dealers, double lat, double lng)
        {

            var tasks = dealers.Select(async dealer =>
            {

                var storeLat = dealer.Latitude;
                var storeLong = dealer.Longitude;
                var dealerLatLngDistance = new LatLngDistanceDTO();


                /** Calculation into KM **/
                var geod = Geodesic.WGS84; // Use WGS84 geodesic model
                var re = geod.Inverse(lat, lng, Convert.ToDouble(storeLat), Convert.ToDouble(storeLong));
                var distance = Math.Round(re.Distance / 1000, 0);
                dealer.DealerDistance = distance;
                return dealer;
            });
            return await Task.WhenAll(tasks);
        }


        public static async Task<LatLngDistanceDTO> GetDistanceFromAddressAsync(string address, double currentLocationLat, double currentLocationLng)
        {
            var dealerLatLngDistance = new LatLngDistanceDTO();

            if (!string.IsNullOrEmpty(address))
            {
                string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={1}&address={0},+Australia&sensor=false", Uri.EscapeDataString(address), apiKey);

                try
                {
                    var response = await _httpClient.GetAsync(requestUri);
                    response.EnsureSuccessStatusCode();
                    var responseStream = await response.Content.ReadAsStreamAsync();
                    XDocument xdoc = XDocument.Load(responseStream);

                    XElement result = xdoc.Element("GeocodeResponse")?.Element("result");

                    if (result != null)
                    {
                        XElement locationElement = result.Element("geometry")?.Element("location");
                        XElement lat = locationElement?.Element("lat");
                        XElement lng = locationElement?.Element("lng");

                        var dealerLat = XElementToDouble(lat);
                        var dealerLng = XElementToDouble(lng);

                        var stateName = xdoc.Descendants("address_component")
                                            .Where(ac => ac.Elements("type")
                                            .Any(t => t.Value == "administrative_area_level_1"))
                                            .Select(ac => ac.Element("short_name")?.Value)
                                            .FirstOrDefault();

                        var postalCode = xdoc.Descendants("address_component")
                                             .Where(ac => ac.Elements("type")
                                             .Any(t => t.Value == "postal_code"))
                                             .Select(ac => ac.Element("long_name")?.Value)
                                             .FirstOrDefault();

                        /** Calculation into KM **/
                        var geod = Geodesic.WGS84; // Use WGS84 geodesic model
                        var re = geod.Inverse(currentLocationLat, currentLocationLng, dealerLat, dealerLng);
                        var distance = Math.Round(re.Distance / 1000, 0);

                        dealerLatLngDistance.Latitude = dealerLat;
                        dealerLatLngDistance.Longitude = dealerLng;
                        dealerLatLngDistance.DealerDistance = distance;
                        dealerLatLngDistance.State = stateName;
                        dealerLatLngDistance.DealerPostCode = postalCode;
                    }
                    else
                    {
                        dealerLatLngDistance.DealerDistance = -1;
                    }
                }
                catch (Exception ex)
                {
                    dealerLatLngDistance.DealerDistance = -1;
                    // Optionally log the exception here
                }
            }
            else
            {
                dealerLatLngDistance.DealerDistance = -1;
            }

            return dealerLatLngDistance;
        }
        public static LatLngDistanceDTO GetDistanceFromAddress(string address, double currentLocationLat, double currentLocationLng)
        {
            var dealerLatLngDistance = new LatLngDistanceDTO();
            if (address != "")
            {
                string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={1}&address={0},+Australia&sensor=false", Uri.EscapeDataString(address), apiKey);
                try
                {
                    WebRequest request = WebRequest.Create(requestUri);
                    WebResponse response = request.GetResponse();
                    XDocument xdoc = XDocument.Load(response.GetResponseStream());

                    XElement result = xdoc.Element("GeocodeResponse").Element("result");
                    if (result != null)
                    {
                        XElement locationElement = result.Element("geometry").Element("location");
                        XElement lat = locationElement.Element("lat");
                        XElement lng = locationElement.Element("lng");

                        var dealerLat = XElementToDouble(lat);
                        var dealerLng = XElementToDouble(lng);

                        var stateName = xdoc.Descendants("address_component")
                                                  .Where(ac => ac.Elements("type")
                                                  .Any(t => t.Value == "administrative_area_level_1"))
                                                  .Select(ac => ac.Element("short_name").Value)
                                                  .FirstOrDefault();

                        var postalCode = xdoc.Descendants("address_component")
                           .Where(ac => ac.Elements("type")
                           .Any(t => t.Value == "postal_code"))
                           .Select(ac => ac.Element("long_name").Value)
                           .FirstOrDefault();

                        /** Calculation into KM **/
                        var geod = Geodesic.WGS84; // Use WGS84 geodesic model
                        var re = geod.Inverse(currentLocationLat, currentLocationLng, dealerLat, dealerLng);
                        var distance = Math.Round(re.Distance / 1000, 0);
                        dealerLatLngDistance.Latitude = dealerLat;
                        dealerLatLngDistance.Longitude = dealerLng;

                        dealerLatLngDistance.DealerDistance = distance;
                        dealerLatLngDistance.State = stateName;
                        dealerLatLngDistance.DealerPostCode = postalCode;
                    }
                    else { dealerLatLngDistance.DealerDistance = -1; }

                }
                catch (Exception ex)
                {
                    dealerLatLngDistance.DealerDistance = -1;
                }
            }
            else { dealerLatLngDistance.DealerDistance = -1; }

            return dealerLatLngDistance;

        }
        private static double XElementToDouble(XElement element)
        {
            if (element != null && double.TryParse(element.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
            {
                return value;
            }
            throw new FormatException("Invalid or missing value in XElement.");
        }
        public static async Task<double> GetDistanceBetweenTwoLocationsFromAddress(double lat1, double lon1, string address)
        {
            const double earthRadiusKm = 6371.0;

            var (lat2, lng2) = await DealerHelpers.GetLatLngFromAddress(address);

            // Convert degrees to radians
            double dLat = DegToRad(lat1 - lat2);
            double dLon = DegToRad(lon1 - lng2);

            // Apply Haversine formula
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                        Math.Cos(DegToRad(lat1)) * Math.Cos(DegToRad(lat2)) *
                        Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return earthRadiusKm * c; // Distance in kilometers
        }
        private static double DegToRad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double RadToDeg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        public static async Task<double> GetDistanceBetweenTwoLocationsFromAddressAsync(double lat1, double lon1, string address)
        {
            var (lat, lng) = await GetLatLngFromAddress(address);

            var lat2 = lat;
            var lon2 = lng;
            var theta = lon1 - lon2;

            var dist = Math.Sin(DegToRad(lat1)) * Math.Sin(DegToRad(lat2)) + Math.Cos(DegToRad(lat1)) * Math.Cos(DegToRad(lat2)) * Math.Cos(DegToRad(theta));

            dist = Math.Acos(dist);
            dist = RadToDeg(dist);
            dist = dist * 60 * 1.1515;
            dist = dist * 1.609344;

            return dist;
        }
        public static async Task<(double lat, double lng)> GetLatLngFromAddress(string address)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)},+Australia&key={apiKey}";
                    var response = await client.GetStringAsync(url);

                    var json = JObject.Parse(response);

                    var location = json["results"]?[0]?["geometry"]?["location"];

                    if (location != null)
                    {
                        double lat = (double)location["lat"];
                        double lng = (double)location["lng"];
                        return (lat, lng);
                    }
                    else
                    {
                        throw new Exception("Unable to find location.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions, log them if necessary
                    throw new Exception($"Geocoding API error: {ex.Message}");
                }
            }
        }

        public static string GetStateFromPostcode(string postcode)
        {
            if (!int.TryParse(postcode, out int postcodeInt))
                throw new ArgumentException("Invalid postcode");

            bool PostcodeBetween(string min, string max) =>
                postcodeInt >= int.Parse(min) && postcodeInt <= int.Parse(max);

            string state = null;

            if (PostcodeBetween("1000", "1999") || PostcodeBetween("2000", "2599") ||
                PostcodeBetween("2619", "2899") || PostcodeBetween("2921", "2999"))
            {
                state = "NSW";
            }
            else if (PostcodeBetween("0200", "0299") || PostcodeBetween("2600", "2618") ||
                     PostcodeBetween("2900", "2920"))
            {
                state = "ACT";
            }
            else if (PostcodeBetween("3000", "3999") || PostcodeBetween("8000", "8999"))
            {
                state = "VIC";
            }
            else if (PostcodeBetween("4000", "4999") || PostcodeBetween("9000", "9999"))
            {
                state = "QLD";
            }
            else if (PostcodeBetween("5000", "5799") || PostcodeBetween("5800", "5999"))
            {
                state = "SA";
            }
            else if (PostcodeBetween("6000", "6797") || PostcodeBetween("6800", "6999"))
            {
                state = "WA";
            }
            else if (PostcodeBetween("7000", "7799") || PostcodeBetween("7800", "7999"))
            {
                state = "TAS";
            }
            else if (PostcodeBetween("0800", "0899") || PostcodeBetween("0900", "0999"))
            {
                state = "NT";
            }

            return state;
        }

        private static bool IsValidPostcode(string postcode, out string pcodeTransform)
        {
            if (int.TryParse(postcode, out int pcode) && pcode >= 800 && pcode <= 7800)
            {
                pcodeTransform = pcode.ToString("D4");  // Ensure 4 digits (e.g., "0800")
                return true;
            }
            pcodeTransform = null;
            return false;
        }

    }
}
