using UnityEngine;
using System.Collections;

public class BoxScript : MonoBehaviour {

    [SerializeField]
    GameObject Top;

    [SerializeField]
    GameObject HammerInstance;

    Quaternion start;

    bool closing;

	void Start () {
        start = Top.transform.rotation * Quaternion.AngleAxis(90, new Vector3(0, 0, 1));
    }
	
	void Update () {
	    if(closing){
            Top.transform.rotation = Quaternion.Slerp(Top.transform.rotation, start, 0.015f);
        }
	}

    public void Close(){
        if(!closing){
            closing = true;
            Instantiate(HammerInstance, Top.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
}
