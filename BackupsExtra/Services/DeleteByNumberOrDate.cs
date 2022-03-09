using System;
using System.Collections.Generic;
using Backups.Entities;
using BackupsExtra.Tools;

namespace BackupsExtra.Services
{
    public class DeleteByNumberOrDate : IDeleteAlgorithm
    {
        public DeleteByNumberOrDate()
        {
        }

        public int FindPointsToDelete(List<RestorePoint> restorePoints, int limitByNumber, DateTime limitByDate)
        {
            var pointsToDelete = new List<RestorePoint>();
            for (int i = 0; i < restorePoints.Count - limitByNumber; i++)
            {
                pointsToDelete.Add(restorePoints[i]);
            }

            for (int i = restorePoints.Count - limitByNumber; i < restorePoints.Count; i++)
            {
                if (restorePoints[i].Date.CompareTo(limitByDate) < 0)
                {
                    pointsToDelete.Add(restorePoints[i]);
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