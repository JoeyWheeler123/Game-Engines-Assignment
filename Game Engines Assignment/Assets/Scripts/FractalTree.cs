using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractalTree : MonoBehaviour
{

	public int sections = 6;
	public int branches = 2;

	public float scale = .5f;
	float angle = 30;

	// Use this for initialization
	void Start()
	{

		sections -= 1;
		for (int i = 0; i < branches; i++)
		{
			if (sections > 0)
			{
				var duplicate = Instantiate(gameObject);
				var duplication = duplicate.GetComponent<FractalTree>();
				//duplication.SendMessage("Grown");
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
		gameObject.transform.position += transform.up * gameObject.transform.localScale.y;
		gameObject.transform.rotation *= Quaternion.Euler(Random.Range(25f, 60f) * ((index * 2) -1), Random.Range(25f, 90f), 0);
		//gameObject.transform.rotation *= Quaternion.Euler(angle, 0, 0);
		gameObject.transform.localScale *= scale;
	}

}
