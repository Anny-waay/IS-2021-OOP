using System;
using System.Collections.Generic;
using Backups.Entities;
using BackupsExtra.Tools;

namespace BackupsExtra.Services
{
    public class DeleteByNumberAlgorithm : IDeleteAlgorithm
    {
        public DeleteByNumberAlgorithm()
        {
        }

        public int FindPointsToDelete(List<RestorePoint> restorePoints, int limitByNumber, DateTime limitByDate)
        {
            var pointsToDelete = new List<RestorePoint>();
            if (restorePoints.Count > limitByNumber)
            {
                for (int i = 0; i < restorePoints.Count - limitByNumber; i++)
                {
                    pointsToDelete.Add(restorePoints[i]);
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