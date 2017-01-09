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
    public sealed partial class Sport_Card : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Event sportEvent = e.Parameter as Event;

            Sports_TeamLogo1_Image.Source = new BitmapImage(new Uri(sportEvent.competitors[0].logo));
            Sports_TeamName1_Textblock.Text = sportEvent.competitors[0].name;

            Sports_TeamLogo2_Image.Source = new BitmapImage(new Uri(sportEvent.competitors[1].logo));
            Sports_TeamName2_Textblock.Text = sportEvent.competitors[1].name;

            SportsAdditionalInfo_TextBlock.Text = sportEvent.weekText + ", " + sportEvent.location;

            if (sportEvent.status == "pre")
            {
                Sports_GameScore_Stackpanel.Visibility = Visibility.Collapsed;
                Sports_PreGame_Stackpanel.Visibility = Visibility.Visible;

                Sports_PreLocation_Textblock.Text = sportEvent.location;
                Sports_PreNetwork_Textblock.Text = sportEvent.broadcast;

                DateTime eventDate = DateTime.ParseExact("2017-01-08T01:15:00Z", "yyyy-MM-ddThh:mm:ssZ", CultureInfo.InvariantCulture);
                Sports_PreStatus_Textblock.Text = eventDate.ToString("dddd, MMM d, hh:mm tt");
            }
            else
            {
                Sports_GameScore_Stackpanel.Visibility = Visibility.Visible;
                Sports_PreGame_Stackpanel.Visibility = Visibility.Collapsed;

                Sports_Score_Textblock.Text = sportEvent.competitors[0].score + " - " + sportEvent.competitors[1].score;
                Sports_Status_Textblock.Text = sportEvent.fullStatus.type.detail;
            }
        }

        public Sport_Card()
        {
            this.InitializeComponent();
        }
    }
}
