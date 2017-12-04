using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DefectReport
{
    public partial class App : Application
    {
        static DataAccess database;

        public App()
        {
            Resources = new ResourceDictionary();
            Resources.Add("primaryGreen", Color.FromHex("91CA47"));
            Resources.Add("primaryDarkGreen", Color.FromHex("6FA22E"));

            var nav = new NavigationPage(new MainPage());
            nav.BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"];
            nav.BarTextColor = Color.White;
        }

        public static DataAccess Database
        {
            get
            {
                if (database == null)
                {
                    try
                    {
                        database = new DataAccess(DependencyService.Get<IDatabasePath>().GetDatabasePath("DefectReport.db3"));

                    }
                    catch (System.Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }

                }
                return database;
            }
        }

        public int ResumeAtDefectReportId { get; set; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
