//#define USE_SETTINGS
using AnimalDtoModel;
using PigVisionMobile.Core.Exceptions;
using PigVisionMobile.Core.Helpers;
using PigVisionMobile.Core.Localization.Resx;
using PigVisionMobile.Core.Validations;
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
    /// Date        : 07-09-2017
    /// Purpose     : Represents an entity of a sow which is bound to the UI
    /// </summary>
    public partial class SowDetailsViewModel : ViewModelBase
    {
        #region Private Storage

        private int _sowId;
        private int _companyId;
        private ValidatableObject<string> _name;
        private ValidatableObject<string> _box;
        private SowDto _selectedSowDto;

        #endregion Private Storage

        #region Public Properties

        #region Bindable Properties


        public int SowId
        {
            get { return _sowId; }
            set { _sowId = value; }
        }
               
        public int CompanyId
        {
            get { return _companyId; }
            set { _companyId = value; }
        }

        
        public ValidatableObject<string> Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }
                
        public ValidatableObject<string> Box
        {
            get
            {
                return _box;
            }
            set
            {
                _box = value;
                RaisePropertyChanged(() => Box);
            }
        }

        #endregion Bindable Properties

        #region Commands
        #endregion Commands

        #endregion Public Properties

        #region C'tor

        public SowDetailsViewModel()
        {
            try
            {

                // activate for validation
                _name = new ValidatableObject<string>();
                _box = new ValidatableObject<string>();





                // set mock
                InvalidateMock();

                // add validations
                AddValidations();
            }
            catch (Exception ex)
            {
                var viewModelName = this.GetType().FullName;
                var methodName = nameof(SowDetailsViewModel);
                var exMessage = ex.InnerException == null ? string.Format($"ErrorMessage: {ex.Message}") : string.Format($"ErrorMessage: {ex.Message}, InnerException Message: {ex.InnerException.Message}");
                NavigationService.DisplayAlert(AppResources.ErrorOccured, new ViewModelMethodExecutionException(viewModelName, methodName, exMessage).Message, "Ok");
                throw ex;
            }

        }
               
        
#endregion C'tor

#region Public Interface

        public void InvalidateMock()
        {
            IsMock = Settings.UseMocks;
        }

        public async override Task InitializeAsync(object navigationData)
        {

            await Task.Run(() => {
#if USE_SETTINGS
                //--- Retrieve the Selected Sow from the Global Settings
                // Map Dto to ViewModel
                //TODO: Replace property mapping with AutoMapper
                _selectedSowDto = GlobalSetting.Instance.CurrentSelectedSowDto;
 
#else

                //--- Map the Selected Sow from the navigationData Parameter
                _selectedSowDto = navigationData as SowDto;


#endif
                SowId = _selectedSowDto.Id;
                Name.Value = _selectedSowDto.Name;
                Box.Value = _selectedSowDto.Box;

            });


        }

        #endregion Public Interface

        #region Protected Interface
        #endregion Protected Interface

        #region Private Interface


        #region Command Logic
        #endregion Command Logic

        #region Validation Logic

        private void AddValidations()
        {
            // sowname is required
            _name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.SowNameRequired });

            // location is required
            _box.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.LocationRequired });
        }

        private bool Validate()
        {
            bool isValidSowName = ValidateSowName();
            bool isValidLocation = ValidateLocation();

            return isValidSowName && isValidLocation;
        }

        private bool ValidateLocation()
        {
            return _box.Validate();
        }

        private bool ValidateSowName()
        {
            return _name.Validate();
        }

#endregion Validation Logic

#endregion Private Interface
    }
}
