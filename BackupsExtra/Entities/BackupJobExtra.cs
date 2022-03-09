using System;
using System.Collections.Generic;
using System.IO;
using Backups.Entities;
using Backups.Services;
using BackupsExtra.Services;
using BackupsExtra.Tools;

namespace BackupsExtra.Entities
{
    public class BackupJobExtra : IBackupJobExtra
    {
        private BackupJobExtra(string name, string path, IRestorePointAlgorithm algorithm, int restorePointNumberLimit, DateTime restorePointDateLimit, ILogger logger)
        {
            CurrentDataStorage = new DataStorage();
            CurrentBackupJob = new BackupJob(name, path, algorithm);
            RestorePointsCounter = 0;
            var tempBackupJob = CurrentDataStorage.Unload(CurrentBackupJob.Path);
            if (tempBackupJob != null)
            {
                CurrentBackupJob = tempBackupJob;
                RestorePointsCounter = CurrentBackupJob.RestorePoints.Count;
            }

            RestorePointDateLimit = restorePointDateLimit;
            RestorePointNumberLimit = restorePointNumberLimit;
            Logger = logger;
            Repository = new RepositoryExtra();
            CurrentDataStorage.Save(CurrentBackupJob);
            Logger.DisplayMessage($"BackupJob with name {name} was created", DateTime.Now);
        }

        public BackupJob CurrentBackupJob { get; }
        public int RestorePointsCounter { get; private set; }
        public int RestorePointNumberLimit { get; }
        public DateTime RestorePointDateLimit { get; }
        public ILogger Logger { get; }
        public RepositoryExtra Repository { get; }
        public DataStorage CurrentDataStorage { get; }

        public FileInfo AddFile(string path)
        {
            Logger.DisplayMessage($"Add file {path} to {CurrentBackupJob.Path}", DateTime.Now);
            CurrentDataStorage.Save(CurrentBackupJob);
            return CurrentBackupJob.AddFile(path);
        }

        public void DeleteFile(FileInfo file)
        {
            Logger.DisplayMessage($"Delete file {file.FullName} from {CurrentBackupJob.Path}", DateTime.Now);
            CurrentDataStorage.Save(CurrentBackupJob);
            CurrentBackupJob.DeleteFile(file);
        }

        public RestorePoint CreateRestorePoint()
        {
            string restorePointPath = CurrentBackupJob.Repository.CreateRestorePointDirectory(CurrentBackupJob.Path, (RestorePointsCounter + 1).ToString());
            var newRestorePoint = new RestorePoint(CurrentBackupJob.Algorithm.CreateZip(CurrentBackupJob.Files, restorePointPath, (RestorePointsCounter + 1).ToString()));
            foreach (FileInfo file in CurrentBackupJob.Files)
            {
                newRestorePoint.JobFiles.Add(file);
            }

            CurrentBackupJob.RestorePoints.Add(newRestorePoint);
            RestorePointsCounter++;
            Logger.DisplayMessage($"Restore Point {RestorePointsCounter} was created in {CurrentBackupJob.Path}", DateTime.Now);
            CurrentDataStorage.Save(CurrentBackupJob);
            return newRestorePoint;
        }

        public RestorePoint CreateRestorePoint(DateTime date)
        {
            string restorePointPath = CurrentBackupJob.Repository.CreateRestorePointDirectory(CurrentBackupJob.Path, (RestorePointsCounter + 1).ToString());
            var newRestorePoint = new RestorePoint(CurrentBackupJob.Algorithm.CreateZip(CurrentBackupJob.Files, restorePointPath, (RestorePointsCounter + 1).ToString()), date);
            foreach (FileInfo file in CurrentBackupJob.Files)
            {
                newRestorePoint.JobFiles.Add(file);
            }

            CurrentBackupJob.RestorePoints.Add(newRestorePoint);
            RestorePointsCounter++;
            Logger.DisplayMessage($"Restore Point {RestorePointsCounter} was created in {CurrentBackupJob.Path}", date);
            CurrentDataStorage.Save(CurrentBackupJob);
            return newRestorePoint;
        }

        public void DeleteRestorePoints(IDeleteAlgorithm deleteAlgorithm)
        {
            int restorePointsNumber = deleteAlgorithm.FindPointsToDelete(CurrentBackupJob.RestorePoints, RestorePointNumberLimit, RestorePointDateLimit);
            if (restorePointsNumber == CurrentBackupJob.RestorePoints.Count)
            {
                throw new BackupsExtraException("For this limit all points will be deleted!");
            }
            else
            {
                for (int i = 0; i < restorePointsNumber; i++)
                {
                    int restorePointNumber = RestorePointsCounter - CurrentBackupJob.RestorePoints.Count + 1 + i;
                    Repository.DeleteRestorePointDirectory(CurrentBackupJob.Path, restorePointNumber);
                }

                CurrentBackupJob.RestorePoints.RemoveRange(0, restorePointsNumber);
            }

            Logger.DisplayMessage($"{restorePointsNumber} restore points was deleted from {CurrentBackupJob.Path}", DateTime.Now);
            CurrentDataStorage.Save(CurrentBackupJob);
        }

