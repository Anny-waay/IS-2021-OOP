using System;
using System.Collections.Generic;
using Backups.Entities;
using Backups.Tools;
using BackupsExtra.Tools;

namespace BackupsExtra.Services
{
    public class DeleteByNumberAndDate : IDeleteAlgorithm
    {
        public DeleteByNumberAndDate()
        {
        }

        public int FindPointsToDelete(List<RestorePoint> restorePoints, int limitByNumber, DateTime limitByDate)
        {
            var pointsToDelete = new List<RestorePoint>();
            if (restorePoints.Count > limitByNumber)
            {
                for (int i = 0; i < restorePoints.Count - limitByNumber; i++)
                {
                    if (restorePoints[i].Date.CompareTo(limitByDate) < 0)
                    {
                        pointsToDelete.Add(restorePoints[i]);
                    }
                    else
                    {
                        throw new BackupsException("It is impossible to meet both limits at once!");
                    }
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