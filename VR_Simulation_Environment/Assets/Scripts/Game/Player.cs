using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{

    public const float MOVE_SPEED = 5;

    public MovementType MoveType { get; private set; }

    private GameObject camera;

    private Rigidbody rigidBody;

    [SerializeField]
    private GameObject Hammer;

    //PointMove
    private GameObject targetObj, queuedTargetObj;
    private float POINT_LOOK_TIME = 0.3f;

    private float currentTime;

    //Motion
    private float maxVelocityChange = 1.0f;

    //Hammer
    private bool hasHammer;

    public GameObject currentCanvas;

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
            case MovementType.RAYCAST_MOVEMENT: break;
            case MovementType.MOTION_MOVEMENT:
                UpdateTiltMovement();
                break;
            case MovementType.POINT_MOVEMENT:
                if (queuedTargetObj != null)
                {
                    currentTime += Time.deltaTime;
                    if (currentTime >= POINT_LOOK_TIME)
                    {
                        targetObj = queuedTargetObj;
                        queuedTargetObj = null;
                        currentTime = 0;
                    }
                }

                if (targetObj != null)
                {
                    rigidBody.MovePosition(Vector3.MoveTowards(rigidBody.position, targetObj.transform.position, Time.deltaTime * MOVE_SPEED)); //.08f

                    if (Vector3.Distance(rigidBody.position, targetObj.transform.position) < 0.5f)
                    {
                        currentTime = 0;
                        targetObj = null;
                    }
                }
                break;
            case MovementType.CONTROLLER_MOVEMENT: break;
        }
    }

    private void UpdateTiltMovement()
    {
        float x = camera.transform.rotation.eulerAngles.x;
        float upDown = ((x > 180) ? (x - 360f) : x) / 22.5f; // -4 straight up, 4 straight down, 0 straight forward.

        if (upDown < -0.5f)
            upDown *= 0.3f;

        if (Math.Abs(x) < 17)
        {
            if (x < 12)
                upDown = 0;
        }

        float z = camera.transform.rotation.eulerAngles.z;
        float tilt = ((z > 180) ? (z - 360f) : z) / 90f; // 1 tilt left, -1 tilt right, 0 no tilt.

        if (Math.Abs(z) < 15)
            tilt = 0;

        Vector3 targetVelocity = camera.transform.rotation * new Vector3(upDown, 0, tilt);

        if (targetVelocity.magnitude >= 1)
        {
            targetVelocity.Normalize();
        }
        targetVelocity = transform.TransformDirection(targetVelocity);

        targetVelocity *= MOVE_SPEED;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = rigidBody.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        rigidBody.AddForce(velocityChange, ForceMode.VelocityChange);
        
    }

    public void OnPointerEnter(GameObject targetObject, Vector3 intersectionPosition, Ray intersectionRay, bool isInteractive)
    {
        if (MoveType == MovementType.POINT_MOVEMENT && isInteractive && targetObject.tag == "RaycastFloor")
        {
            currentTime = 0;
            queuedTargetObj = targetObject;
        }
    }

    public void OnPointerHover(GameObject targetObject, Vector3 intersectionPosition, Ray intersectionRay, bool isInteractive)
    {
        if (MoveType == MovementType.RAYCAST_MOVEMENT)
            GetComponent<RayCastMovement>().OnPointerHover(targetObject, intersectionPosition, intersectionRay, isInteractive);

        if (MoveType == MovementType.POINT_MOVEMENT && isInteractive && targetObject.tag == "RaycastFloor")
        {
            //if (targetObj == null)
             //   targetObj = targetObject;
        }
    }

    public void OnPointerExit(GameObject targetObject)
    {
        if (MoveType == MovementType.POINT_MOVEMENT)
        {
            queuedTargetObj = null;
        }
    }

    public void SetMovement(MovementType type)
    {
        MoveType = type;
        GetComponent<RayCastMovement>().SetEnabled(type == MovementType.RAYCAST_MOVEMENT);
        targetObj = null;
        queuedTargetObj = null;
    }

    public void ShowDot(bool on)
    {
        GetComponentInChildren<GvrReticlePointer>().toggleDot(on);
    }

    public void SetHammer(bool hasHammer)
    {
        this.hasHammer = hasHammer;
        Hammer.SetActive(hasHammer);
    }


    public bool HasHammer() { return hasHammer; }
}
