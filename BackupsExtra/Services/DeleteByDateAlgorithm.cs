using System;
using System.Collections.Generic;
using Backups.Entities;
using BackupsExtra.Tools;

namespace BackupsExtra.Services
{
    public class DeleteByDateAlgorithm : IDeleteAlgorithm
    {
        public DeleteByDateAlgorithm()
        {
        }

        public int FindPointsToDelete(List<RestorePoint> restorePoints, int limitByNumber, DateTime limitByDate)
        {
            var pointsToDelete = new List<RestorePoint>();
            foreach (RestorePoint restorePoint in restorePoints)
            {
                if (restorePoint.Date.CompareTo(limitByDate) < 0)
                {
                    pointsToDelete.Add(restorePoint);
                }
                else
                {
                    break;
                }
            }

            if (pointsToDelete.Count == 0)
            {
                throw new BackupsExtraException("No points to delete!");
            }

            return pointsToDelete.Count;
        }
    }
}