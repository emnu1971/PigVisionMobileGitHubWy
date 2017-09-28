using PigVisionMobile.Core.Exceptions;
using PigVisionMobile.Core.Helpers;
using PigVisionMobile.Core.Localization.Resx;
using PigVisionMobile.Core.Validations;
using PigVisionMobile.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PigVisionMobile.Core.ViewModels.PigVision
{
    /// <summary>
    /// Author  :   Emmanuel Nuyttens
    /// Date    :   29-08-2017
    /// Purpose :   PigVision Login View Model
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        #region Private Storage

        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _password;

        #endregion Private Storage

        #region Public Properties

        #region Bindable Properties

        public ValidatableObject<string> UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                // update current user name in global settings
                RaisePropertyChanged(() => UserName);
            }
        }

        public ValidatableObject<string> Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }


        

        #endregion Bindable Properties

        #region Commands

        public ICommand MockSignInCommand => new Command(async () => await MockSignInAsync());
        public ICommand ValidateUserNameCommand => new Command(() => ValidateUserName());
        public ICommand ValidatePasswordCommand => new Command(() => ValidatePassword());

        #endregion Commands

        #endregion Public Properties

        #region C'tor

        public LoginViewModel()
        {
            // activate for validation
            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            // set mock
            InvalidateMock();

            // validate
            AddValidations();

        }


        #endregion C'tor


        #region Public Interface

        public void InvalidateMock()
        {
            IsMock = Settings.UseMocks;
        }

        #endregion Public Interface
        
        #region Private Interface


        #region Command Logic

        private async Task MockSignInAsync()
        {
            IsBusy = true;
            IsValid = true;

            try
            {
                
                bool isValid = Validate();
                bool isAuthenticated = false;

                // set the current username in globalsetting
                GlobalSetting.Instance.CurrentUserName = UserName.Value;

                if (isValid)
                {
                    try
                    {
                        await Task.Delay(1000);

                        isAuthenticated = true;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"[SignIn] Error signing in: {ex}");
                    }
                }
                else
                {
                    IsValid = false;
                }

                if (isAuthenticated)
                {
                    Settings.AuthAccessToken = GlobalSetting.Instance.AuthToken;


                    await NavigationService.NavigateToAsync<MainViewModel>();
                    await NavigationService.RemoveLastFromBackStackAsync();
                }

            }
            catch (Exception ex)
            {
                var viewModelName = this.GetType().FullName;
                var commandName = nameof(MockSignInAsync);
                var exMessage = ex.InnerException == null ? string.Format($"ErrorMessage: {ex.Message}") : string.Format($"ErrorMessage: {ex.Message}, InnerException Message: {ex.InnerException.Message}");

                await NavigationService.DisplayAlert(AppResources.ErrorOccured, new ViewModelCommandExecutionException(viewModelName, commandName, exMessage).Message, "Ok");

            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion Command Logic

        #region Validation Logic

        private void AddValidations()
        {
            // username is required
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.UserNameRequired });

            // password is required
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.PassWordRequired });

        }


        private bool Validate()
        {
            bool isValidUser = ValidateUserName();
            bool isValidPassword = ValidatePassword();

            return isValidUser && isValidPassword;
        }

        private bool ValidateUserName()
        {
            return _userName.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }

        #endregion Validation Logic


        #endregion Private Interface
    }
}
