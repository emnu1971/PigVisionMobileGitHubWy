using PigVisionMobile.Core.Exceptions;
using PigVisionMobile.Core.Localization.Resx;
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
    /// Date        : 07-09-2017
    /// Purpose     : SowList View : Shows a list of Sows for the Selected Company
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SowListView : ContentPage
    {
        #region Private Storage
        #endregion Private Storage

        #region Public Properties

        #region Bindable Properties
        #endregion Bindable Properties

        #endregion Public Properties

        #region C'tor

        public SowListView()
        {
            InitializeComponent();
        }

        #endregion C'tor

        #region Public Interface

        #endregion Public Interface

        #region Protected Interface
        #endregion Protected Interface

        #region Private Interface

        #region Command Logic

        //TODO: This should be replaced by an "EventToCommandBehavior" in the bound ListView, so this ViewBehind Code can be deleted ...
        private async void OnSowSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                ((SowListViewModel)this.BindingContext).SelectedSowCommand.Execute(e.SelectedItem);
            }
            catch (Exception ex)
            {
                var viewName = this.GetType().FullName;
                var methodName = nameof(OnSowSelected);
                var exMessage = ex.InnerException == null ? string.Format($"ErrorMessage: {ex.Message}") : 
                    string.Format($"ErrorMessage: {ex.Message}, InnerException Message: {ex.InnerException.Message}");
                await DisplayAlert(AppResources.ErrorOccured, new ViewMethodExecutionException(viewName, methodName, exMessage).Message, "Ok");
                throw ex;
            }
        }

        #endregion Command Logic

        #endregion Private Interface

    }
}