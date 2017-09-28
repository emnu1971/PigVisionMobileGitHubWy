using PigVisionMobile.Core.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PigVisionMobile.Core.Localization
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : July 2017
    /// Purpose     : Localization Tools
    /// </summary>
    public static class LocalizationTools
    {
        #region Public Interface

        public static void InitLocalization()
        {

            System.Diagnostics.Debug.WriteLine("====== resource debug info =========");
            var assembly = typeof(App).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
            System.Diagnostics.Debug.WriteLine("====================================");

            // This lookup NOT required for Windows platforms - the Culture will be automatically set
            if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
            {
                // determine the correct, supported .NET culture
                var ci = DependencyService.Get<ILocalizeService>().GetCurrentCultureInfo();
                PigVisionMobile.Core.Localization.Resx.AppResources.Culture = ci;  // set the RESX for resource localization
                DependencyService.Get<ILocalizeService>().SetLocale(ci); // set the Thread for locale-aware methods
            }
        }

        #endregion Public Interface
    }
}
