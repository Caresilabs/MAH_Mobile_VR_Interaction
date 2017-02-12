using UnityEngine;
using System.Collections;

public class FPSController : MonoBehaviour
{

    public float Sensitivity { get; set; }

    public Rigidbody Rigidbody { get; private set; }

    private float maxVelocityChange = 1.0f;

    private float xView, yView;

    private Camera camera;

    // Use this for initialization
    void Start()
    {
        this.Rigidbody = GetComponent<Rigidbody>();
        this.camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateLook();
    }

    private void UpdateMovement()
    {
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (targetVelocity.magnitude >= 1)
        {
            targetVelocity.Normalize();
        }

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
        float yRot = Input.GetAxis("MouseY") * 180 * Time.deltaTime;
        float xRot = Input.GetAxis("MouseX") * 180 * Time.deltaTime;

        xView += xRot;
        transform.Rotate(Vector3.up, xRot);

        camera.transform.localRotation = Quaternion.Lerp(camera.transform.localRotation,
            Quaternion.Euler(camera.transform.eulerAngles.x, 0, 0), Time.deltaTime * 2);

        if (yView + yRot > -89 && yView + yRot < 89)
        {
            yView += yRot;

            camera.transform.Rotate(yRot, 0, 0);
        }
    }
}
