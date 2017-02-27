using UnityEngine;
using System.Collections;

public class BoxScript : MonoBehaviour {

    [SerializeField]
    GameObject Top;

    Quaternion start;

    bool closing;
	// Use this for initialization
	void Start () {
        start = Top.transform.rotation * Quaternion.AngleAxis(90, new Vector3(0, 0, 1));
    }
	
	// Update is called once per frame
	void Update () {
	    if(closing)
        {
            print("Closing");
            Top.transform.rotation = Quaternion.Slerp(Top.transform.rotation, start, 0.01f);
        }
	}

    void Close()
    {
        closing = true;
    }
}
