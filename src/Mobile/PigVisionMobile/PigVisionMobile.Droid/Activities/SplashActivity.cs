using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;

namespace PigVisionMobile.Droid.Activities
{
    [Activity(
         Label = "PigVisionMobile",
         Icon = "@drawable/icon",
         Theme = "@style/Theme.Splash",
         NoHistory = true,
         ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            InvokeMainActivity();
        }

        private void InvokeMainActivity()
        {
            var mainActivityIntent = new Intent(this, typeof(MainActivity));
            StartActivity(mainActivityIntent);
        }
    }
}