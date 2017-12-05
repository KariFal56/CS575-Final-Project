using Android.App;
using Android.Content.PM;
using Android.OS;

namespace DefectReport.Droid
{
    [Activity(Label = "DefectReport", Icon = "@drawable/DefectReport", 
        Theme = "@style/MainTheme", MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class MainActivity : 
    global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    //public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

