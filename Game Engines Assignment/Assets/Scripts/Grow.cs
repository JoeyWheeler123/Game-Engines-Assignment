﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour {

    //public GameObject tree;
    float radius = 1;
    public Vector3 regionSize = Vector3.one;
    public int rejectionSamples = 30;
    public float displayRadius = 1;

    List<Vector3> points;

    private void OnEnable()
    {
        points = Sampling.Points(radius, regionSize, rejectionSamples);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(regionSize / 2, regionSize);
        if (points != null)
        {
            foreach (Vector3 point in points)
            {
                Gizmos.DrawWireSphere(point, displayRadius);
                //Instantiate(tree, point, Quaternion.identity);
            }
        }

    }

    /*private void spawnTrees()
    {
        Gizmos.DrawWireCube(regionSize / 2, regionSize);
        if (points != null)
        {
            foreach (Vector3 point in points)
            {
                Instantiate(tree, point, Quaternion.identity);
            }
        }
    }*/
}
