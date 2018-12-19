using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour {
    //Array of different types of tree(effects performance heavily)
    public GameObject[] trees;
    //Radius between each tree
    float radius = 3;
    //The size of the area in which trees spawn in(large area = lots of trees. Will most likely crash if too many)
    public Vector3 regionSize = Vector3.one;
    //Number of checks it will do before placing a tree
    public int numSamples = 30;
    //Display size for gizmo for testing purposes
    //public float displayRadius = 1;

    //List of points where the trees will spawn
    List<Vector3> points;

    void Start()
    {
        //Here I access the points calculated in the Sampling script and assign them to
        //the list of Vector3s 
        points = Sampling.Points(radius, regionSize, numSamples);

        {
            //Here a tree is instantiated at every point calculated
            foreach (Vector3 point in points)
            {
                Instantiate(trees[Random.Range(0, trees.GetLength(0))], point, Quaternion.identity);
            }
        }
    }

    //Thisused for testing purposes
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(regionSize / 2, regionSize);
        if (points != null)
        {
            foreach (Vector3 point in points)
            {
                Gizmos.DrawWireSphere(point, displayRadius);

            }
        }

    }*/
}
