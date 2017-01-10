using Reflec.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public sealed partial class Horoscope_card : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HoroscopeData horoscope = e.Parameter as HoroscopeData;

            switch (horoscope.sunsign)
            {
                case "aquarius":
                    Horoscope_SignIcon_Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Signs/Sign_Aquarius.png"));
                    break;
                case "aries":
                    Horoscope_SignIcon_Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Signs/Sign_Aires.png"));
                    break;
                case "caner":
                    Horoscope_SignIcon_Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Signs/Sign_Cancer.png"));
                    break;
                case "capicorn":
                    Horoscope_SignIcon_Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Signs/Sign_Capicorn.png"));
                    break;
                case "gemini":
                    Horoscope_SignIcon_Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Signs/Sign_Gemini.png"));
                    break;
                case "leo":
                    Horoscope_SignIcon_Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Signs/Sign_Leo.png"));
                    break;
                case "libra":
                    Horoscope_SignIcon_Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Signs/Sign_Libra.png"));
                    break;
                case "ophiuchus":
                    Horoscope_SignIcon_Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Signs/Sign_Ophiuchus.png"));
                    break;
                case "pisces":
                    Horoscope_SignIcon_Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Signs/Sign_Pisces.png"));
                    break;
                case "sagittarius":
                    Horoscope_SignIcon_Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Signs/Sign_Sagittarius.png"));
                    break;
                case "scorpio":
                    Horoscope_SignIcon_Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Signs/Sign_Scorpio.png"));
                    break;
                case "taurus":
                    Horoscope_SignIcon_Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Signs/Sign_Taurus.png"));
                    break;
                case "virgo":
                    Horoscope_SignIcon_Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Signs/Sign_Virgo.png"));
                    break;
                default:
                    Horoscope_SignIcon_Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Signs/Sign_Unknown.png"));
                    break;
            }

            Horoscope_SignHoroscope_Textblock.Text = horoscope.horoscope;

            DateTime horoscopeDate = DateTime.ParseExact(horoscope.date, "dd-MM-yyyy", CultureInfo.InvariantCulture).AddDays(-1);
            Horoscope_AdditionalInfo_TextBlock.Text = horoscopeDate.ToString("dddd, MMM. d, yyyy");
        }

        public Horoscope_card()
        {
            this.InitializeComponent();
        }
    }
}
