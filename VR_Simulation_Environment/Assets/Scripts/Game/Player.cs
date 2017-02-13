using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{

    public const float MOVE_SPEED = 3;

    public MovementType MoveType { get; private set; }

    private GameObject camera;

    private Rigidbody rigidBody;

    //PointMove
    private GameObject targetObj;
    private float POINT_LOOK_TIME = 0.3f;

    private float currentTime;

    public enum MovementType
    {
        STILL,
        MOTION_MOVEMENT,
        POINT_MOVEMENT,
        RAYCAST_MOVEMENT,
        CONTROLLER_MOVEMENT
    }

    void Start()
    {
        camera = GetComponentInChildren<Camera>().gameObject;
        rigidBody = GetComponent<Rigidbody>();

        if (!GlobalManager.ControllerBuild)
        {
            GetComponent<FPSController>().enabled = false;
        }
    }

    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        switch (MoveType)
        {
            case MovementType.STILL: break;
            case MovementType.RAYCAST_MOVEMENT:
                break;
            case MovementType.MOTION_MOVEMENT: break;
            case MovementType.POINT_MOVEMENT:
                if (targetObj != null)
                {
                    currentTime += Time.deltaTime;
                    if (currentTime >= POINT_LOOK_TIME)
                    {
                        rigidBody.MovePosition(targetObj.transform.position);
                        currentTime = 0;
                        targetObj = null;
                    }
                }
                break;
            case MovementType.CONTROLLER_MOVEMENT: break;
        }
    }

    public void OnPointerEnter(GameObject targetObject, Vector3 intersectionPosition, Ray intersectionRay, bool isInteractive)
    {
        if (MoveType == MovementType.POINT_MOVEMENT)
        {
            targetObj = targetObject;
        }
    }

    public void OnPointerHover(GameObject targetObject, Vector3 intersectionPosition, Ray intersectionRay, bool isInteractive)
    {
        if (MoveType == MovementType.RAYCAST_MOVEMENT)
            GetComponent<RayCastMovement>().OnPointerHover(targetObject, intersectionPosition, intersectionRay, isInteractive);
    }

    public void OnPointerExit(GameObject targetObject)
    {
        if (MoveType == MovementType.POINT_MOVEMENT)
        {
            targetObj = null;
        }
    }

    public void SetMovement(MovementType type)
    {
        MoveType = type;
        GetComponent<RayCastMovement>().enabled = (type == MovementType.RAYCAST_MOVEMENT);
    }

    public void ShowDot(bool on)
    {
        GetComponentInChildren<GvrReticlePointer>().toggleDot(on);
    }
}
