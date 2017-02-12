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
        //Player.SetMovement(Player.MovementType.POINT_MOVEMENT); //change me later move to subclass
        Player.SetMovement(Player.MovementType.RAYCAST_MOVEMENT);
    }

    public void StopChallenge()
    {

    }

    void Start () {
	    
	}
	
	void Update () {
	
	}
}
