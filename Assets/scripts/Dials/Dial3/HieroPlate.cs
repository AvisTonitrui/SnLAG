using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HieroPlate : MonoBehaviour {
    //this script is for the Hieroglyphic pressure plate used to give access to dial3
    public bool impressed = false;
    Vector3 targetPos;

	// Use this for initialization
	void Start () {
        targetPos = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (impressed) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.001f);
        }
	}
}
