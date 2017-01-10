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
    public class TP_Horoscope
    {
        #region Gets current horoscope data for the current day
        public static void getHoroscope(string sign)
        {
            Frame buildFrame = new Frame();

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"http://horoscope-api.herokuapp.com/horoscope/today/{sign}");
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
                        var serializer = new DataContractJsonSerializer(typeof(HoroscopeData));
                        var horoscopeDetails = (HoroscopeData)serializer.ReadObject(stream);

                        buildFrame.Navigate(typeof(Horoscope_card), horoscopeDetails);
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


    #region Class for serializing horoscope data
    [DataContract]
    public class HoroscopeData
    {
        [DataMember]
        public string date { get; set; }

        [DataMember]
        public string horoscope { get; set; }

        [DataMember]
        public string sunsign { get; set; }
    }
    #endregion
}
