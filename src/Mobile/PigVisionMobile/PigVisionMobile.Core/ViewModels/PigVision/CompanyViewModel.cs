using CompanyDtoModel;
using PigVisionMobile.Core.Exceptions;
using PigVisionMobile.Core.Localization.Resx;
using PigVisionMobile.Core.Services.PigVision.Company;
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
    /// Date        : 31/08/2017
    /// Purpose     : PigVision Company ViewModel
    /// </summary>
    public partial class CompanyViewModel : ViewModelBase
    {
        #region Private Storage

        private string _currentUserName;
        private ObservableCollection<CompanyDto> _companies;
        private ICompanyService _companyService;
        private CompanyDto _selectedCompany;

        #endregion Private Storage

        #region Public Properties

        #region Bindable Properties


        public string CurrentUserName
        {
            get
            {
                return _currentUserName;
            }
            set
            {
                _currentUserName = value;
                RaisePropertyChanged(() => CurrentUserName);
            }
        }

        public ObservableCollection<CompanyDto> Companies
        {
            get { return _companies; }
            set
            {
                _companies = value;
                RaisePropertyChanged(() => Companies);
            }
        }

        public CompanyDto SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                _selectedCompany = value;
                RaisePropertyChanged(() => SelectedCompany);
            }
        }

        #endregion Bindable Properties

        #region Commands

        public ICommand SelectedCompanyCommand { get; private set; }

        #endregion Commands

        #endregion Public Properties

        #region C'tor

        public CompanyViewModel(ICompanyService pCompanyService)
        {
            try
            {
                _companyService = pCompanyService;
                CurrentUserName = GlobalSetting.Instance != null ? GlobalSetting.Instance.CurrentUserName : "";
                SelectedCompanyCommand = new Command<CompanyDto>(async c => await SelectCompanyAsync(c));

            }
            catch(Exception ex)
            {
                var viewModelName = this.GetType().FullName;
                var methodName = nameof(InitializeAsync);
                var exMessage = ex.InnerException == null ? string.Format($"ErrorMessage: {ex.Message}") : string.Format($"ErrorMessage: {ex.Message}, " +
                    $"InnerException Message: {ex.InnerException.Message}");
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

                //TODO: set userid as argument for GetCompaniesAsync()
                // get companies (with some fake user id)
                Companies = await _companyService.GetCompaniesAsync(GlobalSetting.Instance.CurrentUserName);
            }
            catch(Exception ex)
            {
                var viewModelName = this.GetType().FullName;
                var methodName = nameof(InitializeAsync);
                var exMessage = ex.InnerException == null ? string.Format($"ErrorMessage: {ex.Message}") : string.Format($"ErrorMessage: {ex.Message}, InnerException Message: {ex.InnerException.Message}");
                await NavigationService.DisplayAlert(AppResources.ErrorOccured, new ViewModelMethodExecutionException(viewModelName, methodName, exMessage).Message, "Ok");
                throw ex;
            }
            finally
            {
                IsBusy = false;
            }

        }


        #endregion Public Interface

        #region Private Interface

        #region Command Logic

        private async Task SelectCompanyAsync(CompanyDto pSelectedCompany)
        {
           try
           {
                if (pSelectedCompany == null)
                    return;

                await NavigationService.DisplayAlert("Company selected", $"Company: {pSelectedCompany.Name} has been selected !", "Ok");

                // add the current company id to the global settings instance
                GlobalSetting.Instance.CurrentCompanyId = pSelectedCompany.Id;

                // navigate to the sows of the current company
                await NavigationService.NavigateToAsync<SowListViewModel>();
                await NavigationService.RemoveLastFromBackStackAsync();

            }
           catch(Exception ex)
           {
                var viewModelName = this.GetType().FullName;
                var commandName = nameof(SelectCompanyAsync);
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
