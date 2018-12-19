using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed = 10.0f;

    void Start()
    {
        //Locks cursor to game screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //Setting basic movement controls for forward and to the side
        float translation = Input.GetAxis("Vertical") * speed;
        float strafe = Input.GetAxis("Horizontal") * speed;
        //Multyplying by time.delta for smoother movement 
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;
        //Moving the transform of the gameobject with transform.translate 
        transform.Translate(strafe, 0, translation);

        //Pressing escape will unlock mouse cursor
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        //Simple fly implementation
        if(Input.GetKey(KeyCode.E))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.F))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }
}
