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

    private List<GameObject> spawnedObjects = new List<GameObject>();

    protected Player Player;

    public void StartChallenge()
    {
        Player = GameManager.Player;
        Player.SetMovement(Player.MovementType.STILL);

        foreach(GameObject g in spawnBoxPosition)
        {
            spawnedObjects.Add((GameObject)Instantiate(boxObject, g.transform.position, g.transform.rotation));
        }

        foreach (GameObject g in spawnHammerPosition)
        {
            spawnedObjects.Add((GameObject)Instantiate(hammerObject, g.transform.position, g.transform.rotation));
        }
    }

    public void StopChallenge()
    {
        foreach (GameObject g in spawnedObjects)
        {
            Destroy(g);
        }
    }

	void Update () {
	
	}
}
