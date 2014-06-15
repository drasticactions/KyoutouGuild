using System.Text.RegularExpressions;
using Windows.ApplicationModel.Activation;
using Windows.System;
using KyoutouGuild.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556
using KyoutouGuild.Core.Entities;
using KyoutouGuild.ViewModels;
using Newtonsoft.Json;

namespace KyoutouGuild.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private NavigationHelper navigationHelper;
        private LoginPageViewModel _vm;

        private const string LaunchUrl =
            "https://account.sonyentertainmentnetwork.com/external/auth/login.action?service-entity=psn&returnURL=https://kyoutou-guild.com/session/ticket";
        public LoginPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            _vm = (LoginPageViewModel) DataContext;
            var args = e.NavigationParameter as ProtocolActivatedEventArgs;
            string jsonObjectString;
            LoginUserEntity user;
            if (args != null)
            {
                IReadOnlyDictionary<string, string> queryString = UriExtensions.ParseQueryString(args.Uri);
                user = await _vm.NewLogIn(queryString["sessionId"]);
                jsonObjectString = JsonConvert.SerializeObject(user);
                Frame.Navigate(typeof(MainPage), jsonObjectString);
                return;
            }
            user = await _vm.IsLoggedIn();
            if (user == null) return;
            jsonObjectString = JsonConvert.SerializeObject(user);
            Frame.Navigate(typeof(MainPage), jsonObjectString);
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void LaunchUrl_OnClick(object sender, RoutedEventArgs e)
        {
            Launcher.LaunchUriAsync(new Uri(LaunchUrl));
        }

        public static class UriExtensions
        {
            private static readonly Regex _regex = new Regex(@"[?|&](\w+)=([^?|^&]+)");

            public static IReadOnlyDictionary<string, string> ParseQueryString(Uri uri)
            {
                Match match = _regex.Match(uri.PathAndQuery);
                var paramaters = new Dictionary<string, string>();
                while (match.Success)
                {
                    paramaters.Add(match.Groups[1].Value, match.Groups[2].Value);
                    match = match.NextMatch();
                }
                return paramaters;
            }
        }
    }
}
