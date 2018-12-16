using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public GameObject mainCamera;
    public float speed;
    public float lookSpeed;

	// Use this for initialization
	void Start ()
    {
		if (mainCamera == null)
        {
            mainCamera = Camera.main.gameObject;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        //This is for keyboard and mouse control
        float mouseX;
        float mouseY;
        float moving = Input.GetAxis("Vertical");
        float strafing = Input.GetAxis("Horizontal");
        Move(moving * speed * Time.deltaTime);
        Strafe(strafing * speed * Time.deltaTime);
        mouseX = Input.GetAxis("MouseX");
        mouseY = Input.GetAxis("MouseY");
        LookX(mouseX * lookSpeed * Time.deltaTime);
	}

    void Move(float units)
    {
        transform.position += mainCamera.transform.forward * units;
    }

    void Strafe(float units)
    {
        transform.position += mainCamera.transform.right * units;
    }

    void LookX(float degrees)
    {
        Quaternion rotate = Quaternion.AngleAxis(degrees, Vector3.forward);
        transform.rotation = rotate * transform.rotation;
    }

    void LookY(float degrees)
    {
        Quaternion rotate = Quaternion.AngleAxis(degrees, Vector3.up);
        transform.rotation = rotate * transform.rotation;
    }
}
