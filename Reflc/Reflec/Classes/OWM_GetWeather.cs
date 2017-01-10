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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Reflec.Classes
{
    class OWM_GetWeather
    {
        #region Gets current weather conditions
        public static WeatherCurrent getCurrent(string city)
        {
            /*var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            if (roamingSettings.Values["ZipCode"] == null)
            {
                roamingSettings.Values["ZipCode"] = "98122";
            }

            string zip = (string)roamingSettings.Values["ZipCode"];*/

            if (city == null)
            {
                city = "98122,US";
            }

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"http://api.openweathermap.org/data/2.5/weather?zip={city}&units=imperial&APPID=e1e2647eeddb5412d5c4ee2fef620871");
            HttpClient client = new HttpClient();
            var response = client.SendAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var bytes = Encoding.Unicode.GetBytes(result);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    var serializer = new DataContractJsonSerializer(typeof(WeatherCurrent));
                    var weatherDetails = (WeatherCurrent)serializer.ReadObject(stream);

                    return weatherDetails;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Gets current weather forecast
        public static void getForecast(string city)
        {
            Frame buildFrame = new Frame();
            /*var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            if (roamingSettings.Values["ZipCode"] == null)
            {
                roamingSettings.Values["ZipCode"] = "98122";
            }

            string zip = (string)roamingSettings.Values["ZipCode"];*/

            if (city == null)
            {
                city = "98122,US";
            }

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"http://api.openweathermap.org/data/2.5/forecast/daily?q={city}&mode=json&cnt=5&units=imperial&APPID=e1e2647eeddb5412d5c4ee2fef620871");
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
                        var serializer = new DataContractJsonSerializer(typeof(WeatherForecast));
                        var weatherDetails = (WeatherForecast)serializer.ReadObject(stream);

                        buildFrame.Navigate(typeof(Weather_Card), weatherDetails);
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

    #region Class for current weather serialization
    [DataContract]
    public class Coord
    {
        [DataMember]
        public double lon { get; set; }

        [DataMember]
        public double lat { get; set; }
    }

    [DataContract]
    public class Weather
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string main { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public string icon { get; set; }
    }

    [DataContract]
    public class Main
    {
        [DataMember]
        public double temp { get; set; }

        [DataMember]
        public double pressure { get; set; }

        [DataMember]
        public int humidity { get; set; }

        [DataMember]
        public double temp_min { get; set; }

        [DataMember]
        public double temp_max { get; set; }

        [DataMember]
        public double sea_level { get; set; }

        [DataMember]
        public double grnd_level { get; set; }
    }

    [DataContract]
    public class Wind
    {
        [DataMember]
        public double speed { get; set; }

        [DataMember]
        public double deg { get; set; }
    }

    [DataContract]
    public class Clouds
    {
        [DataMember]
        public int all { get; set; }
    }

    [DataContract]
    public class Sys
    {
        [DataMember]
        public double message { get; set; }

        [DataMember]
        public string country { get; set; }

        [DataMember]
        public int sunrise { get; set; }

        [DataMember]
        public int sunset { get; set; }
    }

    [DataContract]
    public class WeatherCurrent
    {
        [DataMember]
        public Coord coord { get; set; }

        [DataMember]
        public IList<Weather> weather { get; set; }

        [DataMember(Name = "base")]
        public string weatherBase { get; set; }

        [DataMember]
        public Main main { get; set; }

        [DataMember]
        public Wind wind { get; set; }

        [DataMember]
        public Clouds clouds { get; set; }

        [DataMember]
        public int dt { get; set; }

        [DataMember]
        public Sys sys { get; set; }

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public int cod { get; set; }
    }
    #endregion

    #region Class for weather forecast serialization
    [DataContract]
    public class City
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public Coord coord { get; set; }

        [DataMember]
        public string country { get; set; }

        [DataMember]
        public int population { get; set; }
    }

    [DataContract]
    public class Temp
    {
        [DataMember]
        public double day { get; set; }

        [DataMember]
        public double min { get; set; }

        [DataMember]
        public double max { get; set; }

        [DataMember]
        public double night { get; set; }

        [DataMember]
        public double eve { get; set; }

        [DataMember]
        public double morn { get; set; }
    }

    [DataContract]
    public class Forecast
    {
        [DataMember]
        public int dt { get; set; }

        [DataMember]
        public Temp temp { get; set; }

        [DataMember]
        public double pressure { get; set; }

        [DataMember]
        public int humidity { get; set; }

        [DataMember]
        public IList<Weather> weather { get; set; }

        [DataMember]
        public double speed { get; set; }

        [DataMember]
        public int deg { get; set; }

        [DataMember]
        public int clouds { get; set; }

        [DataMember]
        public double snow { get; set; }

        [DataMember]
        public double? rain { get; set; }
    }

    [DataContract]
    public class WeatherForecast
    {
        [DataMember]
        public City city { get; set; }

        [DataMember]
        public string cod { get; set; }

        [DataMember]
        public double message { get; set; }

        [DataMember]
        public int cnt { get; set; }

        [DataMember(Name = "list")]
        public IList<Forecast> forecast { get; set; }
    }
    #endregion
}
