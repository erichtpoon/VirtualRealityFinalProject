using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerAttackMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collision) {

        Destroy(collision.gameObject);
        Destroy(this.gameObject);

    }
}
