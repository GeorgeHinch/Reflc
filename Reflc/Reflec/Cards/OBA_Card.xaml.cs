using Reflec.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Reflec.Cards
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OBA_Card : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            OBA_StopAD arrival = e.Parameter as OBA_StopAD;

            #region Set location
            TransitStopInfo_TextBlock.Text = arrival.data.references.stops[0].name;
            #endregion

            #region Set arrival 1 info
            foreach(Route r in arrival.data.references.routes)
            {
                if (arrival.data.entry.arrivalsAndDepartures[0].routeShortName == r.shortName && r.color != "")
                {
                    OBA_Arrival1_Operator_Ellipse.Fill = new SolidColorBrush(DataBuilder.colorfromHexBuilder(r.color));
                }
            }

            OBA_Arrival1_Route_TextBlock.Text = arrival.data.entry.arrivalsAndDepartures[0].routeShortName;
            OBA_Arrival1_Headsign_TextBlock.Text = arrival.data.entry.arrivalsAndDepartures[0].tripHeadsign;

            if (arrival.data.entry.arrivalsAndDepartures[0].predictedArrivalTime.ToString() != "0")
            {
                OBA_Arrival1_Time_TextBlock.Text = DataBuilder.arrivalTimeBuilder(arrival.data.entry.arrivalsAndDepartures[0].predictedArrivalTime);
            }
            else
            {
                OBA_Arrival1_Time_TextBlock.Text = DataBuilder.arrivalTimeBuilder(arrival.data.entry.arrivalsAndDepartures[0].scheduledArrivalTime);
            }
            #endregion

            #region Set arrival 2 info
            foreach (Route r in arrival.data.references.routes)
            {
                if (arrival.data.entry.arrivalsAndDepartures[1].routeShortName == r.shortName && r.color != "")
                {
                    OBA_Arrival2_Operator_Ellipse.Fill = new SolidColorBrush(DataBuilder.colorfromHexBuilder(r.color));
                }
            }

            OBA_Arrival2_Route_TextBlock.Text = arrival.data.entry.arrivalsAndDepartures[1].routeShortName;
            OBA_Arrival2_Headsign_TextBlock.Text = arrival.data.entry.arrivalsAndDepartures[1].tripHeadsign;

            if (arrival.data.entry.arrivalsAndDepartures[1].predictedArrivalTime.ToString() != "0")
            {
                OBA_Arrival2_Time_TextBlock.Text = DataBuilder.arrivalTimeBuilder(arrival.data.entry.arrivalsAndDepartures[1].predictedArrivalTime);
            }
            else
            {
                OBA_Arrival2_Time_TextBlock.Text = DataBuilder.arrivalTimeBuilder(arrival.data.entry.arrivalsAndDepartures[1].scheduledArrivalTime);
            }
            #endregion

            #region Set arrival 3 info
            foreach (Route r in arrival.data.references.routes)
            {
                if (arrival.data.entry.arrivalsAndDepartures[2].routeShortName == r.shortName && r.color != "")
                {
                    OBA_Arrival3_Operator_Ellipse.Fill = new SolidColorBrush(DataBuilder.colorfromHexBuilder(r.color));
                }
            }

            OBA_Arrival3_Route_TextBlock.Text = arrival.data.entry.arrivalsAndDepartures[2].routeShortName;
            OBA_Arrival3_Headsign_TextBlock.Text = arrival.data.entry.arrivalsAndDepartures[2].tripHeadsign;

            if (arrival.data.entry.arrivalsAndDepartures[2].predictedArrivalTime.ToString() != "0")
            {
                OBA_Arrival3_Time_TextBlock.Text = DataBuilder.arrivalTimeBuilder(arrival.data.entry.arrivalsAndDepartures[2].predictedArrivalTime);
            }
            else
            {
                OBA_Arrival3_Time_TextBlock.Text = DataBuilder.arrivalTimeBuilder(arrival.data.entry.arrivalsAndDepartures[2].scheduledArrivalTime);
            }
            #endregion

            #region Set arrival 4 info
            foreach (Route r in arrival.data.references.routes)
            {
                if (arrival.data.entry.arrivalsAndDepartures[3].routeShortName == r.shortName && r.color != "")
                {
                    OBA_Arrival4_Operator_Ellipse.Fill = new SolidColorBrush(DataBuilder.colorfromHexBuilder(r.color));
                }
            }

            OBA_Arrival4_Route_TextBlock.Text = arrival.data.entry.arrivalsAndDepartures[3].routeShortName;
            OBA_Arrival4_Headsign_TextBlock.Text = arrival.data.entry.arrivalsAndDepartures[3].tripHeadsign;

            if (arrival.data.entry.arrivalsAndDepartures[3].predictedArrivalTime.ToString() != "0")
            {
                OBA_Arrival4_Time_TextBlock.Text = DataBuilder.arrivalTimeBuilder(arrival.data.entry.arrivalsAndDepartures[3].predictedArrivalTime);
            }
            else
            {
                OBA_Arrival4_Time_TextBlock.Text = DataBuilder.arrivalTimeBuilder(arrival.data.entry.arrivalsAndDepartures[3].scheduledArrivalTime);
            }
            #endregion

            #region Set arrival 5 info
            foreach (Route r in arrival.data.references.routes)
            {
                if (arrival.data.entry.arrivalsAndDepartures[4].routeShortName == r.shortName && r.color != "")
                {
                    OBA_Arrival5_Operator_Ellipse.Fill = new SolidColorBrush(DataBuilder.colorfromHexBuilder(r.color));
                }
            }

            OBA_Arrival5_Route_TextBlock.Text = arrival.data.entry.arrivalsAndDepartures[4].routeShortName;
            OBA_Arrival5_Headsign_TextBlock.Text = arrival.data.entry.arrivalsAndDepartures[4].tripHeadsign;

            if (arrival.data.entry.arrivalsAndDepartures[4].predictedArrivalTime.ToString() != "0")
            {
                OBA_Arrival5_Time_TextBlock.Text = DataBuilder.arrivalTimeBuilder(arrival.data.entry.arrivalsAndDepartures[4].predictedArrivalTime);
            }
            else
            {
                OBA_Arrival5_Time_TextBlock.Text = DataBuilder.arrivalTimeBuilder(arrival.data.entry.arrivalsAndDepartures[4].scheduledArrivalTime);
            }
            #endregion
        }

        public OBA_Card()
        {
            this.InitializeComponent();
        }
    }
}
