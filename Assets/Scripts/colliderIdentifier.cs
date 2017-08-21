using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderIdentifier : MonoBehaviour {
	// Use this for initialization


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collision) {
        
        if(collision.gameObject.tag == "crasher")
        {
            MobSpawn.score++;
            foreach (Transform child in collision.gameObject.transform) {
                Destroy(child.gameObject);
            }
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        
    }

}
