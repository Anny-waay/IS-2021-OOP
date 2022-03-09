using System.Collections.Generic;
using System.IO;
using Ionic.Zip;

namespace Backups.Services
{
    public class SplitAlgorithm : IRestorePointAlgorithm
    {
        public SplitAlgorithm()
        {
        }

        public List<string> CreateZip(List<FileInfo> files, string path, string restorePointNumber)
        {
            var zipRestorePoints = new List<string>();
            foreach (FileInfo file in files)
            {
                string zipName = path + "/" + file.Name + "_" + restorePointNumber + ".zip";
                var zip = new ZipFile();
                zip.AddFile(file.FullName, "/");
                zip.Save(zipName);
                zipRestorePoints.Add(zipName);
            }

            return zipRestorePoints;
        }
    }
}
