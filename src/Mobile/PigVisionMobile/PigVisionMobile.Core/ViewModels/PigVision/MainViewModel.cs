using PigVisionMobile.Core.Exceptions;
using PigVisionMobile.Core.Localization.Resx;
using PigVisionMobile.Core.Models.Navigation;
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
    /// Author  :   Emmanuel Nuyttens
    /// Date    :   04-09-2017
    /// Purpose :   Main View Model
    /// </summary>
    public partial class MainViewModel : ViewModelBase
    {
        #region Private Storage
        #endregion Private Storage

        #region Public Properties

        #region Bindable Properties
        #endregion Bindable Properties

        #region Commands

        public ICommand SettingsCommand => new Command(async () => await SettingsAsync());

        #endregion Commands

        #endregion Public Properties

        #region C'tor

        public MainViewModel()
        {

        }

        #endregion C'tor

        #region Public Interface

        public override Task InitializeAsync(object navigationData)
        {
            try
            {
                IsBusy = true;

                if(navigationData is TabParameter)
                {
                    // change the selected application tab
                    var tabIndex = ((TabParameter)navigationData).TabIndex;
                    MessagingCenter.Send(this, MessageKeys.ChangeTab, tabIndex);
                }
                return base.InitializeAsync(navigationData);
            }
            catch(Exception ex)
            {
                var viewModelName = this.GetType().FullName;
                var methodName = nameof(InitializeAsync);
                var exMessage = ex.InnerException == null ? string.Format($"ErrorMessage: {ex.Message}") : string.Format($"ErrorMessage: {ex.Message}, InnerException Message: {ex.InnerException.Message}");
                NavigationService.DisplayAlert(AppResources.ErrorOccured, new ViewModelMethodExecutionException(viewModelName, methodName, exMessage).Message, "Ok");
                throw ex;
            }
            finally
            {
                
            }
            
        }
        #endregion Public Interface

        #region Protected Interface
        #endregion Protected Interface

        #region Private Interface

        #region Command Logic

        private async Task SettingsAsync()
        {

            try
            {
                await NavigationService.NavigateToAsync<SettingsViewModel>();
            }
            catch(Exception ex)
            {
                var viewModelName = this.GetType().FullName;
                var commandName = nameof(SettingsAsync);
                var exMessage = ex.InnerException == null ? string.Format($"ErrorMessage: {ex.Message}") :
                    string.Format($"ErrorMessage: {ex.Message}, InnerException Message: {ex.InnerException.Message}");

                await NavigationService.DisplayAlert(AppResources.ErrorOccured,
                    new ViewModelCommandExecutionException(viewModelName, commandName, exMessage).Message, "Ok");
                throw ex;

            }
            
        }

        #endregion Command Logic

        #region Validation Logic
        #endregion Validation Logic

        #endregion Private Interface
    }
}
