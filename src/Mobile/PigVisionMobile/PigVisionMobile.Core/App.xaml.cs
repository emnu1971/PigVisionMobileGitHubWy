//#define TEST
using System;
using System.Globalization;
using PigVisionMobile.Core.Helpers;
using PigVisionMobile.Services;
using PigVisionMobile.Core.ViewModels.Base;
using System.Threading.Tasks;
using PigVisionMobile.Core.Models.Location;
using PigVisionMobile.Core.Services.Location;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PigVisionMobile.Core.Localization;
using PigVisionMobile.Core.Views;
using System.Diagnostics;
using PigVisionMobile.Core.Views.Test;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PigVisionMobile
{
    public partial class App : Application
    {
        public bool UseMockServices { get; set; }

        //BOOT: 1. App Constructor
        public App()
        {
            try
            {
                InitializeComponent();

#if TEST
                //UNDO: For Test Purpose
                MainPage = new NavigationPage(new ResourceDictionaryView());
#else 

                //BOOT: 2. Call InitApp() : Application initialization
                InitApp();

                if (Device.RuntimePlatform == Device.Windows)
                {
                    //BOOT: 4. Initialize the Navigation (Win Only)
                    InitNavigation();
                }
#endif
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error occured in: {nameof(App)} C'tor, error: {ex.Message}");
            }
        }

        //BOOT: 3. InitApp(): Application Initialization
        private void InitApp()
        {
            // enable localization
            LocalizationTools.InitLocalization();

            //TODO: UseMockServices : Reset to settings
            UseMockServices = true;
            //UseMockServices = Settings.UseMocks;

            //INFO: Register the Dependencies
            ViewModelLocator.RegisterDependencies(UseMockServices);
        }

        //BOOT: 4. InitNavigation Resolves the Navigation Service
        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }


        protected override async void OnStart()
        {
            base.OnStart();

			if (Device.RuntimePlatform != Device.Windows)
            {
                await InitNavigation();
            }

            if (Settings.AllowGpsLocation && !Settings.UseFakeLocation)
            {
                await GetGpsLocation();
            }

            if (!Settings.UseMocks && !string.IsNullOrEmpty(Settings.AuthAccessToken))
            {
                await SendCurrentLocation();
            }

            base.OnResume();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        private async Task GetGpsLocation()
        {
            var locator = CrossGeolocator.Current;

            if (locator.IsGeolocationEnabled && locator.IsGeolocationAvailable)
            { 
                locator.AllowsBackgroundUpdates = true;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync();

                Settings.Latitude = position.Latitude.ToString();
                Settings.Longitude = position.Longitude.ToString();
            }
            else
            {
                Settings.AllowGpsLocation = false;
            }
        }

        private async Task SendCurrentLocation()
        {
            var location = new Location
            {
                Latitude = double.Parse(Settings.Latitude, CultureInfo.InvariantCulture),
                Longitude = double.Parse(Settings.Longitude, CultureInfo.InvariantCulture)
            };

            var locationService = ViewModelLocator.Resolve<ILocationService>();
            await locationService.UpdateUserLocation(location,
                Settings.AuthAccessToken);
        }
    }
}