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
    class PF_FlightData
    {
        #region Gets current flight data
        public static void getFlightData(string flight)
        {
            Frame buildFrame = new Frame();

            //https://planefinder.net/api/api.php?r=aircraftMetadata&adshex=111111&flightno=AA123&callsign=AA123
            //http://flightaware.com/commercial/flightxml/pricing_class.rvt

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"https://planefinder.net/api/api.php?r=aircraftMetadata&adshex=111111&flightno={flight}");
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
                        var serializer = new DataContractJsonSerializer(typeof(FlightStats));
                        var flightDetails = (FlightStats)serializer.ReadObject(stream);

                        FlightStatsTotal returnFlightStats = new FlightStatsTotal();
                        returnFlightStats.flightNum = flight;
                        returnFlightStats.stats = flightDetails;

                        buildFrame.Navigate(typeof(Flight_Card), returnFlightStats);
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


    #region Serialization for flight data
    [DataContract]
    public class AircraftData
    {

        [DataMember(Name = "aicraftOperator")]
        public string aicraftOperator { get; set; }

        [DataMember(Name = "aircraftAgeString")]
        public string aircraftAgeString { get; set; }

        [DataMember(Name = "aircraftFullType")]
        public string aircraftFullType { get; set; }

        [DataMember(Name = "aircraftManufacturer")]
        public string aircraftManufacturer { get; set; }

        [DataMember(Name = "aircraftSeries")]
        public string aircraftSeries { get; set; }

        [DataMember(Name = "aircraftModel")]
        public string aircraftModel { get; set; }

        [DataMember(Name = "airlineATCCallsign")]
        public string airlineATCCallsign { get; set; }

        [DataMember(Name = "airlineICAO")]
        public string airlineICAO { get; set; }

        [DataMember(Name = "constructionNumber")]
        public string constructionNumber { get; set; }

        [DataMember(Name = "countryCode")]
        public string countryCode { get; set; }

        [DataMember(Name = "deliveredDate")]
        public string deliveredDate { get; set; }

        [DataMember(Name = "engineString")]
        public string engineString { get; set; }

        [DataMember(Name = "firstFlightDate")]
        public string firstFlightDate { get; set; }

        [DataMember(Name = "lineNumber")]
        public string lineNumber { get; set; }

        [DataMember(Name = "registeredDate")]
        public string registeredDate { get; set; }

        [DataMember(Name = "registration")]
        public string registration { get; set; }

        [DataMember(Name = "rolloutDate")]
        public string rolloutDate { get; set; }

        [DataMember(Name = "typeCode")]
        public string typeCode { get; set; }

        [DataMember(Name = "customTitleText")]
        public string customTitleText { get; set; }
    }

    [DataContract]
    public class FlightData
    {

        [DataMember(Name = "daysOfOperation")]
        public string daysOfOperation { get; set; }

        [DataMember(Name = "departureApt")]
        public string departureApt { get; set; }

        [DataMember(Name = "departureTerminal")]
        public string departureTerminal { get; set; }

        [DataMember(Name = "departureGate")]
        public string departureGate { get; set; }

        [DataMember(Name = "arrivalApt")]
        public string arrivalApt { get; set; }

        [DataMember(Name = "arrivalTerminal")]
        public string arrivalTerminal { get; set; }

        [DataMember(Name = "arrivalGate")]
        public string arrivalGate { get; set; }

        [DataMember(Name = "arrivalDay")]
        public string arrivalDay { get; set; }

        [DataMember(Name = "journeyTime")]
        public string journeyTime { get; set; }

        [DataMember(Name = "codeshares")]
        public IList<string> codeshares { get; set; }

        [DataMember(Name = "serviceType")]
        public string serviceType { get; set; }

        [DataMember(Name = "seats")]
        public string seats { get; set; }

        [DataMember(Name = "freightCapacity")]
        public string freightCapacity { get; set; }

        [DataMember(Name = "freightClass")]
        public string freightClass { get; set; }

        [DataMember(Name = "passengerClasses")]
        public IList<string> passengerClasses { get; set; }

        [DataMember(Name = "routing")]
        public IList<string> routing { get; set; }
    }

    [DataContract]
    public class StatusData
    {

        [DataMember(Name = "arrEstLOC")]
        public int arrEstLOC { get; set; }

        [DataMember(Name = "arrSchdLOC")]
        public int arrSchdLOC { get; set; }

        [DataMember(Name = "arrEstUTC")]
        public int arrEstUTC { get; set; }

        [DataMember(Name = "arrSchdUTC")]
        public int arrSchdUTC { get; set; }

        [DataMember(Name = "depEstLOC")]
        public int depEstLOC { get; set; }

        [DataMember(Name = "depSchdLOC")]
        public int depSchdLOC { get; set; }

        [DataMember(Name = "depEstUTC")]
        public int depEstUTC { get; set; }

        [DataMember(Name = "depSchdUTC")]
        public int depSchdUTC { get; set; }

        [DataMember(Name = "departureApt")]
        public string departureApt { get; set; }

        [DataMember(Name = "arrivalApt")]
        public string arrivalApt { get; set; }

        [DataMember(Name = "divertAirport")]
        public object divertAirport { get; set; }

        [DataMember(Name = "status")]
        public int status { get; set; }

        [DataMember(Name = "arrActLOC")]
        public int? arrActLOC { get; set; }

        [DataMember(Name = "arrActUTC")]
        public int? arrActUTC { get; set; }

        [DataMember(Name = "depActUTC")]
        public int? depActUTC { get; set; }

        [DataMember(Name = "depActLOC")]
        public int? depActLOC { get; set; }

        [DataMember(Name = "depOffset")]
        public int depOffset { get; set; }

        [DataMember(Name = "arrOffset")]
        public int arrOffset { get; set; }

        [DataMember(Name = "bestDepartureTS")]
        public int bestDepartureTS { get; set; }

        [DataMember(Name = "bestArrivalTS")]
        public int bestArrivalTS { get; set; }
    }

    [DataContract]
    public class Dynamic
    {

        [DataMember(Name = "selectedAltitude")]
        public object selectedAltitude { get; set; }

        [DataMember(Name = "barometer")]
        public object barometer { get; set; }

        [DataMember(Name = "magneticHeading")]
        public object magneticHeading { get; set; }

        [DataMember(Name = "rollAngle")]
        public object rollAngle { get; set; }

        [DataMember(Name = "groundSpeed")]
        public object groundSpeed { get; set; }

        [DataMember(Name = "indicatedAirSpeed")]
        public object indicatedAirSpeed { get; set; }

        [DataMember(Name = "trueAirSpeed")]
        public object trueAirSpeed { get; set; }

        [DataMember(Name = "mach")]
        public object mach { get; set; }

        [DataMember(Name = "trackAngle")]
        public object trackAngle { get; set; }

        [DataMember(Name = "targetHeading")]
        public object targetHeading { get; set; }

        [DataMember(Name = "windSpeed")]
        public object windSpeed { get; set; }

        [DataMember(Name = "windDirection")]
        public object windDirection { get; set; }

        [DataMember(Name = "outsideAirTemperature")]
        public object outsideAirTemperature { get; set; }

        [DataMember(Name = "vertRate")]
        public object vertRate { get; set; }
    }

    [DataContract]
    public class Airport
    {

        [DataMember(Name = "airportIATA")]
        public string airportIATA { get; set; }

        [DataMember(Name = "airportName")]
        public string airportName { get; set; }

        [DataMember(Name = "airportLat")]
        public double airportLat { get; set; }

        [DataMember(Name = "airportLon")]
        public double airportLon { get; set; }

        [DataMember(Name = "airportCity")]
        public string airportCity { get; set; }
    }

    [DataContract]
    public class AirportDetail
    {

        [DataMember]
        public Airport airport { get; set; }
    }

    [DataContract]
    public class Payload
    {

        [DataMember(Name = "aircraftData")]
        public AircraftData aircraftData { get; set; }

        [DataMember(Name = "photos")]
        public object photos { get; set; }

        [DataMember(Name = "flightData")]
        public FlightData flightData { get; set; }

        [DataMember(Name = "statusData")]
        public StatusData statusData { get; set; }

        [DataMember(Name = "dynamic")]
        public Dynamic dynamic { get; set; }

        [DataMember(Name = "airportDetail")]
        public AirportDetail airportDetail { get; set; }
    }

    [DataContract]
    public class FlightStats
    {
        [DataMember(Name = "success")]
        public bool success { get; set; }

        [DataMember(Name = "payload")]
        public Payload payload { get; set; }
    }

    [DataContract]
    public class FlightStatsTotal
    {
        [DataMember]
        public string flightNum { get; set; }

        [DataMember]
        public FlightStats stats { get; set; }
    }
    #endregion
}
