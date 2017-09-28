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
    /// Date        : 04-09-2017
    /// Purpose     : PigVision Sow Service (Mock)
    /// </summary>
    public partial class SowMockService : ISowService
    {
        #region ISowService Implementation

        public async Task<ObservableCollection<SowDto>> GetSowsAsync(int pCompanyId)
        {
            try
            {
                // simulate webservice connection delay ...
                await Task.Delay(500);

                var sows = MockSows.ToList<SowDto>();

                ObservableCollection<SowDto> ocSows = new ObservableCollection<SowDto>();
                foreach (SowDto sow in sows)
                {
                    // add company-id to differentiate for company
                    sow.Name = $"{sow.Name}.{pCompanyId}";
                    ocSows.Add(sow);
                }

                return ocSows;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion ISowService Implementation

        #region Private Interface

        private ObservableCollection<SowDto> MockSows = new ObservableCollection<SowDto>()
        {
            new SowDto()
            {
                Id = 1,
                Name = "Sow 1",
                Box = "B1.A1.H16"
            },
            new SowDto()
            {
                Id = 2,
                Name = "Sow 2",
                Box = "B2.A3.H17"
            },
            new SowDto()
            {
                Id = 3,
                Name = "Sow 3",
                Box = "C1.A1.H16"
            },
            new SowDto()
            {
                Id = 4,
                Name = "Sow 4",
                Box = "B2.A1.H16"
            },
            new SowDto()
            {
                Id = 5,
                Name = "Sow 5",
                Box = "B3.A2.H11"
            }
        };

        #endregion Private Interface
    }
}
