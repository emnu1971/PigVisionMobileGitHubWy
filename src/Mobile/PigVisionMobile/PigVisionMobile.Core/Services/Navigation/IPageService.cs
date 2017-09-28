using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PigVisionMobile.Core.Services.Navigation
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : July 2017
    /// Purpose     : Interface for PageServices used in the ViewModels
    /// </summary>
    public interface IPageService
    {
        Task PushAsync(Page pPage);
        Task<Page> PopAsync();

        Task<bool> DisplayAlert(string pTitle, string pMessage, string pOk, string pCancel);
        Task DisplayAlert(string pTitle, string pMessage, string pOk);
    }
}
