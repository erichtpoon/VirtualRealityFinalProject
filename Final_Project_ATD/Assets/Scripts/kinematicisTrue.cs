using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kinematicisTrue : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collision) {

        if (collision.gameObject.tag == "crasher") {
            if (collision.gameObject.GetComponent<Rigidbody>().isKinematic == false) {
                foreach (Rigidbody child in collision.gameObject.transform) {
                    child.isKinematic = true;
                }
            }

        }


    }
}
