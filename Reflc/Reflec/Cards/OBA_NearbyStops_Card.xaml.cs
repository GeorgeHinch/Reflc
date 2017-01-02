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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Reflec.Cards
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OBA_NearbyStops_Card : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            OBA_NearbyStop nearbyStop = e.Parameter as OBA_NearbyStop;

            #region Set location
            TransitStopInfo_TextBlock.Text = "Seattle";
            #endregion

            #region Set stop 1 info
            OBA_Stop1_StopNum.Text = nearbyStop.data.list[0].code.ToString();

            OBA_Stop1_StopName.Text = nearbyStop.data.list[0].name + ", " + nearbyStop.data.list[0].direction;
            OBA_Stop1_Routes.Text = DataBuilder.obaRouteBuilder(nearbyStop, nearbyStop.data.list[0].routeIds);
            #endregion

            #region Set stop 2 info
            OBA_Stop1_StopNum.Text = nearbyStop.data.list[1].code.ToString();

            OBA_Stop1_StopName.Text = nearbyStop.data.list[1].name + ", " + nearbyStop.data.list[1].direction;
            OBA_Stop1_Routes.Text = DataBuilder.obaRouteBuilder(nearbyStop, nearbyStop.data.list[1].routeIds);
            #endregion

            #region Set stop 3 info
            OBA_Stop3_StopNum.Text = nearbyStop.data.list[2].code.ToString();

            OBA_Stop3_StopName.Text = nearbyStop.data.list[2].name + ", " + nearbyStop.data.list[2].direction;
            OBA_Stop3_Routes.Text = DataBuilder.obaRouteBuilder(nearbyStop, nearbyStop.data.list[2].routeIds);
            #endregion

            #region Set stop 4 info
            OBA_Stop4_StopNum.Text = nearbyStop.data.list[3].code.ToString();

            OBA_Stop4_StopName.Text = nearbyStop.data.list[3].name + ", " + nearbyStop.data.list[3].direction;
            OBA_Stop4_Routes.Text = DataBuilder.obaRouteBuilder(nearbyStop, nearbyStop.data.list[3].routeIds);
            #endregion

            #region Set stop 5 info
            OBA_Stop5_StopNum.Text = nearbyStop.data.list[4].code.ToString();

            OBA_Stop5_StopName.Text = nearbyStop.data.list[4].name + ", " + nearbyStop.data.list[4].direction;
            OBA_Stop5_Routes.Text = DataBuilder.obaRouteBuilder(nearbyStop, nearbyStop.data.list[4].routeIds);
            #endregion

            #region Set stop 6 info
            OBA_Stop6_StopNum.Text = nearbyStop.data.list[5].code.ToString();

            OBA_Stop6_StopName.Text = nearbyStop.data.list[5].name + ", " + nearbyStop.data.list[5].direction;
            OBA_Stop6_Routes.Text = DataBuilder.obaRouteBuilder(nearbyStop, nearbyStop.data.list[5].routeIds);
            #endregion
        }

        public OBA_NearbyStops_Card()
        {
            this.InitializeComponent();
        }
    }
}
