using PigVisionMobile.Core.Views.Base;
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
    public partial class ResourceDictionaryView : ContentPage
    {
        public ResourceDictionaryView()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            // Change the value of one of the resources
            // This will not work at first
            // we have to use a different markup extension to make this work
            // we should replace StaticResource in the XAML with DynamicResource
            Resources[ViewResourceKeys.buttonBackGroundColor] = Color.AliceBlue;

        }
    }
}