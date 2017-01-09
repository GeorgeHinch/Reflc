using Reflec.Cards;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Reflec.Classes
{
    class OBA_Request
    {
        private static string _apiKey = "TEST";

        #region Gets data for OneBusAway stop
        public static void getStopAD()
        {
            Frame buildFrame = new Frame();
            
            string stop = "1_3600";

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"http://api.pugetsound.onebusaway.org/api/where/arrivals-and-departures-for-stop/{stop}.json?key={_apiKey}");
            HttpClient client = new HttpClient();
            var response = client.SendAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var bytes = Encoding.Unicode.GetBytes(result);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    try
                    {
                        var serializer = new DataContractJsonSerializer(typeof(OBA_StopAD));
                        var stopDetails = (OBA_StopAD)serializer.ReadObject(stream);

                        buildFrame.Navigate(typeof(OBA_Card), stopDetails);
                        MainPage.mainPage.Main_StackPanel.Children.Add(buildFrame);
                    }
                    catch
                    {
                        MainPage.buildError(true);
                    }
                    
                }
            }
            else
            {
                MainPage.buildError(false);
            }
        }
        #endregion

        #region Gets data for OneBusAway stop
        public static void getNearbyStop()
        {
            Frame buildFrame = new Frame();
            
            string lat = "47.602098";
            string lon = "-122.316905";

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"http://api.pugetsound.onebusaway.org/api/where/stops-for-location.json?key={_apiKey}&lat={lat}&lon={lon}");
            HttpClient client = new HttpClient();
            var response = client.SendAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var bytes = Encoding.Unicode.GetBytes(result);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    try
                    {
                        var serializer = new DataContractJsonSerializer(typeof(OBA_NearbyStop));
                        var stopNearby = (OBA_NearbyStop)serializer.ReadObject(stream);

                        buildFrame.Navigate(typeof(OBA_NearbyStops_Card), stopNearby);
                        MainPage.mainPage.Main_StackPanel.Children.Add(buildFrame);
                    }
                    catch
                    {
                        MainPage.buildError(true);
                    }
                }
            }
            else
            {
                MainPage.buildError(false);
            }
        }
        #endregion
    }

    #region Class for current stop serialization
    [DataContract]
    public class LastKnownLocation
    {
        [DataMember]
        public double lat { get; set; }

        [DataMember]
        public double lon { get; set; }
    }

    [DataContract]
    public class Position
    {
        [DataMember]
        public double lat { get; set; }

        [DataMember]
        public double lon { get; set; }
    }

    [DataContract]
    public class TripStatus
    {
        [DataMember]
        public string activeTripId { get; set; }

        [DataMember]
        public int blockTripSequence { get; set; }

        [DataMember]
        public string closestStop { get; set; }

        [DataMember]
        public int closestStopTimeOffset { get; set; }

        [DataMember]
        public double distanceAlongTrip { get; set; }

        [DataMember]
        public object frequency { get; set; }

        [DataMember]
        public int lastKnownDistanceAlongTrip { get; set; }

        [DataMember]
        public LastKnownLocation lastKnownLocation { get; set; }

        [DataMember]
        public int lastKnownOrientation { get; set; }

        [DataMember]
        public object lastLocationUpdateTime { get; set; }

        [DataMember]
        public object lastUpdateTime { get; set; }

        [DataMember]
        public string nextStop { get; set; }

        [DataMember]
        public int nextStopTimeOffset { get; set; }

        [DataMember]
        public double orientation { get; set; }

        [DataMember]
        public string phase { get; set; }

        [DataMember]
        public Position position { get; set; }

        [DataMember]
        public bool predicted { get; set; }

        [DataMember]
        public int scheduleDeviation { get; set; }

        [DataMember]
        public double scheduledDistanceAlongTrip { get; set; }

        [DataMember]
        public object serviceDate { get; set; }

        [DataMember]
        public IList<object> situationIds { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public double totalDistanceAlongTrip { get; set; }

        [DataMember]
        public string vehicleId { get; set; }
    }

    [DataContract]
    public class ArrivalsAndDeparture
    {
        [DataMember]
        public bool arrivalEnabled { get; set; }

        [DataMember]
        public int blockTripSequence { get; set; }

        [DataMember]
        public bool departureEnabled { get; set; }

        [DataMember]
        public double distanceFromStop { get; set; }

        [DataMember]
        public object frequency { get; set; }

        [DataMember]
        public object lastUpdateTime { get; set; }

        [DataMember]
        public int numberOfStopsAway { get; set; }

        [DataMember]
        public bool predicted { get; set; }

        [DataMember]
        public object predictedArrivalInterval { get; set; }

        [DataMember]
        public object predictedArrivalTime { get; set; }

        [DataMember]
        public object predictedDepartureInterval { get; set; }

        [DataMember]
        public object predictedDepartureTime { get; set; }

        [DataMember]
        public string routeId { get; set; }

        [DataMember]
        public string routeLongName { get; set; }

        [DataMember]
        public string routeShortName { get; set; }

        [DataMember]
        public object scheduledArrivalInterval { get; set; }

        [DataMember]
        public object scheduledArrivalTime { get; set; }

        [DataMember]
        public object scheduledDepartureInterval { get; set; }

        [DataMember]
        public object scheduledDepartureTime { get; set; }

        [DataMember]
        public object serviceDate { get; set; }

        [DataMember]
        public IList<object> situationIds { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public string stopId { get; set; }

        [DataMember]
        public int stopSequence { get; set; }

        [DataMember]
        public int totalStopsInTrip { get; set; }

        [DataMember]
        public string tripHeadsign { get; set; }

        [DataMember]
        public string tripId { get; set; }

        [DataMember]
        public TripStatus tripStatus { get; set; }

        [DataMember]
        public string vehicleId { get; set; }
    }

    [DataContract]
    public class Entry
    {
        [DataMember]
        public IList<ArrivalsAndDeparture> arrivalsAndDepartures { get; set; }

        [DataMember]
        public IList<string> nearbyStopIds { get; set; }

        [DataMember]
        public IList<object> situationIds { get; set; }

        [DataMember]
        public string stopId { get; set; }
    }

    [DataContract]
    public class Agency
    {
        [DataMember]
        public string disclaimer { get; set; }

        [DataMember]
        public string email { get; set; }

        [DataMember]
        public string fareUrl { get; set; }

        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string lang { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string phone { get; set; }

        [DataMember]
        public bool privateService { get; set; }

        [DataMember]
        public string timezone { get; set; }

        [DataMember]
        public string url { get; set; }
    }

    [DataContract]
    public class Route
    {
        [DataMember]
        public string agencyId { get; set; }

        [DataMember]
        public string color { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string longName { get; set; }

        [DataMember]
        public string shortName { get; set; }

        [DataMember]
        public string textColor { get; set; }

        [DataMember]
        public int type { get; set; }

        [DataMember]
        public string url { get; set; }
    }

    [DataContract]
    public class Stop
    {
        [DataMember]
        public string code { get; set; }

        [DataMember]
        public string direction { get; set; }

        [DataMember]
        public string id { get; set; }

        [DataMember]
        public double lat { get; set; }

        [DataMember]
        public int locationType { get; set; }

        [DataMember]
        public double lon { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public IList<string> routeIds { get; set; }

        [DataMember]
        public string wheelchairBoarding { get; set; }
    }

    [DataContract]
    public class Trip
    {
        [DataMember]
        public string blockId { get; set; }

        [DataMember]
        public string directionId { get; set; }

        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string routeId { get; set; }

        [DataMember]
        public string routeShortName { get; set; }

        [DataMember]
        public string serviceId { get; set; }

        [DataMember]
        public string shapeId { get; set; }

        [DataMember]
        public string timeZone { get; set; }

        [DataMember]
        public string tripHeadsign { get; set; }

        [DataMember]
        public string tripShortName { get; set; }
    }

    [DataContract]
    public class References
    {
        [DataMember]
        public IList<Agency> agencies { get; set; }

        [DataMember]
        public IList<Route> routes { get; set; }

        [DataMember]
        public IList<object> situations { get; set; }

        [DataMember]
        public IList<Stop> stops { get; set; }

        [DataMember]
        public IList<Trip> trips { get; set; }
    }

    [DataContract]
    public class Data
    {
        [DataMember]
        public Entry entry { get; set; }

        [DataMember]
        public References references { get; set; }

        [DataMember]
        public bool limitExceeded { get; set; }

        [DataMember(Name = "list")]
        public IList<OBAList> list { get; set; }

        [DataMember]
        public bool outOfRange { get; set; }
    }

    [DataContract]
    public class OBA_StopAD
    {
        [DataMember]
        public int code { get; set; }

        [DataMember]
        public long currentTime { get; set; }

        [DataMember]
        public Data data { get; set; }

        [DataMember]
        public string text { get; set; }

        [DataMember]
        public int version { get; set; }
    }
    #endregion

    #region Class for nearby stop serialization
    [DataContract]
    public class OBAList
    {
        [DataMember]
        public string code { get; set; }

        [DataMember]
        public string direction { get; set; }

        [DataMember]
        public string id { get; set; }

        [DataMember]
        public double lat { get; set; }

        [DataMember]
        public int locationType { get; set; }

        [DataMember]
        public double lon { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public IList<string> routeIds { get; set; }

        [DataMember]
        public string wheelchairBoarding { get; set; }
    }

    [DataContract]
    public class OBA_NearbyStop
    {
        [DataMember]
        public int code { get; set; }

        [DataMember]
        public long currentTime { get; set; }

        [DataMember]
        public Data data { get; set; }

        [DataMember]
        public string text { get; set; }

        [DataMember]
        public int version { get; set; }
    }
    #endregion
}
