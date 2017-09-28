using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyDtoModel;

namespace PigVisionMobile.Core.Services.PigVision.Company
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 04-09-2017
    /// Purpose     : PigVision Company Service (Mock)
    /// </summary>
    public partial class CompanyMockService : ICompanyService
    {
        #region ICompanyService Implementation

        public async Task<ObservableCollection<CompanyDto>> GetCompaniesAsync(string pLogin)
        {
            try
            {
                // simulate webservice connection delay ...
                await Task.Delay(500);

                return MockCompanies;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion ICompanyService Implementation

        #region Private Interface

        private ObservableCollection<CompanyDto> MockCompanies = new ObservableCollection<CompanyDto>()
        {
            new CompanyDto()
            {
                Id = 1,
                Name = "Stal de Heidoorn"
            },
            new CompanyDto()
            {
                Id = 2,
                Name = "De Venter Varkensstal"
            },
            new CompanyDto()
            {
                Id = 3,
                Name = "Familiebedrijf Oudensterp"
            }

        };

        #endregion Private Interface
    }
}
