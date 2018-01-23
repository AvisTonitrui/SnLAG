using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollisionControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision) {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
