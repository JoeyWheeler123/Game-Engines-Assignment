using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractalTree : MonoBehaviour
{
    //Here I set how many sections of the tree I would like as well as how many branches
    //(Note this affects performance heavily with regards to the mount of trees in the scene)
	public int sections = 6;
    //Controls the split in the tree
	public int branches = 2;

	public float scale = .5f;

	// Use this for initialization
	void Start()
	{

		sections -= 1;
        //Start a for loop the length of the branches int
		for (int i = 0; i < branches; i++)
		{
            //Basic recursion
			if (sections > 0)
			{
                //Constantly instatiate new branches unitl the loop is finished
				var duplicate = Instantiate(gameObject);
				var duplication = duplicate.GetComponent<FractalTree>();
                //Coloring the branches here
                transform.GetChild(0).GetComponent<Renderer>().material.color = Color.HSVToRGB(Random.Range(0, 1.0f), 1, 1);
                transform.GetChild(1).GetComponent<Renderer>().material.color = Color.HSVToRGB(Random.Range(0, 1.0f), 1, 1);
                duplication.Grown(i);
			}
		}

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Grown(int index)
	{
        //Here is the process in which each branch goes through before growing
		gameObject.transform.position += transform.up * gameObject.transform.localScale.y;
        //A randomish angle on each branch(not completely random for aesthtics)
		gameObject.transform.rotation *= Quaternion.Euler(Random.Range(25f, 60f) * ((index * 2) -1), Random.Range(25f, 90f), 0);
        //Constantly shortening the length of each branch
		gameObject.transform.localScale *= scale;
	}

}
