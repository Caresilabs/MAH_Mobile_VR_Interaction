using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{

    public const float MOVE_SPEED = 6;

    public MovementType MoveType { get; private set; }

    private GameObject camera;

    private Rigidbody rigidBody;

    [SerializeField]
    private GameObject Hammer;

    //PointMove
    private GameObject targetObj;
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

    private void UpdateTiltMovement()
    {
        float x = camera.transform.rotation.eulerAngles.x;
        float upDown = ((x > 180) ? (x - 360f) : x) / 22.5f; // -4 straight up, 4 straight down, 0 straight forward.

        if (Math.Abs(x) < 15)
            upDown = 0;

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

        targetVelocity *= Player.MOVE_SPEED;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = rigidBody.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        rigidBody.AddForce(velocityChange, ForceMode.VelocityChange);

        print(tilt);
    }

    public void OnPointerEnter(GameObject targetObject, Vector3 intersectionPosition, Ray intersectionRay, bool isInteractive)
    {
        if (MoveType == MovementType.POINT_MOVEMENT && isInteractive && targetObject.tag == "RaycastFloor")
        {
            targetObj = targetObject;
            currentTime = 0;
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
        GetComponent<RayCastMovement>().SetEnabled(type == MovementType.RAYCAST_MOVEMENT);
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
