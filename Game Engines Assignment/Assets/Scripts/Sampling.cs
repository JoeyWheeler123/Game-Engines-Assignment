using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sampling : MonoBehaviour
{

    public static List<Vector3> Points(float radius, Vector3 sampleSize, int numSamples = 30)
    {
        //Calculating the size of the cells in the grid(r/square root of 2)
        float cellSize = radius / Mathf.Sqrt(2);
        
        //2d array, the amount of cells that fit into sample size and round to integars will Mathf.CeiltoInt
        int[,] grid = new int[Mathf.CeilToInt(sampleSize.x / cellSize), Mathf.CeilToInt(sampleSize.z / cellSize)];
        //Make a new list of vecotr3s that will be the points
        List<Vector3> points = new List<Vector3>();
        //Make another list of Vector3s that will calculate if a point can be spawned, if it can't
        //it will be removed
        List<Vector3> spawnPoints = new List<Vector3>();

        //Adding a start point in the middle
        spawnPoints.Add(sampleSize / 2);

        //Loop that starts once there is at least one point in the grid
        while (spawnPoints.Count > 0)
        {
            //It will select a random point within the grid and make it the centre spot to calculate can a another point be spawned
            int spawnIndex = Random.Range(0, spawnPoints.Count);
            Vector3 spawnCentre = spawnPoints[spawnIndex];
            bool accept = false;

            //Loop that will check for available spawn locations
            for (int i = 0; i < numSamples; i++)
            {
                //Will check a random angle
                float angle = Random.value * Mathf.PI * 2;
                //Vector3 direction of the sin on the X axis and the cos on the z
                Vector3 dir = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
                //Create a candidate point that's Radius is min value as it guaruntees point will spawn outside of the centre point
                Vector3 candidate = spawnCentre + dir * Random.Range(radius, 2 * radius);

                //If it is possible to spawn the candidate it will be added to the lis of points
                if (isValid(candidate, sampleSize, cellSize, radius, points, grid))
                {
                    points.Add(candidate);
                    spawnPoints.Add(candidate);
                    grid[(int)(candidate.x / cellSize), (int)(candidate.z / cellSize)] = points.Count;
                    accept = true;
                    break;
                }
            }
            
            //Here if no candidate was accepted the spawn point will be removed
            if (!accept)
            {
                spawnPoints.RemoveAt(spawnIndex);
            }
        }

        return points;
    }

    static bool isValid(Vector3 candidate, Vector3 sampleSize, float cellSize, float radius, List<Vector3> points, int [,] grid)
    {
        //check if the candidate is within the sample region
        if(candidate.x >= 0 && candidate.x < sampleSize.x && candidate.z >= 0 && candidate.z < sampleSize.z)
        {
            //Find out which cell the candiate is within so that we can check the surrounding cells
            int cellX = (int)(candidate.x / cellSize);
            int cellZ = (int)(candidate.z / cellSize);
            //Check cells in 5*5 grid
            int startX = Mathf.Max(0, cellX - 2);
            int endX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);
            int startZ = Mathf.Max(0, cellZ - 2);
            int endZ = Mathf.Min(cellZ + 2, grid.GetLength(1) - 1);

            //loops the check over the cells
            for( int x = startX; x <= endX; x++)
            {
                for( int z = startZ; z <= endZ; z++)
                {
                    //if the point index is = -1 it means there is no point in the cell
                    int pointIndex = grid[x,z] - 1;
                    if(pointIndex != -1)
                    {
                        //Find distance between the point found and the candidate
                        float sqrDist = (candidate - points[pointIndex]).sqrMagnitude;
                        //If the distance is less than the radius, the candidate is too close
                        if(sqrDist < radius*radius)
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
