using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class HammerChallenge : MonoBehaviour, IChallenge {

    [SerializeField]
    protected GameManager GameManager;

    protected Player Player;

    public void StartChallenge()
    {
        Player = GameManager.Player;
        Player.SetMovement(Player.MovementType.STILL);
    }

    public void StopChallenge()
    {

    }

	void Update () {
	
	}
}
