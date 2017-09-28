using AnimalDtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyDtoModel
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 04-09-2017
    /// Purpose     : Company DataTransferObject
    /// </summary>
    public partial class CompanyDto
    {
        #region Public Interface

        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }

        #endregion Properties

        #region Associations

        public ICollection<SowDto> Sows { get; set; } = new List<SowDto>();

        #endregion Associations

        #region Calculated Fields

        public int NumberOfSows
        {
            get
            {
                return Sows.Count();
            }
        }

        #endregion Calculated Fields

        #endregion Public Interface

    }
}
