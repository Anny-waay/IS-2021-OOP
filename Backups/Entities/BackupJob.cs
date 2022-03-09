using System.Collections.Generic;
using System.IO;
using Backups.Services;
using Backups.Tools;

namespace Backups.Entities
{
    public class BackupJob : IBackupJob
    {
        public BackupJob()
        {
        }

        public BackupJob(string name, string path, IRestorePointAlgorithm algorithm)
        {
            Algorithm = algorithm;
            Files = new List<FileInfo>();
            RestorePoints = new List<RestorePoint>();
            Repository = new Repository();
            Path = Repository.CreateBackupJobDirectory(name, path);
        }

        public string Path { get; set; }
        public IRestorePointAlgorithm Algorithm { get; set; }
        public List<FileInfo> Files { get; set; }
        public List<RestorePoint> RestorePoints { get; set; }
        public Repository Repository { get; set; }

        public FileInfo AddFile(string path)
        {
            var newFile = new FileInfo(path);
            Files.Add(newFile);
            return newFile;
        }

        public void DeleteFile(FileInfo file)
        {
            Files.Remove(file);
        }

        public void CreateRestorePoint()
        {
            string restorePointPath = Repository.CreateRestorePointDirectory(Path, (RestorePoints.Count + 1).ToString());
            var newRestorePoint = new RestorePoint(Algorithm.CreateZip(Files, restorePointPath, (RestorePoints.Count + 1).ToString()));
            RestorePoints.Add(newRestorePoint);
        }

        public List<RestorePoint> GetRestorePoints() => RestorePoints;
    }
}
