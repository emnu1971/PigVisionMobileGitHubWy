using AnimalDtoModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigVisionMobile.Core.Services.PigVision.Sow
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 05-09-2017
    /// Purtpose    : PigVision Sow Service
    /// </summary>
    public partial interface ISowService
    {
        // get all active sows for the current selected company of the current active login
        Task<ObservableCollection<SowDto>> GetSowsAsync(int pCompanyId);
    }
}
