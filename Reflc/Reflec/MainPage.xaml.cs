using Reflec.Assets.Weather;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Reflec
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage mainPage;
        public MainPage()
        {
            this.InitializeComponent();
            mainPage = this;

            DataBuilder.setWeatherAnimation();
        }

        private void Rain_Click(object sender, RoutedEventArgs e)
        {
            WeartherAnimation_Frame.Navigate(typeof(Rain_Animation), false);
        }

        private void Snow_Click(object sender, RoutedEventArgs e)
        {
            WeartherAnimation_Frame.Navigate(typeof(Snow_Animation), false);
        }

        private void Sun_Click(object sender, RoutedEventArgs e)
        {
            WeartherAnimation_Frame.Navigate(typeof(Sun_Animation), false);
        }

        private void Sun_Cloud_Click(object sender, RoutedEventArgs e)
        {
            WeartherAnimation_Frame.Navigate(typeof(Sun_Cloud_Animation), false);
        }

        private void Moon_Click(object sender, RoutedEventArgs e)
        {
            WeartherAnimation_Frame.Navigate(typeof(Moon_Animation), false);
        }

        private void Moon_Cloud_Click(object sender, RoutedEventArgs e)
        {
            WeartherAnimation_Frame.Navigate(typeof(Moon_Cloud_Animation), false);
        }

        private void Clouds_Click(object sender, RoutedEventArgs e)
        {
            WeartherAnimation_Frame.Navigate(typeof(Cloudy_Animation), false);
        }

        private void LoadWeather_Click(object sender, RoutedEventArgs e)
        {
            OWM_GetWeather.getForecast();
        }
    }
}
