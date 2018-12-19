using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour {

    Vector2 mousePos;
    Vector2 move;
    public float speed = 5.0f;
    public float slide = 3.0f;
    float minY = -90;
    float maxY = 90;

    GameObject player;

    private void Start()
    {
        //This script will be a component of the main camera which is a child of the player
        //I set that so here
        player = transform.parent.gameObject;
    }

    private void Update()
    {
        //Getting the mouse X position and the mouse Y position and settin git to a vector2
        var mouseP = new Vector2(Input.GetAxisRaw("Mouse X"), (Input.GetAxisRaw("Mouse Y")));
        //I multiply the mouse position x and y by the speed in which we want the camera rotates,
        //whilst also multiplying by the slide variable for smoothness
        mouseP = Vector2.Scale(mouseP, new Vector2(speed * slide, speed * slide));
        //Lerp the camera to position with mouse movement
        move.x = Mathf.Lerp(move.x, mouseP.x, 1f / slide);
        move.y = Mathf.Lerp(move.y, mouseP.y, 1f / slide);
        //Updating mouse pos
        mousePos += move;
        //I clamp the Y rotation so that the camera cannot flip around if the mouse is moved too much
        mousePos.y = Mathf.Clamp(mousePos.y, minY, maxY);
        //Rotating the camera around the X axis here
        transform.localRotation = Quaternion.AngleAxis(-mousePos.y, Vector3.right);
        //Rotating around the Y axis here
        player.transform.localRotation = Quaternion.AngleAxis(mousePos.x, player.transform.up);
    }
}
