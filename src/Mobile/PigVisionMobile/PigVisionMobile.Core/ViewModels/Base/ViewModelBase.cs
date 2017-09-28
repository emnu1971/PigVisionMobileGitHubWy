using PigVisionMobile.Core.Helpers;
using PigVisionMobile.Services;
using System.Threading.Tasks;

namespace PigVisionMobile.Core.ViewModels.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {

        #region Private Storage

        private bool _isMock;
        private bool _isValid;
        private bool _isBusy;

        #endregion Private Storage

        #region Public Properties

        #region Bindable Properties

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public bool IsMock
        {
            get
            {
                return _isMock;
            }
            set
            {
                _isMock = value;
                RaisePropertyChanged(() => IsMock);
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        #endregion Bindable Properties

        #region Commands
        #endregion Commands

        #endregion Public Properties

        #region C'tor

        public ViewModelBase()
        {
            DialogService = ViewModelLocator.Resolve<IDialogService>();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
            GlobalSetting.Instance.BaseEndpoint = Settings.UrlBase;
        }

        #endregion C'tor

        #region Public Interface

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        #endregion Public Interface

        #region Protected Interface

        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;
        
        #endregion Protected Interface

        #region Private Interface

        #endregion Private Interface
               

        
    }
}