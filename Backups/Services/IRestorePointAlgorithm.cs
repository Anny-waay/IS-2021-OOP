using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Backups.Services
{
    public interface IRestorePointAlgorithm
    {
        List<string> CreateZip(List<FileInfo> files, string path, string restorePointNumber);
    }
}
