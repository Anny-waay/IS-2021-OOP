using System;
using System.Collections.Generic;
using System.IO;

namespace Backups.Entities
{
    public class RestorePoint
    {
        public RestorePoint()
        {
        }

        public RestorePoint(List<string> zipPaths)
        {
            Date = DateTime.Now;
            ZipPaths = zipPaths;
            JobFiles = new List<FileInfo>();
        }

        public RestorePoint(List<string> zipPaths, DateTime date)
        {
            Date = date;
            ZipPaths = zipPaths;
            JobFiles = new List<FileInfo>();
        }

        public DateTime Date { get; set; }
        public List<string> ZipPaths { get; set; }
        public List<FileInfo> JobFiles { get; set; }
    }
}
