using Reflec_Companion.Classes;
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

namespace Reflec_Companion
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            Emails = new List<Email>
            {
                new Email { From = "Steve Johnson", Subject = "Lunch Tomorrow", Body = "Are you available for lunch tomorrow? A client would like to discuss a project with you." },
                new Email { From = "Becky Davidson", Subject = "Kids game", Body = "Don't forget the kids have their soccer game this Friday. We have to supply end of game snacks." },
                new Email { From = "OneDrive", Subject = "Check out your event recap", Body = "Your new album.\r\nYou uploaded some photos to yuor OneDrive and automatically created an album for you." },
                new Email { From = "Twitter", Subject = "Follow randomPerson, APersonYouMightKnow", Body = "Here are some people we think you might like to follow:\r\n.@randomPerson\r\nAPersonYouMightKnow" },
            };

            Menu = new List<ReflecCompanion_Menu>
            {
                new ReflecCompanion_Menu { glyph = "", title = "Addresses" },
                new ReflecCompanion_Menu { glyph = "", title = "Uber Authentication" },
            };

            this.InitializeComponent();
        }

        /*protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var propertyDesc = e.Parameter as PropertyDescriptor;

            if (propertyDesc != null)
            {
                DataContext = propertyDesc.Expando;
            }
        }*/

        private ICollection<Email> Emails { get; set; }

        private ICollection<ReflecCompanion_Menu> Menu { get; set; }
    }
}
