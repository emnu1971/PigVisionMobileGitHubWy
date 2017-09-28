using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalDtoModel;

namespace PigVisionMobile.Core.Services.PigVision.Sow
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 14-09-2017
    /// Purpose     : Sow Service
    /// </summary>
    public partial class SowService : ISowService
    {
        public Task<ObservableCollection<SowDto>> GetSowsAsync(int pCompanyId)
        {
            throw new NotImplementedException();
        }
    }
}
