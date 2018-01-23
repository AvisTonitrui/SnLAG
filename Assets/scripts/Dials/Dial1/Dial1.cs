using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dial1 : MonoBehaviour {
    //this script is for the first dial, which moves the coverstone of the first floor out of the way backwards

    //needed gameObjects
    public GameObject coverstone;
    GameObject trigger;

    //boolean to show whether or not the dial has already been activated
    bool activated = false;

    //target position for when the dial sinks
    Vector3 targetPos;

	// Use this for initialization
	void Start () {
		//finds the trigger child object
        foreach (Transform child in transform) {
            if (child.tag == "Dial") {
                trigger = child.gameObject;
            }
        }

        //setting the target position
        targetPos = transform.position - new Vector3(0, 0.1f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (trigger.GetComponent<Dial>().active && !activated) {
            //having the coverstone activate
            coverstone.GetComponent<Coverstone>().activate = true;
            activated = true;
        }

        if (activated) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.001f);
        }
	}
}
