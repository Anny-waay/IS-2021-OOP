using System.IO;

namespace Backups.Entities
{
    public class Repository
    {
        public Repository()
        {
        }

        public string CreateBackupJobDirectory(string name, string path)
        {
            string directoryPath = path + "/" + name;
            Directory.CreateDirectory(directoryPath);
            return directoryPath;
        }

        public string CreateRestorePointDirectory(string path, string restorePointNumber)
        {
            string directoryPath = path + "/RestorePoint" + restorePointNumber;
            Directory.CreateDirectory(directoryPath);
            return directoryPath;
        }
    }
}
