using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coverstone : MonoBehaviour {
    //this script is for the coverston, which will move back once it has been activated

    //bools for activation and making sure it hasn't already activated
    public bool activate;

    //the target location for the coverstone
    Vector3 target;

	// Use this for initialization
	void Start () {
        target = transform.position + new Vector3(-3.1f, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (activate) {
            transform.position = Vector3.MoveTowards(transform.position, target, 0.02f);

        }
	}
}
