using System;
using System.Collections.Generic;
using System.IO;
using Backups.Entities;
using Backups.Services;
using BackupsExtra.Tools;

namespace BackupsExtra.Services
{
    public interface IBackupJobExtra
    {
        FileInfo AddFile(string path);
        void DeleteFile(FileInfo file);
        RestorePoint CreateRestorePoint();
        RestorePoint CreateRestorePoint(DateTime date);
        void DeleteRestorePoints(IDeleteAlgorithm deleteAlgorithm);
        void Merge();
        void Recovery(RestorePoint restorePoint);
        void Recovery(RestorePoint restorePoint, string directoryPath);
        List<RestorePoint> GetRestorePoints();
    }
}