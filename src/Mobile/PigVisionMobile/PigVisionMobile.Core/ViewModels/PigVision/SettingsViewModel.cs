using PigVisionMobile.Core.Helpers;
using PigVisionMobile.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PigVisionMobile.Core.ViewModels.PigVision
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 14-09-2017
    /// Purpose     : Settings View Model
    /// </summary>
    public partial class SettingsViewModel : ViewModelBase
    {
        #region Private Storage

        private bool _useAzureServices;
        private string _titleUseAzureServices;
        private string _descriptionUseAzureServices;
        private string _endpoint;

        #endregion Private Storage

        #region Public Properties

        #region Bindable Properties

        public string TitleUseAzureServices
        {
            get => _titleUseAzureServices;
            set
            {
                _titleUseAzureServices = value;
                RaisePropertyChanged(() => TitleUseAzureServices);
            }
        }

        public string DescriptionUseAzureServices
        {
            get => _descriptionUseAzureServices;
            set
            {
                _descriptionUseAzureServices = value;
                RaisePropertyChanged(() => DescriptionUseAzureServices);
            }
        }

        public bool UseAzureServices
        {
            get => _useAzureServices;
            set
            {
                _useAzureServices = value;

                UpdateUseAzureServices();

                RaisePropertyChanged(() => UseAzureServices);
            }
        }

        public string Endpoint
        {
            get => _endpoint;
            set
            {
                _endpoint = value;

                if (!string.IsNullOrEmpty(_endpoint))
                {
                    UpdateEndpoint();
                }

                RaisePropertyChanged(() => Endpoint);
            }
        }


        #endregion Bindable Properties

        #region Commands

        public ICommand ToggleMockServicesCommand => new Command(async () => await ToggleMockServicesAsync());

        #endregion Commands

        #endregion Public Properties

        #region C'tor

        public SettingsViewModel()
        {
            _useAzureServices = !Settings.UseMocks;
            _endpoint = Settings.UrlBase;
        }

        #endregion C'tor

        #region Public Interface

        public override Task InitializeAsync(object navigationData)
        {
            UpdateInfoUseAzureServices();
            return base.InitializeAsync(navigationData);
        }

        #endregion Public Interface

        #region Protected Interface
        #endregion Protected Interface

        #region Private Interface

        private void UpdateInfoUseAzureServices()
        {
            if (!UseAzureServices)
            {
                TitleUseAzureServices = "Use Mock Services";
                DescriptionUseAzureServices = "Mock Services are simulated objects that mimic the behavior of real services using a controlled approach.";
            }
            else
            {
                TitleUseAzureServices = "Use Microservices/Containers from PigVisionMobile";
                DescriptionUseAzureServices = "When enabling the use of microservices/containers, the app will attempt to use real services deployed as Docker containers at the specified base endpoint, which will must be reachable through the network.";
            }
        }

        private void UpdateUseAzureServices()
        {
            // Save use mocks services to local storage
            Settings.UseMocks = !_useAzureServices;
        }

        private void UpdateEndpoint()
        {
            // Update remote endpoint (save to local storage)
            GlobalSetting.Instance.BaseEndpoint = Settings.UrlBase = _endpoint;
        }

        #region Command Logic

        private async Task ToggleMockServicesAsync()
        {
           await Task.Run(() =>
           {
                // register the real or mock services
                ViewModelLocator.RegisterDependencies(!UseAzureServices);

                // update the UI
                UpdateInfoUseAzureServices();
           });

            
        }

        #endregion Command Logic

        #region Validation Logic
        #endregion Validation Logic

        #endregion Private Interface
    }
}
