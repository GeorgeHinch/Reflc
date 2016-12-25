using Reflec.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Reflec.Cards
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Weather_Card : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            WeatherForecast forecast = e.Parameter as WeatherForecast;

            #region Set location
            WeatherCity_TextBlock.Text = forecast.city.name;
            #endregion

            #region Set day 1 weather
            Forecast_Day1_Date_Image.Source = new BitmapImage(new Uri(DataBuilder.setWeatherIcon(forecast.forecast[0].weather[0].id)));
            Forecast_Day1_Date_TextBlock.Text = "Today";
            Forecast_Day1_TempHigh_TextBlock.Text = Math.Round(forecast.forecast[0].temp.max, 0).ToString() + "°";
            Forecast_Day1_TempLow_TextBlock.Text = Math.Round(forecast.forecast[0].temp.min, 0).ToString() + "°";
            #endregion

            #region Set day 2 weather
            Forecast_Day2_Date_Image.Source = new BitmapImage(new Uri(DataBuilder.setWeatherIcon(forecast.forecast[1].weather[0].id)));
            Forecast_Day2_Date_TextBlock.Text = DataBuilder.dayBuilder(forecast.forecast[1].dt);
            Forecast_Day2_TempHigh_TextBlock.Text = Math.Round(forecast.forecast[1].temp.max, 0).ToString() + "°";
            Forecast_Day2_TempLow_TextBlock.Text = Math.Round(forecast.forecast[1].temp.min, 0).ToString() + "°";
            #endregion

            #region Set day 3 weather
            Forecast_Day3_Date_Image.Source = new BitmapImage(new Uri(DataBuilder.setWeatherIcon(forecast.forecast[2].weather[0].id)));
            Forecast_Day3_Date_TextBlock.Text = DataBuilder.dayBuilder(forecast.forecast[2].dt);
            Forecast_Day3_TempHigh_TextBlock.Text = Math.Round(forecast.forecast[2].temp.max, 0).ToString() + "°";
            Forecast_Day3_TempLow_TextBlock.Text = Math.Round(forecast.forecast[2].temp.min, 0).ToString() + "°";
            #endregion

            #region Set day 4 weather
            Forecast_Day4_Date_Image.Source = new BitmapImage(new Uri(DataBuilder.setWeatherIcon(forecast.forecast[3].weather[0].id)));
            Forecast_Day4_Date_TextBlock.Text = DataBuilder.dayBuilder(forecast.forecast[3].dt);
            Forecast_Day4_TempHigh_TextBlock.Text = Math.Round(forecast.forecast[3].temp.max, 0).ToString() + "°";
            Forecast_Day4_TempLow_TextBlock.Text = Math.Round(forecast.forecast[3].temp.min, 0).ToString() + "°";
            #endregion

            #region Set day 5 weather
            Forecast_Day5_Date_Image.Source = new BitmapImage(new Uri(DataBuilder.setWeatherIcon(forecast.forecast[4].weather[0].id)));
            Forecast_Day5_Date_TextBlock.Text = DataBuilder.dayBuilder(forecast.forecast[4].dt);
            Forecast_Day5_TempHigh_TextBlock.Text = Math.Round(forecast.forecast[4].temp.max, 0).ToString() + "°";
            Forecast_Day5_TempLow_TextBlock.Text = Math.Round(forecast.forecast[4].temp.min, 0).ToString() + "°";
            #endregion

        }
        public Weather_Card()
        {
            this.InitializeComponent();
        }
    }
}
