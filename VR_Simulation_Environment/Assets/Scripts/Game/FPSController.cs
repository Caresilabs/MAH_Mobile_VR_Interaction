using UnityEngine;
using System.Collections;
using System;

public class FPSController : MonoBehaviour
{

    public float Sensitivity { get; set; }

    public Rigidbody Rigidbody { get; private set; }

    public Player Player { get; private set; }

    private float maxVelocityChange = 1.0f;

    private float xView, yView;

    private Camera cam;

    private int invertX = 1, invertY = 1;

    // Use this for initialization
    void Start()
    {
        this.Rigidbody = GetComponent<Rigidbody>();
        this.cam = GetComponentInChildren<Camera>();
        this.Player = GetComponent<Player>();
        this.Sensitivity = 110;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateLook();
        UpdateSensitivity();
    }

    private void UpdateSensitivity()
    {
        if (Input.GetButtonDown("SensitivityPlus"))
        {
            Sensitivity = Mathf.Clamp(Sensitivity + 10, 1, 9999);
        }
        else if (Input.GetButtonDown("SensitivityMinus"))
        {
            Sensitivity = Mathf.Clamp(Sensitivity - 10, 1, 9999);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            invertX *= -1;
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            invertY *= -1;
        }
    }

    private void UpdateMovement()
    {
        if (Player.MoveType != Player.MovementType.CONTROLLER_MOVEMENT)
            return;

        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (targetVelocity.magnitude >= 1)
        {
            targetVelocity.Normalize();
        }
        targetVelocity = transform.TransformDirection(targetVelocity);

        targetVelocity *= Player.MOVE_SPEED;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = Rigidbody.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        Rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    private void UpdateLook()
    {
        float yRot = Input.GetAxis("Mouse Y") * -Sensitivity * invertY * Time.deltaTime;
        float xRot = Input.GetAxis("Mouse X") * Sensitivity * invertX * Time.deltaTime;

        xView += xRot;
        transform.Rotate(Vector3.up, xRot);

        //camera.transform.parent.localRotation = Quaternion.Lerp(camera.transform.localRotation,
        //    Quaternion.Euler(camera.transform.parent.eulerAngles.x, 0, 0), Time.deltaTime * 2);

        if (yView + yRot > -89 && yView + yRot < 89)
        {
            yView += yRot;

            cam.transform.parent.Rotate(yRot, 0, 0);
        }
    }
}
