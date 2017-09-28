using CompanyDtoModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigVisionMobile.Core.Services.PigVision.Company
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 04-09-2017
    /// Purpose     : PigVision Company Service
    /// </summary>
    public partial interface ICompanyService
    {
        // get all companies for the current login
        Task<ObservableCollection<CompanyDto>> GetCompaniesAsync(string pLogin);
    }
}
