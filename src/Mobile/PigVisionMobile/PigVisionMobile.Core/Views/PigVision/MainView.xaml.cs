using PigVisionMobile.Core.Exceptions;
using PigVisionMobile.Core.Localization.Resx;
using PigVisionMobile.Core.ViewModels.Base;
using PigVisionMobile.Core.ViewModels.PigVision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PigVisionMobile.Core.Views.PigVision
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 31/08/2017
    /// Purpose     : PigVision Main View
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : TabbedPage
    {
        #region Private Storage
        #endregion Private Storage

        #region C'tor

        public MainView()
        {
            InitializeComponent();
        }

        #endregion C'tor

        #region Public Interface
        #endregion Public Interface

        #region Protected Interface

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                MessagingCenter.Subscribe<MainViewModel, int>(this, MessageKeys.ChangeTab, (sender, arg) =>
                {
                    switch (arg)
                    {
                        case 0:
                            CurrentPage = _companyView;
                            break;
                        case 1:
                            CurrentPage = _sowProfileView;
                            break;
                        case 2:
                            CurrentPage = _sowActionView;
                            break;
                        case 3:
                            break;
                    }
                });

                // show the company view as default
                await ((CompanyViewModel)_companyView.BindingContext).InitializeAsync(null);
                await ((SowProfileViewModel)_sowProfileView.BindingContext).InitializeAsync(null);
                await ((SowActionViewModel)_sowActionView.BindingContext).InitializeAsync(null);
            }
            catch(Exception ex)
            {
                var viewName = this.GetType().FullName;
                var methodName = nameof(OnAppearing);
                var exMessage = ex.InnerException == null ? string.Format($"ErrorMessage: {ex.Message}") : string.Format($"ErrorMessage: {ex.Message}, InnerException Message: {ex.InnerException.Message}");
                await DisplayAlert(AppResources.ErrorOccured,new ViewMethodExecutionException(viewName,methodName,exMessage).Message,"Ok");
                throw ex;
            }
        }

        protected override async void OnCurrentPageChanged()
        {
            try
            {
                base.OnCurrentPageChanged();

                if (CurrentPage is CompanyView)
                {
                    // Force company view refresh every time we access it
                    await (_companyView.BindingContext as ViewModelBase).InitializeAsync(null);
                }
                else if (CurrentPage is SowProfileView)
                {
                    // Force sow profile view refresh every time we access it
                    await (_sowProfileView.BindingContext as ViewModelBase).InitializeAsync(null);
                }
                else if (CurrentPage is SowActionView)
                {
                    // Force sow action view refresh every time we access it
                    await (_sowActionView.BindingContext as ViewModelBase).InitializeAsync(null);
                }
            }
            catch(Exception ex)
            {
                var viewName = this.GetType().FullName;
                var methodName = nameof(OnCurrentPageChanged);
                var exMessage = ex.InnerException == null ? string.Format($"ErrorMessage: {ex.Message}") : string.Format($"ErrorMessage: {ex.Message}, InnerException Message: {ex.InnerException.Message}");
                await DisplayAlert(AppResources.ErrorOccured, new ViewMethodExecutionException(viewName, methodName, exMessage).Message, "Ok");
                throw ex;
            }
            
        }

        #endregion Protected Interface

    }
}