        public void Merge()
        {
            if (CurrentBackupJob.Algorithm is SplitAlgorithm)
            {
                var zipPaths = new List<string>();
                var jobFiles = new List<FileInfo>();
                for (int i = CurrentBackupJob.RestorePoints.Count - 1; i >= 0; i--)
                {
                    for (int j = 0; j < CurrentBackupJob.RestorePoints[i].JobFiles.Count; j++)
                    {
                        bool found = false;
                        foreach (FileInfo addedJobFile in jobFiles)
                        {
                            if (CurrentBackupJob.RestorePoints[i].JobFiles[j] == addedJobFile)
                            {
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            jobFiles.Add(CurrentBackupJob.RestorePoints[i].JobFiles[j]);
                            zipPaths.Add(CurrentBackupJob.RestorePoints[i].ZipPaths[j]);
                        }
                    }
                }

                string tempRestorePointPath = CurrentBackupJob.Repository.CreateRestorePointDirectory(CurrentBackupJob.Path, "0");
                var tempZipPaths = new List<string>();
                for (int i = 0; i < zipPaths.Count; i++)
                {
                    tempZipPaths.Add(Repository.MoveFileToDirectory(zipPaths[i], tempRestorePointPath, jobFiles[i].Name, "0"));
                }

                for (int i = 0; i < CurrentBackupJob.RestorePoints.Count; i++)
                {
                    int restorePointNumber = RestorePointsCounter - CurrentBackupJob.RestorePoints.Count + 1 + i;
                    Repository.DeleteRestorePointDirectory(CurrentBackupJob.Path, restorePointNumber);
                }

                string restorePointPath = CurrentBackupJob.Repository.CreateRestorePointDirectory(CurrentBackupJob.Path, "1");
                zipPaths.Clear();
                for (int i = 0; i < tempZipPaths.Count; i++)
                {
                    zipPaths.Add(Repository.MoveFileToDirectory(tempZipPaths[i], restorePointPath, jobFiles[i].Name, "1"));
                }

                Repository.DeleteRestorePointDirectory(CurrentBackupJob.Path, 0);

                var restorePoint = new RestorePoint(zipPaths);
                restorePoint.JobFiles = jobFiles;
                CurrentBackupJob.RestorePoints.RemoveRange(0, CurrentBackupJob.RestorePoints.Count);
                CurrentBackupJob.RestorePoints.Add(restorePoint);
                RestorePointsCounter = 1;
            }
            else
            {
                for (int i = 0; i < CurrentBackupJob.RestorePoints.Count - 1; i++)
                {
                    int restorePointNumber = RestorePointsCounter - CurrentBackupJob.RestorePoints.Count + 1 + i;
                    Repository.DeleteRestorePointDirectory(CurrentBackupJob.Path, restorePointNumber);
                }

                CurrentBackupJob.RestorePoints.RemoveRange(0, CurrentBackupJob.RestorePoints.Count - 1);
            }

            Logger.DisplayMessage("All restore points were merged to one", DateTime.Now);
            CurrentDataStorage.Save(CurrentBackupJob);
        }

        public void Recovery(RestorePoint restorePoint)
        {
            for (int i = 0; i < restorePoint.ZipPaths.Count; i++)
            {
                Repository.Recovery(restorePoint.ZipPaths[i], restorePoint.JobFiles[i].DirectoryName, restorePoint.JobFiles[i].FullName);
                Logger.DisplayMessage($"File {restorePoint.JobFiles[i].Name} was recovered to original directory", DateTime.Now);
            }
        }

        public void Recovery(RestorePoint restorePoint, string directoryPath)
        {
            for (int i = 0; i < restorePoint.ZipPaths.Count; i++)
            {
                Repository.Recovery(restorePoint.ZipPaths[i], directoryPath, restorePoint.JobFiles[i].FullName);
                Logger.DisplayMessage($"File {restorePoint.JobFiles[i].Name} was recovered to different directory", DateTime.Now);
            }
        }

        public List<RestorePoint> GetRestorePoints() => CurrentBackupJob.RestorePoints;

        public class BackupJobExtraBuilder
        {
            private string _name;
            private string _path;
            private IRestorePointAlgorithm _algorithm;
            private int _restorePointNumberLimit;
            private DateTime _restorePointDateLimit;
            private ILogger _logger;

            public BackupJobExtraBuilder SetName(string name)
            {
                _name = name;
                return this;
            }

            public BackupJobExtraBuilder SetPath(string path)
            {
                _path = path;
                return this;
            }

            public BackupJobExtraBuilder SetAlgorithm(IRestorePointAlgorithm algorithm)
            {
                _algorithm = algorithm;
                return this;
            }

            public BackupJobExtraBuilder SetRestorePointNumberLimit(int restorePointNumberLimit)
            {
                _restorePointNumberLimit = restorePointNumberLimit;
                return this;
            }

            public BackupJobExtraBuilder SetRestorePointDateLimit(DateTime restorePointDateLimit)
            {
                _restorePointDateLimit = restorePointDateLimit;
                return this;
            }

            public BackupJobExtraBuilder SetLogger(ILogger logger)
            {
                _logger = logger;
                return this;
            }

            public BackupJobExtra GetBackupJobExtra() => new BackupJobExtra(_name, _path, _algorithm, _restorePointNumberLimit, _restorePointDateLimit, _logger);
        }
    }
}