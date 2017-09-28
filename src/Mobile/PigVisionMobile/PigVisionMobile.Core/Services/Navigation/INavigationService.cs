using PigVisionMobile.Core.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PigVisionMobile.Services
{
    public interface INavigationService
    {
		ViewModelBase PreviousPageViewModel { get; }

        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();

        Task PushAsync(Page pPage);
        Task<Page> PopAsync();

        Task<bool> DisplayAlert(string pTitle, string pMessage, string pOk, string pCancel);
        Task DisplayAlert(string pTitle, string pMessage, string pOk);

    }
}