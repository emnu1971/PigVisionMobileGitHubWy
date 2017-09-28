using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyDtoModel;
using PigVisionMobile.Core.Services.RequestProvider;
using PigVisionMobile.Core.Helpers;
using PigVisionMobile.Core.Extensions;

namespace PigVisionMobile.Core.Services.PigVision.Company
{

    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 14-09-2017
    /// Purpose     : Company Service
    /// </summary>
    public partial class CompanyService : ICompanyService
    {

        #region Private Storage

        private readonly IRequestProvider _requestProvider;

        #endregion Private Storage

        #region C'tor

        public CompanyService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        #endregion C'tor

        #region ICompanyService Implementation

        public async Task<ObservableCollection<CompanyDto>> GetCompaniesAsync(string pLogin)
        {
            try
            {
                UriBuilder builder = new UriBuilder(GlobalSetting.Instance.CompanyEndpoint);

                builder.Path = "api/companies";

                string uri = builder.ToString();

                List<CompanyDto> companies = await _requestProvider.GetAsync<List<CompanyDto>>(uri);

                var result = new ObservableCollection<CompanyDto>();

                if (companies != null)
                {

                    //foreach (CompanyDto company in companies)
                    //    result.Add(company);
                    return companies.ToObservableCollection<CompanyDto>();

                }
                
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion ICompanyService Implementation
    }
}
