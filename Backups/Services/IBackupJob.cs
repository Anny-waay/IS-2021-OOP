using System.Collections.Generic;
using System.IO;
using Backups.Entities;

namespace Backups.Services
{
    public interface IBackupJob
    {
        FileInfo AddFile(string path);
        void DeleteFile(FileInfo file);
        void CreateRestorePoint();
        List<RestorePoint> GetRestorePoints();
    }
}