using UnityEngine;
using System.Collections;

public class GlobalManager : MonoBehaviour {

    public static bool ControllerBuild { get; private set; }

    void Awake () {
        DontDestroyOnLoad(this);

        ControllerBuild = Application.platform != RuntimePlatform.Android;
        ControllerBuild = true; // TODO DEBUG ONLY

       
    }

    void Start()
    {
        GvrViewer.Instance.VRModeEnabled = !ControllerBuild;
    }
	
	void Update () {
	}

}
