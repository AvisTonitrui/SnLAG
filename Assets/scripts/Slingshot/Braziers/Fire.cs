using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    //this script is for actually lighting the fire

    //game object for self
    Renderer self;

	// Use this for initialization
	void Start () {
        self = gameObject.GetComponent<Renderer>();

        self.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (self.GetComponentInParent<LightStart>() != null) {
            self.enabled = true;
            //Debug.Log("Lighting fire");
        } else if (self.GetComponentInParent<Light>() != null){
            if (self.GetComponentInParent<Light>().lit) {
                self.enabled = true;
            } else {
                self.enabled = false;
            }
        } else {
            self.enabled = false;
        }
	}
}
