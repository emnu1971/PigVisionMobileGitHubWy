using PigVisionMobile.Core.Exceptions;
using PigVisionMobile.Core.Localization.Resx;
using PigVisionMobile.Core.ViewModels.PigVision;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PigVisionMobile.Core.Views.PigVision
{
    /// <summary>
    /// Author:     Emmanuel Nuyttens
    /// Date:       29-08-2017
    /// Purpose:    PigVision Login View
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
        #region Private Storage

        private bool _animate;

        #endregion Private Storage
        
        #region C'tor

        public LoginView ()
		{
            try
            {
                InitializeComponent();
                //_image.Source = ImageSource.FromResource("PigVisionMobile.Core.Images.GreenPigLogo.PNG");
            }
            catch(Exception ex)
            {
                var viewName = this.GetType().FullName;
                var methodName = nameof(LoginView);
                var exMessage = ex.InnerException == null ? string.Format($"ErrorMessage: {ex.Message}") : 
                    string.Format($"ErrorMessage: {ex.Message}, InnerException Message: {ex.InnerException.Message}");
                DisplayAlert(AppResources.ErrorOccured, 
                    new ViewMethodExecutionException(viewName, methodName, exMessage).Message, "Ok");
                throw ex;
            }
        }

        #endregion C'tor

        #region Public Interface

        //public async Task AnimateIn()
        //{
        //    if (Device.RuntimePlatform == Device.Windows)
        //    {
        //        return;
        //    }

        //    await AnimateItem(_banner, 10500);
        //}

        #endregion Public Interface

        #region Protected Interface

        //protected override async void OnAppearing()
        //{
        //    var content = this.Content;
        //    this.Content = null;
        //    this.Content = content;

        //    var vm = BindingContext as LoginViewModel;
        //    if (vm != null)
        //    {
        //        vm.InvalidateMock();

        //        if (!vm.IsMock)
        //        {
        //            _animate = true;
        //            await AnimateIn();
        //        }
        //    }
        //}

        //protected override void OnDisappearing()
        //{
        //    _animate = false;
        //}

        #endregion Protected Interface

        #region Private Interface

        //private async Task AnimateItem(View uiElement, uint duration)
        //{
        //    try
        //    {
        //        while (_animate)
        //        {
        //            await uiElement.ScaleTo(1.05, duration, Easing.SinInOut);
        //            await Task.WhenAll(
        //                uiElement.FadeTo(1, duration, Easing.SinInOut),
        //                uiElement.LayoutTo(new Rectangle(new Point(0, 0), new Size(uiElement.Width, uiElement.Height))),
        //                uiElement.FadeTo(.9, duration, Easing.SinInOut),
        //                uiElement.ScaleTo(1.15, duration, Easing.SinInOut)
        //            );
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
        //}
        #endregion Private Interface

    }
}