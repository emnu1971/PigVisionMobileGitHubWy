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
    /// Date        : 04-09-2017
    /// Purpose     : Custom Exception thrown when any error occures in a ViewModelMethod
    /// </summary>
    public class ViewModelMethodExecutionException : Exception
    {
        #region C'tor

        public ViewModelMethodExecutionException()
        {

        }

        public ViewModelMethodExecutionException(string pViewModelName, string pMethodName, string pErrorMessage)
            : base($"{AppResources.MethodCallingError}\r\nViewModel: {pViewModelName}\r\nMethod: {pMethodName}\r\n{pErrorMessage}")
        {

        }

        #endregion C'tor
    }
}
