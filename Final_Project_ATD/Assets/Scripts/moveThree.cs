using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveThree : MonoBehaviour {

   
    public GameObject thisObject;

    //O path
    public Transform startOLocation;
    public Transform targetOFirst;
    public Transform targetOSecond;
    public Transform targetOThird;
    private bool Opath;

    //P path
    public Transform startPLocation;
    public Transform targetPFirst;
    public Transform targetPSecond;
    public Transform targetPThird;
    private bool Ppath;
    
    private float whatIsEnd;
    private int integerwhatIsEnd;


    private bool wayPointOnePassed;
    private bool wayPointTwoPassed;

    public float stepValue = 8.0f;
    // Use this for initialization
    void Start() {
        whatIsEnd = Random.Range(1.0f, 2.99f);
        integerwhatIsEnd = (int)whatIsEnd;

        wayPointOnePassed = false;
        wayPointTwoPassed = false;
        Opath = false;
        Ppath = false;

        if (thisObject.transform.position == startPLocation.position) {
            Ppath = true;
        }
        else if (thisObject.transform.position == startOLocation.position) {
            Opath = true;
        }
    }

    // Update is called once per frame
    void Update() {
        float step = stepValue * Time.deltaTime;


        if (Opath) {

            //No point passed
            if (!wayPointOnePassed) {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetOFirst.transform.position.x, targetOFirst.transform.position.y, targetOFirst.transform.position.z), step);
                if (this.transform.position == targetOFirst.transform.position) {
                    wayPointOnePassed = true;
                }
            }
            //First point passed
            else if (wayPointOnePassed == true && wayPointTwoPassed == false) {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetOSecond.transform.position.x, targetOSecond.transform.position.y, targetOSecond.transform.position.z), step);
                if (this.transform.position == targetOSecond.transform.position) {
                    wayPointOnePassed = true;
                    wayPointTwoPassed = true;
                }
            }
            //both points passed
            else if (wayPointOnePassed == true && wayPointTwoPassed == true) {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetOThird.transform.position.x, targetOThird.transform.position.y, targetOThird.transform.position.z), step);
                if (this.transform.position == targetOThird.transform.position) {
                    Destroy(gameObject);
                    Destroy(thisObject);
                    MobSpawnGamma.lives--;
                }
            }
        }
        else if (Ppath) {
            //No point passed
            if (!wayPointOnePassed) {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPFirst.transform.position.x, targetPFirst.transform.position.y, targetPFirst.transform.position.z), step);
                if (this.transform.position == targetPFirst.transform.position) {
                    wayPointOnePassed = true;
                }
            }
            //First point passed
            else if (wayPointOnePassed == true && wayPointTwoPassed == false) {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPSecond.transform.position.x, targetPSecond.transform.position.y, targetPSecond.transform.position.z), step);
                if (this.transform.position == targetPSecond.transform.position) {
                    wayPointOnePassed = true;
                    wayPointTwoPassed = true;
                }
            }
            //both points passed
            else if (wayPointOnePassed == true && wayPointTwoPassed == true) {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPThird.transform.position.x, targetPThird.transform.position.y, targetPThird.transform.position.z), step);
                if (this.transform.position == targetPThird.transform.position) {
                    Destroy(gameObject);
                    Destroy(thisObject);
                    MobSpawnGamma.lives--;
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