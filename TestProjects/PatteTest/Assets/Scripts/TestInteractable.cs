using UnityEngine;
using System.Collections;
using System;

public class TestInteractable : MonoBehaviour, IGvrGazeResponder
{

    bool selected;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (selected)
            gameObject.transform.RotateAround(transform.position, transform.up, 15);
	}

    public void OnGazeEnter()
    {
        selected = true;
        Console.WriteLine("OnGazeEnter()");
    }

    public void OnGazeExit()
    {
        selected = false;
        Console.WriteLine("OnGazeExit()");
    }

    public void OnGazeTrigger()
    {
        Console.WriteLine("OnGazeTrigger()");
    }
}
