using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    public GameObject target;
    public GameObject thisObject;
    public float stepValue = 2.0f;
    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {

        float step = stepValue * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z), step);
        if (this.transform.position == target.transform.position) {
            Destroy(gameObject);
            Destroy(thisObject);
            MobSpawn.lives--;
        }
    }

    public void changeSpeed(float newSpeed) {
        stepValue = newSpeed;
    }
    // private float velocity = "Something";

    //function OnTriggerEnter/OnCollisionEnter(col : Collider ) { if (GameObject.FindWithTag("wall")} { velocity = 0; )

}