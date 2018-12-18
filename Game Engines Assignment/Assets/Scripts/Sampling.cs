﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sampling : MonoBehaviour
{

    public static List<Vector2> Points(float radius, Vector2 sampleSize, int numSamples = 30)
    {
        float cellSize = radius / Mathf.Sqrt(2);

        int[,] grid = new int[Mathf.CeilToInt(sampleSize.x / cellSize), Mathf.CeilToInt(sampleSize.y / cellSize)];
        List<Vector2> points = new List<Vector2>();
        List<Vector2> spawnPoints = new List<Vector2>();

        spawnPoints.Add(sampleSize / 2);
        while (spawnPoints.Count > 0)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Count);
            Vector2 spawnCentre = spawnPoints[spawnIndex];
            bool accept = false;

            for (int i = 0; i < numSamples; i++)
            {
                float angle = Random.value * Mathf.PI * 2;
                Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                Vector2 candidate = spawnCentre + dir * Random.Range(radius, 2 * radius);

                if (isValid(candidate, sampleSize, cellSize, radius, points, grid))
                {
                    points.Add(candidate);
                    spawnPoints.Add(candidate);
                    grid[(int)(candidate.x / cellSize), (int)(candidate.y / cellSize)] = points.Count;
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

    static bool isValid(Vector2 candidate, Vector2 sampleSize, float cellSize, float radius, List<Vector2> points, int [,] grid)
    {
        if(candidate.x >= 0 && candidate.x < sampleSize.x && candidate.y >= 0 && candidate.y < sampleSize.y)
        {
            int cellX = (int)(candidate.x / cellSize);
            int cellY = (int)(candidate.y / cellSize);
            int startX = Mathf.Max(0, cellX - 2);
            int endX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);
            int startY = Mathf.Max(0, cellY - 2);
            int endY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);

            for( int x = startX; x <= endX; x++)
            {
                for( int y = startY; y <= endY; y++)
                {
                    int pointIndex = grid[x, y] - 1;
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
        }
        return true;
    }

}
