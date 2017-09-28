using PigVisionMobile.Core.Localization.Resx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigVisionMobile.Core.Exceptions
{

    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date:       : 04-09-2017
    /// Purpose     : Customp Exception thrown when any error occures in a ViewMethod
    /// </summary>
    public class ViewMethodExecutionException : Exception
    {

        #region C'tor

        public ViewMethodExecutionException()
        {

        }

        public ViewMethodExecutionException(string pViewName, string pMethodName, string pErrorMessage)
            : base($"{AppResources.MethodCallingError}\r\nView: {pViewName}\r\nMethod: {pMethodName}\r\n{pErrorMessage}")
        {

        }

        #endregion C'tor
    }
}
