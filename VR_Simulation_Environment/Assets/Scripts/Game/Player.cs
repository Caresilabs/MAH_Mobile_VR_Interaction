using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    private bool            controllerEnabled;
    private GameObject      camera;
    private MOVEMENT_TYPE   moveType;
    private Rigidbody       rigidBody;

    //PointMove
    private GameObject      targetObj;
    private float           POINT_LOOK_TIME = 0.3f;

    private float           currentTime;

    public enum MOVEMENT_TYPE
    {
        STILL,
        MOTION_MOVEMENT,
        POINT_MOVEMENT,
        RAYCAST_MOVEMENT,
        CONTROLLER_MOVEMENT
    }

	void Start () {
        camera = GetComponentInChildren<Camera>().gameObject;
        rigidBody = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        UpdateController();

        UpdateMovement();

    }

    private void UpdateController()
    {
        if (controllerEnabled)
        {
            //update camera by controller here and add senecetivity
            if (Input.anyKey)
            {

            }
            //
        }
    }

    private void UpdateMovement()
    {
        switch(moveType)
        {
            case MOVEMENT_TYPE.STILL: break;
            case MOVEMENT_TYPE.RAYCAST_MOVEMENT: break;
            case MOVEMENT_TYPE.MOTION_MOVEMENT: break;
            case MOVEMENT_TYPE.POINT_MOVEMENT:
                if(targetObj != null)
                {
                    currentTime += Time.deltaTime;
                    if(currentTime >= POINT_LOOK_TIME)
                    {
                        rigidBody.MovePosition(targetObj.transform.position);
                        currentTime = 0;
                        targetObj = null;
                    }
                }
                break;
            case MOVEMENT_TYPE.CONTROLLER_MOVEMENT: break;
        }
    }

    public void OnPointerEnter(GameObject targetObject, Vector3 intersectionPosition,Ray intersectionRay, bool isInteractive)
    {
        if(moveType == MOVEMENT_TYPE.POINT_MOVEMENT)
        {
            targetObj = targetObject;
        }
    }

    public void OnPointerHover(GameObject targetObject, Vector3 intersectionPosition, Ray intersectionRay, bool isInteractive)
    {

    }

    public void OnPointerExit(GameObject targetObject)
    {
        if (moveType == MOVEMENT_TYPE.POINT_MOVEMENT)
        {
            targetObj = null;
        }
    }

    public void SetMovement(MOVEMENT_TYPE type)
    {
        moveType = type;
    }

    public void SetVrMode()
    {
        GvrViewer.Instance.VRModeEnabled = true;
        controllerEnabled = false;
    }

    public void SetControllerMode()
    {
        GvrViewer.Instance.VRModeEnabled = false;
        controllerEnabled = true;
    }
}
