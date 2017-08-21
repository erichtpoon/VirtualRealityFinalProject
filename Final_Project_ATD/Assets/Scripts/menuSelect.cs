using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuSelect : MonoBehaviour {
    public SteamVR_Controller.Device contDevice;
    public GameObject VRCamera;
    private LineRenderer pointerLine;
    Vector3[] positions = new Vector3[2];
    private float laserRange = 30.0f;
    private RaycastHit hit;
    public Material choose;
    public Material defaultMat;
    private MeshRenderer other;


    // Use this for initialization
    void Start() {
        //In order to poll the controller device, we need the device index. We can get this from the SteamVR_TrackedObj.
        var TrackedObj = GetComponent<SteamVR_TrackedObject>();
        contDevice = SteamVR_Controller.Input((int)TrackedObj.index);
        pointerLine = this.GetComponent<LineRenderer>();
        pointerLine.enabled = false;
    }

    // Update is called once per frame
    void Update() 
        {
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

        if (contDevice.GetPress(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu)) {
            Vector3 rayOrigin = this.transform.position + this.transform.forward * laserRange;
            positions[0] = this.transform.position;
            positions[1] = rayOrigin;
            pointerLine.SetPositions(positions);
            pointerLine.enabled = true;
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, laserRange)) {
                Vector3[] rayCastVector = new Vector3[2];
                rayCastVector[0] = this.transform.position;
                rayCastVector[1] = hit.point;
                pointerLine.SetPositions(rayCastVector);
                if (other != null) {
                    other.material = defaultMat;
                }
                if (hit.collider.tag == "menuObject1" || hit.collider.tag == "menuObject2" || hit.collider.tag == "menuObject3") {
                    other = hit.collider.GetComponent<MeshRenderer>();
                    var material = other.material;
                    material = choose;
                    other.material = material;

                }

            }
            else {
                Vector3[] rayCastVector2 = new Vector3[2];
                rayCastVector2[0] = this.transform.position;
                rayCastVector2[1] = ((this.transform.position + this.transform.forward * laserRange) + (this.transform.forward * laserRange));
                pointerLine.SetPositions(rayCastVector2);
                if (other != null) {
                    other.material = defaultMat;
                    
                }
            }
        }
        else if (triUp) {
            try {
                pointerLine.enabled = false;
                other.material = defaultMat;
                switch (hit.collider.tag) {
                    case "menuObject1":
                        SceneManager.LoadScene("level1", LoadSceneMode.Single);
                        break;
                    case "menuObject2":
                        SceneManager.LoadScene("level2", LoadSceneMode.Single);
                        break;
                    case "menuObject3":
                        SceneManager.LoadScene("level3", LoadSceneMode.Single);
                        break;
                }
            }
            catch (NullReferenceException e) { }
        }
    }
}
