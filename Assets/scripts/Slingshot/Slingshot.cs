using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour {
    //this script is for firing the slingshot, it will check both for slingshot targets and for lighting braziers

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire")) {
            //only goes off when the mouse is cliked
            RaycastHit hit;


            //casts a ray for the lengthy of the largest map
            if (Physics.Raycast(transform.position + new Vector3(0, 1, 0) , transform.forward * 139, out hit)) {
                if (hit.collider.tag == "Target") {
                    //code that activates the collider, which is a target
                    hit.collider.gameObject.GetComponent<Target>().activate = true;
                } else if (hit.collider.tag == "Fire") {
                    Debug.Log("I hit a fire!");

                    //this code now makes a raycastAll to see if a lit brazier is in the path, which will then light all other braziers hit
                    RaycastHit[] hits;
                    hits = Physics.RaycastAll(transform.position + new Vector3(0, 1, 0), transform.forward * 139);

                    //var for knowing whether a lit brazier was hit or not
                    bool litHit = false;

                    //checks if ray hit a lit brazier
                    for (int i = 0; i < hits.Length; i++) {
                        if (hits[i].collider.gameObject.GetComponent<LightStart>() != null) {
                            //for hitting an initial flame
                            litHit = true;
                            break;

                        } else if (hits[i].collider.gameObject.GetComponent<Light>() != null) {
                            //for hitting a non-initial flame
                            if (hits[i].collider.gameObject.GetComponent<Light>().lit) {
                                litHit = true;
                                break;
                            }
                        }
                    }

                    Debug.Log(litHit);

                    if (litHit) {
                        for (int i = 0; i < hits.Length; i++) {
                            if (hits[i].collider.tag == "Fire" && hits[i].collider.gameObject.GetComponent<Light>() != null) {
                                hits[i].collider.gameObject.GetComponent<Light>().lit = true;
                            }
                        }
                    }
                }
            }

        }

	}
}
