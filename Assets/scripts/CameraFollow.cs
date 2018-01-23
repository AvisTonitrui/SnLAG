using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    //this script is put onto the follow point to make it follow at an exact distance to the player

    //constants for maximum Y angle on camera
    private const float yAngleMin = -50.0f;
    private const float yAngleMax = 50.0f;

    //Transform vars
    public Transform lookAt;
    private Transform camTransform;

    //vars for distance and default vars for rotation
    private float distance = 3.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;

    //gameobject var for stopping clipping
    GameObject intersect;

	// Use this for initialization
	void Start () {

        //initializing the transform of the attached object
        camTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        //getting the mouse input for rotation
        currentX += Input.GetAxis("Mouse X") * 1.5f;
        currentY -= Input.GetAxis("Mouse Y") * 1.5f;

        //clamping the Y angle to prevent looping
        currentY = Mathf.Clamp(currentY, yAngleMin, yAngleMax);
	}

    private void LateUpdate() {
        //moving the camera to an initial position to manipulate if it is outside the bounds
        Vector3 dir = new Vector3(0, 0.75f, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        //conditional to the camera be right behind the player if interacting with a box

        camTransform.position = lookAt.position + new Vector3(0, 1, 0) + rotation * dir;

        //setting up booleans for logic control later
        bool hitWall = false;

        //looking at the target so the forward is in the correct rotation
        camTransform.LookAt(lookAt.position + new Vector3(0, 1, 0));

        //initializing loop counter for saftey breaking
        int loops = 0;

        //resetting distance
        distance = 3.0f;

        //check to see if the camera is outside the camera walls, and bring it closer if it is
        //safety counter is included to prevent infinite looping
        while (loops < 500) {
            
            //debug ray
            //Debug.DrawRay(camTransform.position, camTransform.forward * distance, Color.magenta);

            //first creates an array of all hits within distance
            RaycastHit[] hits;
            hits = Physics.RaycastAll(new Ray(camTransform.position, camTransform.forward * distance), distance);

            //checks if ray hit the camera wall
            for (int i = 0; i < hits.Length; i++) {
                //Debug.Log(hits[i].collider.gameObject.tag);

                if (hits[i].collider.gameObject.tag == "CameraWall") {
                    hitWall = true;
                    break;
                } else {
                    hitWall = false;
                }
            }

            //decreasing distance if hit, plus logic control for distance re-increase
            if (hitWall) {
                distance -= 0.01f;

                //repositioning the camera
                distance = Mathf.Clamp(distance, 0.1f, 3f);
                dir = new Vector3(0, 0.75f, -distance);
                camTransform.position = lookAt.position + new Vector3(0, 1, 0) + rotation * dir;
                camTransform.LookAt(lookAt.position);
            } else {
                break;
            }

            loops++;
        }

        //Debug.Log(loops.ToString() + " Walls");
        loops = 0;

        //setting up booleans for logic control later
        bool hitFloor = false;
        //bool hitFloor2 = false;

        //looking at the target so the forward is in the correct rotation
        camTransform.LookAt(lookAt.position + new Vector3(0, 1, 0));

        //check to see if the camera is outside the camera walls, and bring it closer if it is
        while (loops < 500) {

            //debug ray
            //Debug.DrawRay(camTransform.position, -camTransform.up * 10.0f, Color.cyan);

            //first creates an array of all hits within distance
            RaycastHit[] hits;
            hits = Physics.RaycastAll(new Ray(camTransform.position, -camTransform.up * 10.0f), 10.0f);

            //checks if ray hit the camera wall
            for (int i = 0; i < hits.Length; i++) {
                if (hits[i].collider.gameObject.tag == "CameraFloor") {
                    hitFloor = true;
                    break;
                }
                else {
                    hitFloor = false;
                }
            }

            //decreasing distance if hit, plus logic control for distance re-increase
            if (!hitFloor) {
                //repositioning the camera
                camTransform.position = camTransform.position + new Vector3(0, 0.01f, 0);
            }
            else {
                break;
            }

            loops++;
        }

        //Debug.Log(loops.ToString() + " Floors");
        loops = 0;

        //having the camera look at the target after all transformations are complete
        camTransform.LookAt(lookAt.position + new Vector3(0, 1, 0));
    }
}
