using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private bool            controllerEnabled;
    private GameObject      camera;

	void Start () {
        camera = GetComponentInChildren<Camera>().gameObject;
        SetVrMode();
	}
	
	void Update () {
        if(controllerEnabled)
        {
            //update camera by controller here
            //test
            if (Input.anyKey)
            {
                transform.Rotate(camera.transform.up, 0.2f);
            }
            //
        }
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
