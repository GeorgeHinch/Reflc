using Reflec.Assets.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Reflec.Classes
{
    class DataBuilder
    {
        #region Sets weather icon to appropriate animation based on OWM
        private static HashSet<int> weatherHash_Rain = new HashSet<int>() { 200, 201, 202, 210, 211, 212, 221, 230, 231, 232, 300, 301, 302, 310, 311, 312, 313, 314, 321, 500, 501, 502, 503, 504, 511, 520, 521, 522, 531 };
        private static HashSet<int> weatherHash_Snow = new HashSet<int>() { 600, 601, 602, 611, 612, 615, 616, 220, 621, 622 };
        private static HashSet<int> weatherHash_Clear = new HashSet<int>() { 800 };
        private static HashSet<int> weatherHash_Clouds = new HashSet<int>() { 701, 801, 802, 803, 804 };
        public static void setWeatherIcon()
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
    }
}
