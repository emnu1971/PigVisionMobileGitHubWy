using PigVisionMobile.Core.Helpers;
using PigVisionMobile.Core.ViewModels;
using PigVisionMobile.Core.Views;
using PigVisionMobile.Core.ViewModels.Base;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using PigVisionMobile.Core.Services.Navigation;

namespace PigVisionMobile.Services
{
    public class NavigationService : INavigationService
    {
        #region INavigationService Implementation

        public ViewModelBase PreviousPageViewModel
		{
			get
			{
				var mainPage = Application.Current.MainPage as CustomNavigationView;
				var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
				return viewModel as ViewModelBase;
			}
		}

        //BOOT: 5. Navigate to the PigVisionLoginViewModel in case not already authenticated, to the MainViewModel in case authenticated
        public Task InitializeAsync()
        {
            //UNDO : Force login
            Settings.AuthAccessToken = string.Empty;
            if (string.IsNullOrEmpty(Settings.AuthAccessToken))
                //UNDO: Set to PigVision Login
                //return NavigateToAsync<LoginViewModel>();
                return NavigateToAsync<PigVisionMobile.Core.ViewModels.PigVision.LoginViewModel>();
            else
                return NavigateToAsync<MainViewModel>();
        }

        //BOOT: 6. Delegates the LoginViewModel Creation to InternalNavigateToAsync().
        //LOGIN: 2. Delegates the MainViewModel Creation to InternalNavigateToAsync().
        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        //BOOT: 7. Resolves View from ViewModel, Creates View and sets ViewModel as BindingContext.
        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, parameter);

            if (page is LoginView)
            {
                Application.Current.MainPage = new CustomNavigationView(page);
            }
            else
			{
                var navigationPage = Application.Current.MainPage as CustomNavigationView;
                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    Application.Current.MainPage = new CustomNavigationView(page);
                }
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
		{
            // as we distinct between controller and data view models, get rid of the prefix
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
			var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
			var viewType = Type.GetType(viewAssemblyName);
			return viewType;
		}

        private Page CreatePage(Type viewModelType, object parameter)
		{
			Type pageType = GetPageTypeForViewModel(viewModelType);
			if (pageType == null)
			{
				throw new Exception($"Cannot locate page type for {viewModelType}");
			}

			Page page = Activator.CreateInstance(pageType) as Page;
			return page;
		}

        #endregion INavigationService Implementation


        #region IPageService Implementation

        public async Task DisplayAlert(string title, string message, string ok)
        {
            await MainPage.DisplayAlert(title, message, ok);
        }

        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await MainPage.DisplayAlert(title, message, ok, cancel);
        }

        public async Task PushAsync(Page page)
        {
            await MainPage.Navigation.PushAsync(page);
        }

        public async Task<Page> PopAsync()
        {
            return await MainPage.Navigation.PopAsync();
        }

        private Page MainPage
        {
            get { return Application.Current.MainPage; }
        }

        #endregion IPageService Implementation
    }
}