using System.IO;
using System.IO.Compression;

namespace BackupsExtra.Entities
{
    public class RepositoryExtra
    {
        public RepositoryExtra()
        {
        }

        public void DeleteRestorePointDirectory(string backupJobPath, int restorePointNumber)
        {
            string directorypath = backupJobPath + "/RestorePoint" + restorePointNumber.ToString();
            Directory.Delete(directorypath, true);
        }

        public string MoveFileToDirectory(string filePath, string directoryPath, string fileName, string restorePointNumber)
        {
            string newFileName = directoryPath + "/" + fileName + "_" + restorePointNumber + ".zip";
            File.Move(filePath, newFileName);
            return newFileName;
        }

        public void Recovery(string zipFile, string directory, string fullName)
        {
            if (File.Exists(fullName))
            {
                File.Delete(fullName);
            }

            ZipFile.ExtractToDirectory(zipFile, directory);
        }
    }
}