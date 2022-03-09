using System;
using System.Collections.Generic;
using Backups.Entities;

namespace BackupsExtra.Services
{
    public interface IDeleteAlgorithm
    {
        int FindPointsToDelete(List<RestorePoint> restorePoints, int limitByNumber, DateTime limitByDate);
    }
}