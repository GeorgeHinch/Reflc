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

namespace Reflec.Assets.Weather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Rain_Animation : Page
    {
        private bool? p;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            p = e.Parameter as bool?;

            if (p == true)
            {
                Cloud.Fill = new SolidColorBrush(Color.FromArgb(255, 204, 208, 201));
                path.Fill = new SolidColorBrush(Color.FromArgb(255, 204, 208, 201));
                path1.Fill = new SolidColorBrush(Color.FromArgb(255, 204, 208, 201));
                path2.Fill = new SolidColorBrush(Color.FromArgb(255, 204, 208, 201));
                path3.Fill = new SolidColorBrush(Color.FromArgb(255, 204, 208, 201));
                path4.Fill = new SolidColorBrush(Color.FromArgb(255, 204, 208, 201));
                path5.Fill = new SolidColorBrush(Color.FromArgb(255, 204, 208, 201));
                path6.Fill = new SolidColorBrush(Color.FromArgb(255, 204, 208, 201));
                path7.Fill = new SolidColorBrush(Color.FromArgb(255, 204, 208, 201));
                path8.Fill = new SolidColorBrush(Color.FromArgb(255, 204, 208, 201));
                path9.Fill = new SolidColorBrush(Color.FromArgb(255, 204, 208, 201));
            }
        }

        public Rain_Animation()
        {
            this.InitializeComponent();

            AnimationStart.Begin();
            AnimationStart.Completed += AnimationStart_Completed;
        }

        private void AnimationStart_Completed(object sender, object e)
        {
            AnimationRepeat.Begin();
        }
    }
}
