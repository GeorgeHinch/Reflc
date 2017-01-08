using Reflec.Classes;
using System;
using System.Collections.Generic;
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
    public sealed partial class Flight_Card : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FlightStatsTotal flight = e.Parameter as FlightStatsTotal;

            AirlineFlight_Textblock.Text = flight.stats.payload.aircraftData.aicraftOperator + " (" + flight.flightNum + ")";
            AirlineStatus_Textblock.Text = DataBuilder.buildFlightStatus(flight.stats);

            FlightProg_ProgressBar.Value = DataBuilder.buildFlightProgress(flight.stats);
            if(FlightProg_ProgressBar.Value == 0)
            {
                FlightProg_Start_Ellipse.Fill = new SolidColorBrush(Color.FromArgb(255, 228, 228, 228));
                FlightProg_End_Ellipse.Fill = new SolidColorBrush(Color.FromArgb(255, 228, 228, 228));
            }
            else if (flight.stats.payload.statusData.arrSchdUTC >= flight.stats.payload.statusData.arrEstUTC)
            {
                FlightProg_Start_Ellipse.Fill = new SolidColorBrush(Color.FromArgb(255, 62, 220, 129));
                FlightProg_ProgressBar.Foreground = new SolidColorBrush(Color.FromArgb(255, 62, 220, 129));
                if(FlightProg_ProgressBar.Value == 100)
                {
                    FlightProg_End_Ellipse.Fill = new SolidColorBrush(Color.FromArgb(255, 62, 220, 129));
                }
            }
            else if (flight.stats.payload.statusData.arrSchdUTC < flight.stats.payload.statusData.arrEstUTC)
            {
                FlightProg_Start_Ellipse.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 108, 92));
                FlightProg_ProgressBar.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 108, 92));
                if (FlightProg_ProgressBar.Value == 100)
                {
                    FlightProg_End_Ellipse.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 108, 92));
                }
            }

            DepartAirport_Textblock.Text = flight.stats.payload.flightData.departureApt;
            ArriveAirport_Textblock.Text = flight.stats.payload.flightData.arrivalApt;

            DepartTime_Textblock.Text = DataBuilder.epochAMPM(flight.stats.payload.statusData.depActLOC.Value);
            DepartSchedTime_Textblock.Text = "sched. " + DataBuilder.epochAMPM(flight.stats.payload.statusData.depSchdLOC);

            ArriveTime_Textblock.Text = DataBuilder.epochAMPM(flight.stats.payload.statusData.arrEstLOC);
            ArriveSchedTime_Textblock.Text = "sched. " + DataBuilder.epochAMPM(flight.stats.payload.statusData.arrSchdLOC);

            GateInfo_Textblock.Text = "Arrival Gate: " + DataBuilder.buildFlightGate(flight.stats);

            FlightAdditionalInfo_TextBlock.Text = flight.flightNum + " - " + AirlineStatus_Textblock.Text;
        }

        public Flight_Card()
        {
            this.InitializeComponent();
        }
    }
}
