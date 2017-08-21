using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class choosing : MonoBehaviour {

    // contDevice is a public field to allow access in the update loop.
    public SteamVR_Controller.Device contDevice;
    public GameObject VRCamera;
    public GameObject scoreScreen;
    public Text textScreen;

    private LineRenderer pointerLine;
    Vector3[] positions = new Vector3[2];
    private float laserRange = 30.0f;
    private RaycastHit hit;
    public Material choose;
    private bool enable = false;
    public Material defaultMat;
    private MeshRenderer other;
    private Vector3 indicatorLocation;
    private GameObject telelocation;
    public GameObject tower1;
    public GameObject tower2;
    private GameObject tower;
    private bool placedTower = false;
    private int towerCount = 0;

    private float textTimeLeft;


    private static int waveText;

    private Scene currentScene;
    private String currentSceneName;

    /*******************************
     *  Use this for initialization
     ********************************/
    void Start() 
        {
        //In order to poll the controller device, we need the device index. We can get this from the SteamVR_TrackedObj.
        var TrackedObj = GetComponent<SteamVR_TrackedObject>();
        contDevice = SteamVR_Controller.Input((int)TrackedObj.index);
        pointerLine = this.GetComponent<LineRenderer>();
        pointerLine.enabled = false;

        waveText = MobSpawn.waveNumber;
        textTimeLeft = 2;

        currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;
    }

    /*******************************
    *  Update is called once per frame
    ********************************/
    void Update() {
        // down is true only on the frame in which the button is first pressed.
        // up is true only on the frame in which the button is released.

        bool crossDown = contDevice.GetPress(1ul << 8);
        bool crossUp = contDevice.GetPressUp(1ul << 8);

        bool sqDown = contDevice.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip);
        bool sqUp = contDevice.GetPressUp(Valve.VR.EVRButtonId.k_EButton_Grip);

        bool triDown = contDevice.GetPressDown(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
        bool triUp = contDevice.GetPressUp(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);

        bool circDown = contDevice.GetPressDown(Valve.VR.EVRButtonId.k_EButton_A);
        bool circUp = contDevice.GetPressUp(Valve.VR.EVRButtonId.k_EButton_A);


        if (contDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[1] < -0.7f)
        {
            if (contDevice.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Axis0))
            {
                if (scoreScreen.activeSelf)
                {
                    scoreScreen.SetActive(false);
                }
                else
                {
                    if (currentSceneName != "level3") {
                        if (MobSpawn.intermissionon == true) {
                            textScreen.text = "Score: " + MobSpawn.score + "\nLifes: " + MobSpawn.lives + "\nWave: Intermission";
                        }
                        else if (MobSpawn.waveNumber == 4) {
                            textScreen.text = "Score: " + MobSpawn.score + "\nLifes: " + MobSpawn.lives + "\nWave: Complete";
                        }
                        else {
                            textScreen.text = "Score: " + MobSpawn.score + "\nLifes: " + MobSpawn.lives + "\nWave:" + MobSpawn.waveNumber;
                        }
                        scoreScreen.SetActive(true);
                        textTimeLeft = 2;
                    }
                    else {
                        if (MobSpawnGamma.intermissionon == true) {
                            textScreen.text = "Score: " + MobSpawnGamma.score + "\nLifes: " + MobSpawnGamma.lives + "\nWave: Intermission";
                        }
                        else if (MobSpawnGamma.waveNumber == 4) {
                            textScreen.text = "Score: " + MobSpawnGamma.score + "\nLifes: " + MobSpawnGamma.lives + "\nWave: Complete";
                        }
                        else {
                            textScreen.text = "Score: " + MobSpawnGamma.score + "\nLifes: " + MobSpawnGamma.lives + "\nWave:" + MobSpawnGamma.waveNumber;
                        }
                        scoreScreen.SetActive(true);
                        textTimeLeft = 2;
                    }
                }
            }

        }

        textTimeLeft = textTimeLeft - Time.deltaTime;
        if (textTimeLeft < 0)
        {
            scoreScreen.SetActive(false);
            textTimeLeft = 2;
        }


        if (contDevice.GetPress(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu) || contDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[0] > 0.8f || contDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[0] < -0.8f) 
            {
            Vector3 rayOrigin = this.transform.position + this.transform.forward * laserRange;
            positions[0] = this.transform.position;
            positions[1] = rayOrigin;
            pointerLine.SetPositions(positions);
            pointerLine.enabled = true;
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, laserRange)) 
            {
                Vector3[] rayCastVector = new Vector3[2];
                rayCastVector[0] = this.transform.position;
                rayCastVector[1] = hit.point;
                pointerLine.SetPositions(rayCastVector);
                if (other != null) 
                {
                    other.material = defaultMat;
                }
                if (contDevice.GetPress(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu) && (hit.collider.tag == "end" || hit.collider.tag == "tower")) 
                {
                    other = hit.collider.GetComponent<MeshRenderer>();
                    var material = other.material;
                    material = choose;
                    other.material = material;
                    telelocation = hit.collider.gameObject;
                }

                if ((contDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[0] > 0.6f || contDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[0] < -0.6f) && hit.collider.tag == "floor" && towerCount <= 2) 
                {
                    if (contDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[0] > 0.6f && contDevice.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad)) 
                    {
                        //if (placedTower == false) 
                        //{
                        tower = Instantiate(tower1);
                        tower.SetActive(true);
                        towerCount += 1;
                        //}
                        tower.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
                    }
                    else if (contDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[0] < -0.6f && contDevice.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad)) 
                    {
                            //if (placedTower == false)
                            //{
                        tower = Instantiate(tower2);
                        tower.SetActive(true);
                        towerCount += 1;
                        //}
                        tower.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
                    }

                    placedTower = true;
                }
                
            }
            else 
            {
                Vector3[] rayCastVector2 = new Vector3[2];
                rayCastVector2[0] = this.transform.position;
                rayCastVector2[1] = ((this.transform.position + this.transform.forward * laserRange) + (this.transform.forward * laserRange));
                pointerLine.SetPositions(rayCastVector2);
                if (other != null) 
                {
                    other.material = defaultMat;
                }
                    
            }
        }
        else if (triUp) 
        {
            pointerLine.enabled = false;
            try
            {
                other.material = defaultMat;
                if (hit.collider.tag == "end" || hit.collider.tag == "tower")
                {
                    SteamVR_Fade.View(Color.black, 1);
                    indicatorLocation = telelocation.transform.position;
                    Invoke("screenFade", 2);
                }
            }
            catch(NullReferenceException e)
            {
                
            }

        }
        else if(contDevice.GetTouchUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad)) 
        {
            pointerLine.enabled = false;
            placedTower = false;
        }

        if (contDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[0] > 0.8f && contDevice.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad)) 
        {
            placedTower = false;
        }
        
        if (contDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[0] < -0.8f && contDevice.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad)) 
        {
            placedTower = false;
        }

        //When grip is pressed down
        if (contDevice.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip)) 
        {
            SceneManager.LoadScene("menu", LoadSceneMode.Single);
        }

    }

    void screenFade() 
        {
        VRCamera.transform.position = indicatorLocation;
        VRCamera.transform.position += new Vector3(0,(float)5.5,0);
        VRCamera.transform.LookAt(new Vector3(0, VRCamera.transform.position.y,0));
        SteamVR_Fade.View(Color.clear, 1);
    }

}