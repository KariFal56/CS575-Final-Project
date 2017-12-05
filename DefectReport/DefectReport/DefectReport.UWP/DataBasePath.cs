using Windows.Storage;
using DefectReport.UWP;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(DataBasePath))]
namespace DefectReport.UWP
{
    class DataBasePath : IDatabasePath
    {
        public string GetDatabasePath(string fileName)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, fileName);
        }
    }
}

