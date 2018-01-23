using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerCheck : MonoBehaviour {
    //this script checks whenever the player enters a trigger and, depending on the trigger, executes/allows certain events

    //needed game objects
    GameObject trigger;
    GameObject dial;
    GameObject boxTrigger;
    GameObject box;
    GameObject plate;

    //booleans for triggers
    public bool canInteractDial = false;
    public bool canInteractBox = false;
    public bool interacting = false;
    public bool interacting2 = false;
    public bool canInteractPlate = false;

    //the transform of the player
    Transform playerPos;

	// Use this for initialization
	void Start () {
        playerPos = transform;
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Interact")) {
            //activates the dial
            if (canInteractDial) {
                dial.GetComponent<Dial>().active = true;
            }


            //will grab the box and allow it to move with the player
            if (canInteractBox) {
                if (!interacting) {
                    //first will snap the player to the location of the box trigger, but maintains its current y
                    playerPos.position = new Vector3(boxTrigger.transform.position.x, playerPos.position.y, boxTrigger.transform.position.z);

                    //lets the computer know that a box is attached to the player
                    interacting = true;

                    //sets the box being interacted with
                    box = boxTrigger.transform.parent.gameObject;
                }                
            }

            if (interacting2) {
                interacting = false;
                interacting2 = false;

                box.transform.rotation = new Quaternion(0, 0, 0, 0);
            }

            if (interacting) {
                interacting2 = true;
            }

            //interacts with a hieroglyph plate
            if (canInteractPlate) {
                plate.GetComponentInParent<HieroPlate>().impressed = true;
            }
        }

        //moves the box with the player if interacting with a box
        if (interacting) {
            //need to determine which direction the box is from the player
            if (playerPos.position.x > box.transform.position.x + 1) {
                box.transform.position = Vector3.MoveTowards(box.transform.position, new Vector3(playerPos.position.x - 1.35f, box.transform.position.y, playerPos.position.z + 6f), 1);
            } else if (playerPos.position.x < box.transform.position.x - 1) {
                box.transform.position = Vector3.MoveTowards(box.transform.position, new Vector3(playerPos.position.x + 1.35f, box.transform.position.y, playerPos.position.z + 6f), 1);
            } else if (playerPos.position.z > box.transform.position.z - 5) {
                box.transform.position = Vector3.MoveTowards(box.transform.position, new Vector3(playerPos.position.x, box.transform.position.y, playerPos.position.z + 4.6f), 1);
            } else if (playerPos.position.z < box.transform.position.z - 7) {
                box.transform.position = Vector3.MoveTowards(box.transform.position, new Vector3(playerPos.position.x, box.transform.position.y, playerPos.position.z + 7.3f), 1);
            }

        }
	}

    private void OnTriggerEnter(Collider other) {
        trigger = other.gameObject;

        //when entering the trigger range of a dial allows for interaction with the dial
        if (other.tag == "Dial") {
            dial = trigger;

            canInteractDial = true;
        }

        //when entering the trigger area of stairs, it will automatically move you to the next floor
        if (other.tag == "Stairs") {
            trigger.GetComponent<SceneTrigger>().activate = true;
        }

        //when entering the trigger of a box, allows the grabbing off the box
        if (other.tag == "Box") {
            // getting the box to be interacted with
            boxTrigger = trigger;

            canInteractBox = true;
        }

        if (other.tag == "Plate") {
            plate = trigger;

            canInteractPlate = true;

        }
    }

    private void OnTriggerExit(Collider other) {
        //for leaving a dial trigger
        if (other.tag == "Dial") {
            canInteractDial = false;
        }

        //for leaving a box trigger
        if (other.tag == "Box") {
            canInteractBox = false;
        }

        if (other.tag == "Plate") {
            canInteractPlate = false;
        }
    }
}
