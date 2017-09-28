using AnimalDtoModel;

namespace PigVisionMobile.Core
{
    public class GlobalSetting
    {
        public const string AzureTag = "Azure";
        public const string MockTag = "Mock";
        public const string DefaultEndpoint = "http://localhost";

        private string _baseEndpoint;
        private static readonly GlobalSetting _instance = new GlobalSetting();

        public GlobalSetting()
        {
            AuthToken = "INSERT AUTHENTICATION TOKEN";
            BaseEndpoint = DefaultEndpoint;
        }

        public static GlobalSetting Instance
        {
            get { return _instance; }
        }

        public string BaseEndpoint
        {
            get { return _baseEndpoint; }
            set
            {
                _baseEndpoint = value;
                UpdateEndpoint(_baseEndpoint);
            }
        }

        public string ClientId { get { return "xamarin"; }}

        public string ClientSecret { get { return "secret"; }}

        public string AuthToken { get; set; }

        public string RegisterWebsite { get; set; }

        public string CatalogEndpoint { get; set; }

        public string OrdersEndpoint { get; set; }

        public string BasketEndpoint { get; set; }

        public string IdentityEndpoint { get; set; }

        public string LocationEndpoint { get; set; }

        public string MarketingEndpoint { get; set; }

        public string UserInfoEndpoint { get; set; }

        public string TokenEndpoint { get; set; }

        public string LogoutEndpoint { get; set; }

        public string IdentityCallback { get; set; }

        public string LogoutCallback { get; set; }

        private void UpdateEndpoint(string baseEndpoint)
        {
            RegisterWebsite = $"{baseEndpoint}:5105/Account/Register";
            CatalogEndpoint = $"{baseEndpoint}:5101";
            OrdersEndpoint = $"{baseEndpoint}:5102";
            BasketEndpoint = $"{baseEndpoint}:5103";
            IdentityEndpoint = $"{baseEndpoint}:5105/connect/authorize";
            UserInfoEndpoint = $"{baseEndpoint}:5105/connect/userinfo";
            TokenEndpoint = $"{baseEndpoint}:5105/connect/token";
            LogoutEndpoint = $"{baseEndpoint}:5105/connect/endsession";
            IdentityCallback = $"{baseEndpoint}:5105/xamarincallback";
            LogoutCallback = $"{baseEndpoint}:5105/Account/Redirecting";
            LocationEndpoint = $"{baseEndpoint}:5109";
            MarketingEndpoint = $"{baseEndpoint}:5110";

            #region PigVision

            CompanyEndpoint = $"{baseEndpoint}:47544";

            #endregion PigVision
        }

        // name of the logged in user
        public string CurrentUserName { get; set; }

        // id of the current selected company
        public int CurrentCompanyId { get; set; }

        // reference to current selected sow
        public SowDto CurrentSelectedSowDto { get; set; }

        #region PigVision

        public string CompanyEndpoint { get; set; }

        #endregion PigVision
    }
}