using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class MazeChallenge : MonoBehaviour, IChallenge {

    [SerializeField]
    protected Player    Player; 

    public void StartChallenge()
    {
        print("Start Challenge");
        Player.SetMovement(Player.MOVEMENT_TYPE.POINT_MOVEMENT); //change me later
    }

    public void StopChallenge()
    {

    }

    void Start () {
	    
	}
	
	void Update () {
	
	}
}
