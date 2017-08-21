using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerAttack : MonoBehaviour {
    public GameObject towerArrows;

    private float coolDown = 10;
    private float step;
    private GameObject instantiatedArrow;
    private Collider col;

	// Use this for initialization
	void Start () {
        step = (float)10.0 * Time.deltaTime;
    }
	
	// Update is called once per frame
	void Update () {
        coolDown -= Time.deltaTime;
		print (coolDown);
        //print(coolDown);
        if(instantiatedArrow)
            instantiatedArrow.transform.position = Vector3.MoveTowards(instantiatedArrow.transform.position, col.gameObject.transform.position, step);
    }
    void OnTriggerStay(Collider collision) {

        if (collision.gameObject.tag == "crasher" ) {
            if (coolDown < 0) {
                col = collision;
                instantiatedArrow = Instantiate(towerArrows);
                instantiatedArrow.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                instantiatedArrow.transform.LookAt(collision.gameObject.transform.position);
                coolDown = 15;
                print("Hello!");
                Debug.Log(collision.gameObject.tag);
                MobSpawn.score++;
                //Destroy(collision.gameObject);
            }
            else if (collision.gameObject.GetComponent<Rigidbody>().isKinematic == false){
                collision.GetComponent<Rigidbody>().isKinematic = true;
                print("isKinematic is turned on");
            }
            if (collision.gameObject.GetComponent<Rigidbody>().isKinematic == true && coolDown < 0) {
                MobSpawn.score++;
                //Destroy(collision.gameObject);
            }
        }
       

    }
}
