using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{

    public const float MOVE_SPEED = 6;

    public MovementType MoveType { get; private set; }

    private GameObject camera;

    private Rigidbody rigidBody;

    //PointMove
    private GameObject targetObj;
    private float POINT_LOOK_TIME = 0.3f;

    private float currentTime;

    //Motion
    private float maxVelocityChange = 1.0f;

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
                float x = camera.transform.rotation.eulerAngles.x;
                float upDown = ((x > 180) ? (x-360f) : x) / 45f; // -2 straight up, 2 straight down, 0 straight forward.

                float z = camera.transform.rotation.eulerAngles.z;
                float tilt = ((z > 180) ? -(z - 360f) : -z) / 90f; // 1 tilt left, -1 tilt right, 0 no tilt.

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

    public void OnPointerEnter(GameObject targetObject, Vector3 intersectionPosition, Ray intersectionRay, bool isInteractive)
    {
        if (MoveType == MovementType.POINT_MOVEMENT && isInteractive)
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
