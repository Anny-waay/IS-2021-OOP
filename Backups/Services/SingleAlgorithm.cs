using System.Collections.Generic;
using System.IO;
using Ionic.Zip;

namespace Backups.Services
{
    public class SingleAlgorithm : IRestorePointAlgorithm
    {
        public SingleAlgorithm()
        {
        }

        public List<string> CreateZip(List<FileInfo> files, string path, string restorePointNumber)
        {
            var zipRestorePoints = new List<string>();
            var zip = new ZipFile();
            string zipName = path + "/RestorePoint" + restorePointNumber + ".zip";
            foreach (FileInfo file in files)
            {
                zip.AddFile(file.FullName, "/");
            }

            zip.Save(zipName);
            zipRestorePoints.Add(zipName);
            return zipRestorePoints;
        }
    }
}
