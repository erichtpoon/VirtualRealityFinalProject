using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawingArrow : MonoBehaviour
{
    //steam controller
    public SteamVR_Controller.Device input;

    //Controllers for Bows
    public Transform notchRestPosition;
    public GameObject otherHand;
    public GameObject drawBow;
    public GameObject lookAtArrowPos;



    //Controllers for Arrows
    public GameObject arrow;
    private GameObject tempArrow;
    private GameObject createdArrow;

    //conditions for arrows
    private bool hasArrow = false;
    private bool arrowLocked = false;
    private bool arrowdrawing = false;
    private bool arrowAttached = false;

    //distance thresholds
    public float distanceToNockPosition;
    public float rotationLerpThreshold;
    public float lerpCompleteDistance;
    public float positiontoConnect;
    private float vectorSpeed = 35;

    //velocity calculation
    private Vector3 velo;

    //fixed joint
    private FixedJoint join;

    //time before the arrow disappears
    public float lifetime;

    //Haptic Thresholds
    public float firstThresh;
    public float secondThresh;
    public float thirdThresh;

    /*******************************
    *  Use this for initialization
    ********************************/
    void Start()
    {
        //initializes the steam controller
        var trackedObj = this.GetComponent<SteamVR_TrackedObject>();
        input = SteamVR_Controller.Input((int)trackedObj.index);

        //initializing joint
        join = this.GetComponent<FixedJoint>();

        //initializing conditions
        hasArrow = false;
        arrowLocked = false;
        arrowdrawing = false;
        arrowAttached = false;

        //initializing thresholds
        firstThresh = 0.5f;
        secondThresh = 0.75f;
        thirdThresh = 1f;

        //initializing destroy threshold
        lifetime = 5;

        //initializing distance thresholds
        rotationLerpThreshold = 0.15f;
        lerpCompleteDistance = 0.08f;
        positiontoConnect = 0.5f;
}

    /*******************************
    *  Update is called once per frame
    ********************************/
    void Update() 
    {
        if (input.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && arrowLocked == true)
        {
            //arrow is locked and drawing
            arrowdrawing = true;

            //hand no longer holds the arrow
            hasArrow = false;

            //assign arrow hands position to the arrow
            tempArrow.transform.position = this.transform.position;

            tempArrow.transform.parent = otherHand.transform;
            tempArrow.transform.eulerAngles = otherHand.transform.eulerAngles;
            tempArrow.transform.localPosition = new Vector3(0f, 0f, this.transform.localPosition.z - 0.5f);
            tempArrow.transform.LookAt(lookAtArrowPos.transform.position);
           
            //print(tempArrow.transform.position + " " + this.transform.position);
            

            float distanceDifference = calculateDistance(otherHand);

            //Haptics for small draw
            if (distanceDifference < firstThresh)
            {
                input.TriggerHapticPulse(1000);
            }
            else
            {
                //Haptics for medium draw
                if (distanceDifference < secondThresh)
                {
                    input.TriggerHapticPulse(2000);
                }
                else
                {
                    //Haptics for large draw
                    if (distanceDifference < thirdThresh)
                    {
                        input.TriggerHapticPulse(3000);
                    }
                    else
                    {
                        //if true, detach arrow and return to hand
                        if (detachArrow(distanceDifference))
                        {
                            //resets conditions
                            hasArrow = true;
                            arrowLocked = false;
                            arrowdrawing = false;
                            arrowAttached = false;
                            controllerModelVisibility(false);

                            tempArrow.transform.position = this.transform.position - new Vector3(this.transform.forward.x / (float)3.5, this.transform.forward.y / (float)3.5, this.transform.forward.z / (float)3.5);
                            tempArrow.transform.eulerAngles = this.transform.eulerAngles;
                            join.connectedBody = tempArrow.GetComponent<Rigidbody>();
                        }
                    }
                }
            }
            

            velo = drawBow.transform.position - tempArrow.transform.position;
            velo = velo * vectorSpeed;
        }
        else
        {
            //When the arrow has been released
            if (input.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && arrowdrawing == true)
            {
                //detach from local space
                tempArrow.transform.parent = null;
                //applied velocity and angularVelocity
                tempArrow.GetComponent<Rigidbody>().velocity = velo;
                tempArrow.GetComponent<Rigidbody>().angularVelocity = velo;
                //resets conditions
                hasArrow = false;
                arrowLocked = false;
                arrowdrawing = false;
                arrowAttached = false;
                controllerModelVisibility(true);
                Destroy(tempArrow, lifetime);
            }
        }

        if (hasArrow) {
            distanceToNockPosition = calculateDistance(otherHand);
            // If we're close enough to the nock position that we want to start arrow position lerp, do so
            if ((distanceToNockPosition < positiontoConnect) && input.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                //hasArrow = false;
                //print("distanceToNockPosition:" + distanceToNockPosition);
                //print("positionLerpThreshold:" + positiontoConnect);
                //print("ran");
                arrowAttached = true;
                this.join.connectedBody = null;
                tempArrow.transform.position = notchRestPosition.transform.position;
                tempArrow.transform.eulerAngles = notchRestPosition.transform.eulerAngles;
            }
            else if(arrowAttached)
            {
                tempArrow.transform.position = notchRestPosition.transform.position;
                tempArrow.transform.eulerAngles = notchRestPosition.transform.eulerAngles;
                if (input.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
                {
                    arrowLocked = true;
                }
            }
        }
    }

    /*******************************
    *  Calculates the distance betweeen bow and arrow
    ********************************/
    private float calculateDistance(GameObject otherHand)
    {
        float distanceToNockPosition;
        distanceToNockPosition = Vector3.Distance(this.transform.position, otherHand.transform.position);
        return distanceToNockPosition;
    }

    /*******************************
    * Calculated whether the threshold has been reached
    ********************************/
    private bool detachArrow(float distanceDifference) 
    {
        bool overThresh;
        if (thirdThresh  < distanceDifference)
        {
            overThresh = true ;
        }
        else
        {
            overThresh = false;
        }
        return overThresh;
    }

    /*******************************
    *  Triggers during collision with another object
    ********************************/
    private void OnTriggerStay(Collider collision) 
    {
        if (collision.gameObject.tag == "quiver" && input.GetHairTriggerDown() && hasArrow == false)
        {
            createdArrow = Instantiate(arrow);
            tempArrow = createdArrow;
            tempArrow.transform.position = this.transform.position - new Vector3(this.transform.forward.x / (float)3.5, this.transform.forward.y / (float)3.5, this.transform.forward.z / (float)3.5);
            tempArrow.transform.eulerAngles = this.transform.eulerAngles;
            join.connectedBody = tempArrow.GetComponent<Rigidbody>();

            //User is now holding arrow
            hasArrow = true;

            //Hides controller
            controllerModelVisibility(false);

            //haptic feedback for receiving arrow
            StartCoroutine(LongVibration(1, 2000));
        }
    }

    /*******************************
    *  hides the vive controller
    ********************************/
    void controllerModelVisibility(bool visible) 
    {
        foreach (SteamVR_RenderModel model in this.GetComponentsInChildren<SteamVR_RenderModel>())
        {
            foreach (var child in model.GetComponentsInChildren<MeshRenderer>())
            {
                child.enabled = visible;
            }
        }
    }

    /*******************************
    *  Lingering haptic feedback
    ********************************/
    IEnumerator LongVibration(float length, float strength) {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            input.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
            yield return null;
        }
    }
}
