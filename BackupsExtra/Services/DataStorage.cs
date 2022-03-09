using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Entities;
using Newtonsoft.Json;

namespace BackupsExtra.Services
{
    public class DataStorage
    {
        private JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            Converters = new List<JsonConverter>
            {
                new FileInfoConverter(),
            },
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
        };
        public DataStorage()
        {
        }

        public void Save(BackupJob backupJob)
        {
            string json = JsonConvert.SerializeObject(backupJob, Formatting.Indented, _settings);
            File.WriteAllText(@"./../../../../BackupsExtra/DataStorage.json", json);
        }

        public BackupJob Unload(string backupJobPath)
        {
            string file = File.ReadAllText(@"./../../../../BackupsExtra/DataStorage.json");
            BackupJob backupJob = JsonConvert.DeserializeObject<BackupJob>(file, _settings);
            if (file.Length != 0 && backupJob.Path == backupJobPath)
            {
                return backupJob;
            }

            return null;
        }
    }
}