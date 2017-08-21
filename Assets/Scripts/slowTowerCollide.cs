using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class slowTowerCollide : MonoBehaviour {


    private Scene currentScene;
    private String currentSceneName;


	// Use this for initialization
	void Start () {
        currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collision) {

       
        if (currentSceneName == "level1" && collision.gameObject.tag=="crasher") {
            move speed = collision.GetComponent<move>();
            speed.changeSpeed(1.25f);
            try {
                speed.changeSpeed(1.25f);
            }
            catch (NullReferenceException e) { }
        }

        if (currentSceneName == "level2" && collision.gameObject.tag == "crasher") {
            moveTwo speed = collision.GetComponent<moveTwo>();
            speed.changeSpeed(1.25f);
            try {
                speed.changeSpeed(1.25f);
            }
            catch (NullReferenceException e) { }
        }

        if (currentSceneName == "level3" && collision.gameObject.tag == "crasher") {
            moveThree speed = collision.GetComponent<moveThree>();
            speed.changeSpeed(1.25f);
            try {
                speed.changeSpeed(1.25f);
            }
            catch (NullReferenceException e) { }
        }

       
    }

    private void OnTriggerExit(Collider other) {


        if (currentSceneName == "level1") {
            move speed = other.GetComponent<move>();
            speed.changeSpeed(2.0f);
            try {
                speed.changeSpeed(2.0f);
            }
            catch (NullReferenceException e) { }
        }


        if (currentSceneName == "level3") {
            moveThree speed = other.GetComponent<moveThree>();
            speed.changeSpeed(4.0f);
            try {
                speed.changeSpeed(4.0f);
            }
            catch (NullReferenceException e) { }
        }

    }

}
