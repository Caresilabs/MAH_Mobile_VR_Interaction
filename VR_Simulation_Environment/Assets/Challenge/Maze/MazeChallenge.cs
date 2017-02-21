using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class MazeChallenge : MonoBehaviour, IChallenge {

    [SerializeField]
    protected GameManager GameManager;

    protected Player Player;

    public virtual void StartChallenge()
    {
        this.Player = GameManager.Player;

        print("Start Challenge");

        Player.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;

        var startEnd = FindObjectsOfType<StartEndChecker>();
        foreach (var checker in startEnd)
        {
            checker.Reset(this);
        }
    }

    public virtual void StopChallenge()
    {

    }

    void Start () {
	    
	}
	
	void Update () {
	
	}

    public void OnStartEnter()
    {
        GameManager.StartTimer();
    }

    public void OnGoalEnter()
    {
        GameManager.StopTimer();

        GameManager.Analytics.OnEvent("Goal", "MazeTime", GameManager.Timer);

       // Invoke("StandStill", 3); TODO after end button is there

        GameManager.NextPhase(); // TODO replace with button
    }

    private void StandStill()
    {
        Player.SetMovement(Player.MovementType.STILL);
    }

   
}
