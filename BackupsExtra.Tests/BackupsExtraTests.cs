using System;
using System.IO;
using Backups.Services;
using Backups.Entities;
using BackupsExtra.Entities;
using BackupsExtra.Services;
using BackupsExtra.Tools;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class Tests
    {
        private IBackupJobExtra _backupJob;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DeleteByNumberTest_TwoRestorePoints()
        {
            var builder = new BackupJobExtra.BackupJobExtraBuilder();
            builder.SetName("TestBackup1").SetPath("./").SetAlgorithm(new SplitAlgorithm()).SetLogger(new ConsoleLogger(false));
            _backupJob = builder.SetRestorePointNumberLimit(2).SetRestorePointDateLimit(DateTime.Today).GetBackupJobExtra();
            FileInfo fileA = _backupJob.AddFile("./../../../Files/FileA");
            FileInfo fileB = _backupJob.AddFile("./../../../Files/FileB");
            FileInfo fileC = _backupJob.AddFile("./../../../Files/FileC");
            _backupJob.CreateRestorePoint(new DateTime(2021, 12, 3));
            _backupJob.CreateRestorePoint(new DateTime(2021, 12, 3));
            _backupJob.DeleteFile(fileC);
            _backupJob.CreateRestorePoint(new DateTime(2021, 12, 3));
            _backupJob.DeleteRestorePoints(new DeleteByNumberAlgorithm());
            Assert.AreEqual(_backupJob.GetRestorePoints().Count, 2);
            Directory.Delete("./TestBackup1", true);
        }
        
        [Test]
        public void DeleteByDateTest_ThrowException()
        {
            var builder = new BackupJobExtra.BackupJobExtraBuilder();
            builder.SetName("TestBackup2").SetPath("./").SetAlgorithm(new SplitAlgorithm()).SetLogger(new ConsoleLogger(false));
            _backupJob = builder.SetRestorePointNumberLimit(2).SetRestorePointDateLimit(DateTime.Today).GetBackupJobExtra();
            FileInfo fileA = _backupJob.AddFile("./../../../Files/FileA");
            FileInfo fileB = _backupJob.AddFile("./../../../Files/FileB");
            _backupJob.CreateRestorePoint(new DateTime(2021, 12, 3));
            _backupJob.DeleteFile(fileB);
            _backupJob.CreateRestorePoint(new DateTime(2021, 12, 3));
            Assert.Catch<BackupsExtraException>(() =>
            {
                _backupJob.DeleteRestorePoints(new DeleteByDateAlgorithm());
            });
            Directory.Delete("./TestBackup2", true);
        }
        
        [Test]
        public void Merge_OneRestorePoint()
        {
            var builder = new BackupJobExtra.BackupJobExtraBuilder();
            builder.SetName("TestBackup3").SetPath("./").SetAlgorithm(new SplitAlgorithm()).SetLogger(new ConsoleLogger(false));
            _backupJob = builder.SetRestorePointNumberLimit(2).SetRestorePointDateLimit(DateTime.Today).GetBackupJobExtra();
            FileInfo fileA = _backupJob.AddFile("./../../../Files/FileA");
            FileInfo fileB = _backupJob.AddFile("./../../../Files/FileB");
            FileInfo fileC = _backupJob.AddFile("./../../../Files/FileC");
            _backupJob.CreateRestorePoint(new DateTime(2021, 12, 3));
            _backupJob.CreateRestorePoint(new DateTime(2021, 12, 3));
            _backupJob.DeleteFile(fileC);
            _backupJob.CreateRestorePoint(new DateTime(2021, 12, 3));
            _backupJob.Merge();
            Assert.AreEqual(_backupJob.GetRestorePoints().Count, 1);
            Directory.Delete("./TestBackup3", true);
        }
    }
}