using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalDtoModel
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 05-09-2017
    /// Purpose     : Sow DataTransferObject
    /// 
    /// </summary>
    public partial class SowDto
    {
        #region Public Interface

        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public string Box { get; set; }

        #endregion Properties

        #endregion Public Interface
    }
}
