using Reflec.Assets.Weather;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace Reflec.Classes
{
    class DataBuilder
    {
        #region Sets weather icon to appropriate icon based on OWM
        private static HashSet<int> weatherHash_Rain = new HashSet<int>() { 200, 201, 202, 210, 211, 212, 221, 230, 231, 232, 300, 301, 302, 310, 311, 312, 313, 314, 321, 500, 501, 502, 503, 504, 511, 520, 521, 522, 531 };
        private static HashSet<int> weatherHash_Snow = new HashSet<int>() { 600, 601, 602, 611, 612, 615, 616, 220, 621, 622 };
        private static HashSet<int> weatherHash_Clear = new HashSet<int>() { 800 };
        private static HashSet<int> weatherHash_FewClouds = new HashSet<int>() { 801 };
        private static HashSet<int> weatherHash_Clouds = new HashSet<int>() { 701, 802, 803, 804 };

        #region Sets weather animation on home
        public static void setWeatherAnimation()
        {
            WeatherCurrent weather = OWM_GetWeather.getCurrent();

            if (weatherHash_Rain.Contains(weather.weather[0].id)){
                if (DateTime.Now.Hour < 8 || DateTime.Now.Hour > 18)
                {
                    MainPage.mainPage.WeartherAnimation_Frame.Navigate(typeof(Rain_Animation), true);
                }
                else { MainPage.mainPage.WeartherAnimation_Frame.Navigate(typeof(Rain_Animation)); }
            }

            if (weatherHash_Snow.Contains(weather.weather[0].id))
            {
                if (DateTime.Now.Hour < 8 || DateTime.Now.Hour > 18)
                {
                    MainPage.mainPage.WeartherAnimation_Frame.Navigate(typeof(Snow_Animation), true);
                }
                else { MainPage.mainPage.WeartherAnimation_Frame.Navigate(typeof(Snow_Animation)); }
            }

            if (weatherHash_Clear.Contains(weather.weather[0].id))
            {
                if (DateTime.Now.Hour < 8 || DateTime.Now.Hour > 18)
                {
                    MainPage.mainPage.WeartherAnimation_Frame.Navigate(typeof(Moon_Animation));
                }
                else { MainPage.mainPage.WeartherAnimation_Frame.Navigate(typeof(Sun_Animation)); }
            }

            if (weatherHash_FewClouds.Contains(weather.weather[0].id))
            {
                if (DateTime.Now.Hour < 8 || DateTime.Now.Hour > 18)
                {
                    MainPage.mainPage.WeartherAnimation_Frame.Navigate(typeof(Moon_Cloud_Animation));
                }
                else { MainPage.mainPage.WeartherAnimation_Frame.Navigate(typeof(Sun_Cloud_Animation)); }
            }

            if (weatherHash_Clouds.Contains(weather.weather[0].id))
            {
                if (DateTime.Now.Hour < 8 || DateTime.Now.Hour > 18)
                {
                    MainPage.mainPage.WeartherAnimation_Frame.Navigate(typeof(Cloudy_Animation), true);
                }
                else { MainPage.mainPage.WeartherAnimation_Frame.Navigate(typeof(Cloudy_Animation), false); }
            }

            else
            {
                string slackMessage = "The weather ID " + weather.weather[0].id + " (" + weather.weather[0].main + ", " + weather.weather[0].description + ") was not found in a hashset.";
                SlackSender.slackMessageSender("Mirror Weather Error", slackMessage);

                MainPage.mainPage.WeartherAnimation_Frame.Navigate(typeof(Page));
            }
        }
        #endregion

        #region Sets weather icon on weather cards
        public static string setWeatherIcon(int id)
        {
            string returnString;

            if (weatherHash_Rain.Contains(id))
            {
                returnString = "ms-appx:///Assets/Weather/Still/Rain.png";
            }

            else if (weatherHash_Snow.Contains(id))
            {
                returnString = "ms-appx:///Assets/Weather/Still/Snow.png";
            }

            else if (weatherHash_Clear.Contains(id))
            {
                returnString = "ms-appx:///Assets/Weather/Still/Sun.png";
            }

            else if (weatherHash_FewClouds.Contains(id))
            {
                returnString = "ms-appx:///Assets/Weather/Still/PartlyCloudy.png";
            }

            else if (weatherHash_Clouds.Contains(id))
            {
                returnString = "ms-appx:///Assets/Weather/Still/Cloudy.png";
            }

            else
            {
                returnString = "ms-appx:///Assets/Square44x44Logo.png";
            }

            return returnString;
        }
        #endregion
        #endregion

        #region Builds string based on unix DateTime
        public static string dayBuilder(double dt)
        {
            string returnString;

            DateTime epochDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            returnString = epochDateTime.AddSeconds(dt).DayOfWeek.ToString();

            return returnString;
        }

        public static string arrivalTimeBuilder(object dt)
        {
            string returnString;

            string wMili = dt.ToString();
            string woMili = wMili.Substring(0, wMili.Length - 3);
            double dtDouble = Double.Parse(woMili);

            DateTime epochDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime dtDateTime = epochDateTime.AddSeconds(dtDouble);
            TimeSpan diffTimeSpan = dtDateTime - DateTime.UtcNow;
            
            if(Math.Floor(diffTimeSpan.TotalMinutes) == 0)
            {
                returnString = "Now";
            }
            else
            {
                returnString = Math.Floor(diffTimeSpan.TotalMinutes).ToString() + " min";
            }

            return returnString;
        }
        #endregion

        #region Builds color from hex
        public static Color colorfromHexBuilder(string hex)
        {
            var r = Convert.ToByte(hex.Substring(0, 2), 16);
            var g = Convert.ToByte(hex.Substring(2, 2), 16);
            var b = Convert.ToByte(hex.Substring(4, 2), 16);

            return Color.FromArgb(255, r, g, b);
        }
        #endregion

        #region Builds a temp string
        public static string tempBuilder(double temp)
        {
            string returnString;

            returnString = Math.Round(temp, 0).ToString() + "°";

            return returnString;
        }
        #endregion
    }
}
