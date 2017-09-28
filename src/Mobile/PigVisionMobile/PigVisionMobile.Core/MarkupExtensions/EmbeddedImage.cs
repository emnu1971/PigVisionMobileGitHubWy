using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PigVisionMobile.Core.MarkupExtensions
{
    /// <summary>
    /// Author  : Emmanuel Nuyttens
    /// Date    : June 2017
    /// Purpose : This class is used to make it possible to bind to resource images from xaml
    /// </summary>
    [ContentProperty("ResourceId")]
    public class EmbeddedImage : IMarkupExtension
    {
        public string ResourceId { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (String.IsNullOrWhiteSpace(ResourceId))
                return null;

            return ImageSource.FromResource(ResourceId);
        }
    }
}
