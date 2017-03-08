using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;
using System.Collections.Generic;

public class HammerChallenge : MonoBehaviour, IChallenge {

    [SerializeField]
    protected GameManager GameManager;

    [SerializeField]
    private GameObject boxObject;

    [SerializeField]
    private List<GameObject> spawnBoxPosition = new List<GameObject>();

    [SerializeField]
    private GameObject hammerObject;

    [SerializeField]
    private List<GameObject> spawnHammerPosition = new List<GameObject>();

    protected Player Player;

    public void StartChallenge()
    {
        print("WAW" + spawnBoxPosition.Count);
        Player = GameManager.Player;
        Player.SetMovement(Player.MovementType.STILL);

        foreach(GameObject g in spawnBoxPosition)
        {
            Instantiate(boxObject, g.transform.position, g.transform.rotation);
            print("SPAWN BOX");
        }

        foreach (GameObject g in spawnHammerPosition)
        {
            Instantiate(hammerObject, g.transform.position, g.transform.rotation);
        }
    }

    public void StopChallenge()
    {

    }

	void Update () {
	
	}
}
