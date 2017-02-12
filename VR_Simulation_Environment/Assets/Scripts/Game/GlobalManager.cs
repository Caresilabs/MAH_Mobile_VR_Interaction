using UnityEngine;
using System.Collections;

public class GlobalManager : MonoBehaviour {

    public static bool ControllerBuild { get; private set; }

    void Start () {
        DontDestroyOnLoad(this);

        ControllerBuild = Application.platform != RuntimePlatform.Android;
        ControllerBuild = true; // TODO DEBUG ONLY

        GvrViewer.Instance.VRModeEnabled = !ControllerBuild;
    }
	
	void Update () {
	}

}
