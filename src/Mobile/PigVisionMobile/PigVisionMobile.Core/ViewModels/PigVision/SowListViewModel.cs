//#define USE_SETTINGS
using AnimalDtoModel;
using PigVisionMobile.Core.Exceptions;
using PigVisionMobile.Core.Localization.Resx;
using PigVisionMobile.Core.Services.PigVision.Sow;
using PigVisionMobile.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PigVisionMobile.Core.ViewModels.PigVision
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 05-09-2017
    /// Purpose     : PigVision SowList ViewModel
    /// </summary>
    public partial class SowListViewModel : ViewModelBase
    {
        #region Private Storage

        private ObservableCollection<SowDto> _sows;
        private ISowService _sowService;
        private int _currentCompanyId;
        private SowDto _selectedSow;

        #endregion Private Storage

        #region Public Properties

        #region Bindable Properties

        public ObservableCollection<SowDto> Sows
        {
            get { return _sows; }
            set
            {
                _sows = value;
                RaisePropertyChanged(() => Sows);
            }
        }

        public SowDto SelectedSow
        {
            get { return _selectedSow; }
            set
            {
                _selectedSow = value;
                RaisePropertyChanged(() => SelectedSow);
            }
        }

        #endregion Bindable Properties

        #region Commands

        public ICommand SelectedSowCommand { get; private set; }

        #endregion Commands

        #endregion Public Properties

        #region C'tor

        public SowListViewModel(ISowService pSowService)
        {
            try
            {
                _sowService = pSowService;

                _currentCompanyId = GlobalSetting.Instance != null ? GlobalSetting.Instance.CurrentCompanyId : -1;

                SelectedSowCommand = new Command<SowDto>(async s => await SelectedSowAsync(s));
            }
            catch(Exception ex)
            {
                var viewModelName = this.GetType().FullName;
                var methodName = nameof(SowListViewModel);
                var exMessage = ex.InnerException == null ? string.Format($"ErrorMessage: {ex.Message}") : 
                    string.Format($"ErrorMessage: {ex.Message}, InnerException Message: {ex.InnerException.Message}");
                NavigationService.DisplayAlert(AppResources.ErrorOccured, 
                    new ViewModelMethodExecutionException(viewModelName, methodName, exMessage).Message, "Ok");
                throw ex;
            }
        }
              

        #endregion C'tor

        #region Public Interface

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                IsBusy = true;
                // get sows for the selected company
                Sows = await _sowService.GetSowsAsync(_currentCompanyId);
            }
            catch(Exception ex)
            {
                var viewModelName = this.GetType().FullName;
                var methodName = nameof(InitializeAsync);
                var exMessage = ex.InnerException == null ? string.Format($"ErrorMessage: {ex.Message}") : 
                    string.Format($"ErrorMessage: {ex.Message}, InnerException Message: {ex.InnerException.Message}");
                await NavigationService.DisplayAlert(AppResources.ErrorOccured, 
                    new ViewModelMethodExecutionException(viewModelName, methodName, exMessage).Message, "Ok");
                throw ex;
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion Public Interface

        #region Protected Interface
        #endregion Protected Interface

        #region Private Interface

        #region Command Logic

        private async Task SelectedSowAsync(SowDto pSelectedSow)
        {
            try
            {
                if (pSelectedSow == null)
                    return;

                await NavigationService.DisplayAlert("Sow selected", $"Sow: {pSelectedSow.Name} has been selected !", "Ok");


#if USE_SETTINGS

                //--- Navigation by adding the selected sow to the global settings

                // add the selected sow to the global settings
                GlobalSetting.Instance.CurrentSelectedSowDto = pSelectedSow;
                // navigate to the sow detail view for the selected sow
                await NavigationService.NavigateToAsync<SowDetailsViewModel>();
#else
                //--- Navigation by passing the selected sow as a parameter of the destination View-Model
                await NavigationService.NavigateToAsync<SowDetailsViewModel>(pSelectedSow);

#endif


                await NavigationService.RemoveLastFromBackStackAsync();

            }
            catch(Exception ex)
            {
                var viewModelName = this.GetType().FullName;
                var commandName = nameof(SelectedSowAsync);
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
