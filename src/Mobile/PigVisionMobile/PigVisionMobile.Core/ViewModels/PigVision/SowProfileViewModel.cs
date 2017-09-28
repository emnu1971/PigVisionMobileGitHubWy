using PigVisionMobile.Core.Exceptions;
using PigVisionMobile.Core.Localization.Resx;
using PigVisionMobile.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigVisionMobile.Core.ViewModels.PigVision
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 06-09-2017
    /// Purpose     : Sow Profile ViewModel
    /// </summary>
    public partial class SowProfileViewModel : ViewModelBase
    {
        #region Private Storage
        #endregion Private Storage

        #region Public Properties

        #region Bindable Properties
        #endregion Bindable Properties

        #region Commands
        #endregion Commands

        #endregion Public Properties

        #region C'tor

        public SowProfileViewModel()
        {

        }

        #endregion C'tor

        #region Public Interface

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                IsBusy = true;
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

        #region Protected Interface
        #endregion Protected Interface

        #region Private Interface

        #region Command Logic
        #endregion Command Logic

        #region Validation Logic
        #endregion Validation Logic

        #endregion Private Interface
    }
}
