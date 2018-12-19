using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisual : MonoBehaviour {

    GameObject target;
    public float maxScale = 2;

	// Use this for initialization
	void Start ()
    {
        target = gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
		for(int i = 0; i < 512; i++)
        {
            target.transform.localScale = new Vector3(2, (AudioPlayer.samples[i] * maxScale) + 2, 2);
        }
	}
}
