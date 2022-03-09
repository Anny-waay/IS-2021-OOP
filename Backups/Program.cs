using System;
using System.IO;
using Backups.Entities;
using Backups.Services;
using Ionic.Zip;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            IRestorePointAlgorithm algorithm = new SingleAlgorithm();
            IBackupJob backupJob = new BackupJob("BackupJob1", "/Users/annakomova/Desktop", algorithm);
            FileInfo fileA = backupJob.AddFile("/Users/annakomova/Desktop/FileA");
            FileInfo fileB = backupJob.AddFile("/Users/annakomova/Desktop/FileB");
            FileInfo fileC = backupJob.AddFile("/Users/annakomova/Desktop/FileC");
            backupJob.CreateRestorePoint();
            backupJob.CreateRestorePoint();
            backupJob.DeleteFile(fileC);
            backupJob.CreateRestorePoint();
        }
    }
}
