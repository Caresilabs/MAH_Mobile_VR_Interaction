using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;
using Assets.Scripts.UI;

public class MazeChallenge : MonoBehaviour, IChallenge {

    [SerializeField]
    protected GameManager GameManager;

    protected Player Player;

    private static SelectMenuGeneral readyMenu;

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

        if (readyMenu == null)
            readyMenu = FindObjectOfType<SelectMenuGeneral>();

        readyMenu.gameObject.SetActive(false);
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
        GameManager.Analytics.OnEvent("Goal", "MazeTime", GameManager.Timer);
        GameManager.StopTimer();

        readyMenu.gameObject.SetActive(true);
        readyMenu.Callback.AddListener(delegate { OnUserReadyForNextClick(); });
    }

    public void OnUserReadyForNextClick()
    {
        GameManager.NextPhase();
    }

    //private void StandStill()
    //{
    //    if ( GameManager.CurrentChallenge == (IChallenge)this )
    //        Player.SetMovement(Player.MovementType.STILL);
    //}

   
}
