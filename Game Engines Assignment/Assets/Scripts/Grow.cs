﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour {

    public GameObject[] trees;
    float radius = 3;
    public Vector3 regionSize = Vector3.one;
    public int numSamples = 30;
    public float displayRadius = 1;

    List<Vector3> points;

    void Start()
    {
        points = Sampling.Points(radius, regionSize, numSamples);

        {
            foreach (Vector3 point in points)
            {
                Instantiate(trees[Random.Range(0, trees.GetLength(0))], point, Quaternion.identity);
            }
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(regionSize / 2, regionSize);
        if (points != null)
        {
            foreach (Vector3 point in points)
            {
                //Gizmos.DrawWireSphere(point, displayRadius);

            }
        }

    }*/
}
