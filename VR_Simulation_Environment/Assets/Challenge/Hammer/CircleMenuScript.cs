using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CircleMenuScript : MonoBehaviour {

    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject Canvas;

    bool showing;

    public void Enter(int type)
    {
        print("Callled " + type);
        if (type == 1)
        {
            player.SetHammer(true);
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Canvas.gameObject.SetActive(false);
        }
        Canvas.gameObject.SetActive(false);
        showing = false;
    }

    public void Show()
    {
        if (player.HasHammer())
            return;
        if (!showing)
        {
            Canvas.gameObject.SetActive(true);
            Quaternion rotation = Quaternion.LookRotation(transform.position - player.transform.position);
            Canvas.transform.rotation = rotation;
            showing = true;
        }
    }
	void Start () {
        Canvas.gameObject.SetActive(false);
    }
	
	void Update () {
    }
}
