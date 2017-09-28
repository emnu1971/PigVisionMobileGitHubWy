using PigVisionMobile.Core.Localization.Resx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigVisionMobile.Core.Exceptions
{
    /// <summary>
    /// Author:     Emmanuel Nuyttens
    /// Date:       30 August 2017 (on my birthday ;-) )
    /// Purpose:    Custom Exception thrown when any error occurs in a ViewModelCommand.
    /// </summary>
    public class ViewModelCommandExecutionException : Exception
    {

        #region C'tor

        public ViewModelCommandExecutionException()
        {

        }
               

        public ViewModelCommandExecutionException(string pViewModelName, string pCommandName, string pErrorMessage)
            : base($"{AppResources.MethodCallingError}\r\nViewModel: {pViewModelName}\r\nCommand: {pCommandName}\r\n{pErrorMessage}")
        {

        }

        #endregion C'tor
    }
}
