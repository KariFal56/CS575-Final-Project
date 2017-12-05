﻿using System;
using DefectReport.iOS;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataBasePath))]
namespace DefectReport.iOS
{
    public class DataBasePath : IDatabasePath
    {
        public string GetDatabasePath(string fileName)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, fileName);
        }
    }
}
