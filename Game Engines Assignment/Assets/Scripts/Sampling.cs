using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sampling : MonoBehaviour
{

    public static List<Vector3> Points(float radius, Vector3 sampleSize, int numSamples = 30)
    {
        float cellSize = radius / Mathf.Sqrt(2);

        int[,] grid = new int[Mathf.CeilToInt(sampleSize.x / cellSize), Mathf.CeilToInt(sampleSize.z / cellSize)];
        List<Vector3> points = new List<Vector3>();
        List<Vector3> spawnPoints = new List<Vector3>();

        spawnPoints.Add(sampleSize / 2);
        while (spawnPoints.Count > 0)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Count);
            Vector3 spawnCentre = spawnPoints[spawnIndex];
            bool accept = false;

            for (int i = 0; i < numSamples; i++)
            {
                float angle = Random.value * Mathf.PI * 2;
                Vector3 dir = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
                Vector3 candidate = spawnCentre + dir * Random.Range(radius, 2 * radius);

                if (isValid(candidate, sampleSize, cellSize, radius, points, grid))
                {
                    points.Add(candidate);
                    spawnPoints.Add(candidate);
                    grid[(int)(candidate.x / cellSize), (int)(candidate.z / cellSize)] = points.Count;
                    accept = true;
                    break;
                }
            }
            
            if (!accept)
            {
                spawnPoints.RemoveAt(spawnIndex);
            }
        }

        return points;
    }

    static bool isValid(Vector3 candidate, Vector3 sampleSize, float cellSize, float radius, List<Vector3> points, int [,] grid)
    {
        if(candidate.x >= 0 && candidate.x < sampleSize.x && candidate.z >= 0 && candidate.z < sampleSize.z)
        {
            int cellX = (int)(candidate.x / cellSize);
            int cellZ = (int)(candidate.z / cellSize);
            int startX = Mathf.Max(0, cellX - 2);
            int endX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);
            int startZ = Mathf.Max(0, cellZ - 2);
            int endZ = Mathf.Min(cellZ + 2, grid.GetLength(1) - 1);

            for( int x = startX; x <= endX; x++)
            {
                for( int z = startZ; z <= endZ; z++)
                {
                    int pointIndex = grid[x,z] - 1;
                    if(pointIndex != -1)
                    {
                        float sqrDist = (candidate - points[pointIndex]).sqrMagnitude;
                        if(sqrDist < radius)
                        {
                            return false;
                        }
                    }

                }
            }
            return true;
        }
        return false;
    }

}
