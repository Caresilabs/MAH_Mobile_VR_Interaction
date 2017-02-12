using UnityEngine;
using System.Collections;

public class GlobalManager : MonoBehaviour {

    public static bool ControllerBuild { get; private set; }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);

        ControllerBuild = Application.platform != RuntimePlatform.Android;

        ControllerBuild = true; // TODO DEBUG ONLY
    }
	
	void Update () {
	
	}
}
