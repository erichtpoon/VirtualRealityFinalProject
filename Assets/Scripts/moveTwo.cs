using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTwo : MonoBehaviour {

    public GameObject target;
    public GameObject targetChoiceOne;
    public GameObject targetChoiceTwo;
    public GameObject thisObject;


    public static float step;
    private float whatIsEnd;
    private int integerwhatIsEnd;


    private bool wayPointPassed;

    public float stepValue = 2.0f;
    // Use this for initialization
    void Start() {
        step = (float)2.0 * Time.deltaTime;
        whatIsEnd = Random.Range(1.0f, 2.99f);
        integerwhatIsEnd = (int)whatIsEnd;

        

        wayPointPassed = false;
    }

    // Update is called once per frame
    void Update() {
        float step = stepValue * Time.deltaTime;
        if (!wayPointPassed) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z), step);
            if (this.transform.position == target.transform.position) {
                wayPointPassed = true;
            }
        }

        if (wayPointPassed) {
            if (integerwhatIsEnd == 1) {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetChoiceOne.transform.position.x, targetChoiceOne.transform.position.y, targetChoiceOne.transform.position.z), step);
                if (this.transform.position == targetChoiceOne.transform.position) {
                    Destroy(gameObject);
                    Destroy(thisObject);
                    MobSpawn.lives--;
                }

            }
            else if (integerwhatIsEnd == 2) {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetChoiceTwo.transform.position.x, targetChoiceTwo.transform.position.y, targetChoiceTwo.transform.position.z), step);
                if (this.transform.position == targetChoiceTwo.transform.position) {
                    Destroy(gameObject);
                    Destroy(thisObject);
                    MobSpawn.lives--;
                }
            }
        }

    }
    public void changeSpeed(float newSpeed) {
        stepValue = newSpeed;
    }

    // private float velocity = "Something";

    //function OnTriggerEnter/OnCollisionEnter(col : Collider ) { if (GameObject.FindWithTag("wall")} { velocity = 0; )

}