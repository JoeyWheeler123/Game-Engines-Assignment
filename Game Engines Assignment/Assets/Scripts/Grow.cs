using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour {

    public void Grown()
	{
		gameObject.transform.position += Vector3.up;
	}
}
