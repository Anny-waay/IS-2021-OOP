using System.IO;
using Backups.Services;
using Backups.Entities;
using NUnit.Framework;

namespace Backups.Tests
{
    public class Tests
    {
        private IBackupJob _backupJob;

        [SetUp]
        public void Setup()
        {
            IRestorePointAlgorithm algorithm = new SplitAlgorithm();
            _backupJob = new BackupJob("TestBackup", "./", algorithm);
        }

        [Test]
        public void SplitTest()
        {
            FileInfo fileA = _backupJob.AddFile("./../../../Files/FileA");
            FileInfo fileB = _backupJob.AddFile("./../../../Files/FileB");
            _backupJob.CreateRestorePoint();
            _backupJob.DeleteFile(fileB);
            _backupJob.CreateRestorePoint();
            Assert.AreEqual(_backupJob.GetRestorePoints().Count, 2);
            Assert.AreEqual(_backupJob.GetRestorePoints()[0].ZipPaths.Count, 2);
            Assert.AreEqual(_backupJob.GetRestorePoints()[1].ZipPaths.Count, 1);
            Directory.Delete("./TestBackup", true);
        }
    }
}