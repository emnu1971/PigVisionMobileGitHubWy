using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PigVisionMobile.Core.Views.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowLogoView : ContentPage
    {
        public ShowLogoView()
        {
            InitializeComponent();
            _image.Source = ImageSource.FromResource("PigVisionMobile.Core.Images.GreenPigLogo.PNG");
        }
    }
}