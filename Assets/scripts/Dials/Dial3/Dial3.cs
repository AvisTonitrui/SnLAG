using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dial3 : MonoBehaviour {
    //this script is for the third dial, which rises after the correct plate is interacted with and moves the second instance of quetzalcoatl out of the way

    //initial gameObjects needed
    public GameObject plate;
    public GameObject quetz;
    GameObject trigger;

    //boolean to show whether or not the dial has already been activated
    bool activated = false;

    //target position for when the dial sinks
    Vector3 targetPos;

    // Use this for initialization
    void Start() {
        //finds the trigger child object
        foreach (Transform child in transform) {
            if (child.tag == "Dial") {
                trigger = child.gameObject;
            }
        }

        //setting the target position
        targetPos = transform.position + new Vector3(0, 0.1f, 0);
    }

    // Update is called once per frame
    void Update() {
        if (trigger.GetComponent<Dial>().active) {
            activated = true;
        }

        if (plate.GetComponent<HieroPlate>().impressed && !activated) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos + new Vector3(0, 0.1f, 0), 0.01f);
        } else if (plate.GetComponent<HieroPlate>().impressed && activated) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.01f);
            quetz.transform.position = new Vector3(quetz.transform.position.x, quetz.transform.position.y, quetz.transform.position.z - 0.03f);
        }
    }
}
