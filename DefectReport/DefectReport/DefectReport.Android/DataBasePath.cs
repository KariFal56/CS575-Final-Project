using System;
using System.IO;
using DefectReport.Droid;
using Xamarin.Forms;


[assembly: Dependency(typeof(DatabasePath))]
namespace DefectReport.Droid
{
    class DatabasePath : IDatabasePath
    {
        public string GetDatabasePath(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, fileName);
        }

    }
}