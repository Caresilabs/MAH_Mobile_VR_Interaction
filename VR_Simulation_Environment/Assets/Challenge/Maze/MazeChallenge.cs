using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class MazeChallenge : MonoBehaviour, IChallenge {

    [SerializeField]
    protected Player    Player; 

    public virtual void StartChallenge()
    {
        print("Start Challenge");
    }

    public virtual void StopChallenge()
    {

    }

    void Start () {
	    
	}
	
	void Update () {
	
	}
}
