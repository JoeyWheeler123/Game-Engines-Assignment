using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoissonDiscSampler : MonoBehaviour
{

    private const int k = 30;

    private readonly Vector3 cube;
    private readonly float radius2;
    private readonly float cellSize;
    private Vector3[,,] grid;
    private List<Vector3> activeSamples = new List<Vector3>();

    public PoissonDiscSampler(float width, float height, float depth, float radius)
    {
        cube = new Vector3(width, height, depth); // Here I would make the width, height and depth the same as terrain
        radius2 = radius2 * radius;
        cellSize = radius / Mathf.Sqrt(3);
        grid = new Vector3[Mathf.CeilToInt(width / cellSize), Mathf.CeilToInt(height / cellSize), Mathf.CeilToInt(depth / cellSize)];
        Debug.Log(grid.GetLength(0));
        Debug.Log(grid.GetLength(1));
        Debug.Log(grid.GetLength(2));
    }

    public IEnumerable<Vector3> Samples()
    {
        yield return AddSample(new Vector3(Random.value * cube.x, Random.value * cube.y, Random.value * cube.z));

        while (activeSamples.Count > 0)
        {
            int i = (int)Random.value * activeSamples.Count;
            Vector3 sample = activeSamples[i];

            bool found = false;
            for (int j = 0; j < k; i++)
            {
                Vector3 candidate = GenerateRandomPointAround(sample, Random.value * 3 * radius2 + radius2);

                if (IsContains(candidate, cube) && IsFarEnough(candidate))
                {
                    found = true;
                    yield return AddSample(candidate);
                    break;
                }
            }

            if (!found)
            {
                activeSamples[i] = activeSamples[activeSamples.Count - 1];
                activeSamples.RemoveAt(activeSamples.Count - 1);
            }
        }
    }

    private bool IsContains(Vector3 v, Vector3 area)
    {
        if (v.x >= 0 && v.x < area.x && v.y >= 0 && v.y < area.y && v.z >= 0 && v.z < area.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Vector3 GenerateRandomPointAround(Vector3 point, float minDist)
    {
        float r1 = Random.value;
        float r2 = Random.value;
        float r3 = Random.value;

        float radius = minDist * (r1 + 1);

        float angle1 = 2.0f * Mathf.PI * r2;
        float angle2 = 2.0f * Mathf.PI * r3;

        float newX = point.x + radius * Mathf.Cos(angle1) * Mathf.Sin(angle2);
        float newY = point.y + radius * Mathf.Sin(angle1) * Mathf.Sin(angle2);
        float newZ = point.z + radius * Mathf.Cos(angle2);

        return new Vector3(newX, newY, newZ);
    }

    private bool IsFarEnough(Vector3 sample)
    {
        GridPos pos = new GridPos(sample, cellSize);

        int xmin = Mathf.Max(pos.x - 2, 0);
        int ymin = Mathf.Max(pos.y - 2, 0);
        int zmin = Mathf.Max(pos.z - 2, 0);

        int xmax = Mathf.Min(pos.x + 2, grid.GetLength(0) - 1);
        int ymax = Mathf.Min(pos.y + 2, grid.GetLength(1) - 1);
        int zmax = Mathf.Min(pos.z + 2, grid.GetLength(2) - 1);

        for( int z = zmin; z <= zmax; z++)
        {
            for( int y = ymin; y <= ymax; y++)
            {
                for( int x = xmin; x <= xmax; x++)
                {
                    Vector3 s = grid[x, y, z];
                    if(s != Vector3.zero)
                    {
                        Vector3 d = s - sample;
                        if (d.x * d.x + d.y * d.y + d.z * d.z < radius2) return false;
                    }
                }
            }
        }

        return true;
    }

    private Vector3 AddSample ( Vector3 sample)
    {
        activeSamples.Add(sample);
        GridPos pos = new GridPos(sample, cellSize);
        grid[pos.x, pos.y, pos.z] = sample;
        return sample;
    }

    private struct GridPos
    {
        public int x;
        public int y;
        public int z;

        public GridPos(Vector3 sample, float cellSize)
        {
            x = (int)(sample.x / cellSize);
            y = (int)(sample.y / cellSize);
            z = (int)(sample.z / cellSize);
        }
    }
}

    

