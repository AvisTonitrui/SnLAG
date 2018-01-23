using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dial2 : MonoBehaviour {
    //this script is for the second dial, which rises after a target is hit and moves the first instance of quetzalcoatl out of the way

    //initial gameObjects needed
    public GameObject target;
    public GameObject quetz;
    GameObject trigger;

    //boolean to show whether or not the dial has already been activated
    bool activated = false;

    //target position for when the dial sinks
    Vector3 targetPos;

    //target position for quetzalcoatl
    Vector3 quetzTarget;

    // Use this for initialization
    void Start () {
        //finds the trigger child object
        foreach (Transform child in transform) {
            if (child.tag == "Dial") {
                trigger = child.gameObject;
            }
        }

        //setting the target positions
        targetPos = transform.position + new Vector3(0, 0.1f, 0);
        quetzTarget = quetz.transform.position + new Vector3(0, 0, 20);
    }
	
	// Update is called once per frame
	void Update () {
        if (trigger.GetComponent<Dial>().active) {
            activated = true;
        }

        if (target.GetComponent<Target>().activate && !activated) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 2.77f, transform.position.z), 0.01f);
        } else if (target.GetComponent<Target>().activate && activated) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.01f);

            quetz.transform.position = Vector3.MoveTowards(quetz.transform.position, quetzTarget, 0.03f);
        }
	}
}
