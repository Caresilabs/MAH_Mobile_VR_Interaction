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

        int color = 0;
        foreach(GameObject g in spawnBoxPosition)
        {
            GameObject go = (GameObject)Instantiate(boxObject, g.transform.position, g.transform.rotation);
            go.GetComponentInChildren<BoxMenu>().SetColor(GetColor(color));
            spawnedObjects.Add(go);
            color++;
        }

        color = 0;
        foreach (GameObject g in spawnHammerPosition)
        {
            GameObject go = (GameObject)Instantiate(hammerObject, g.transform.position, g.transform.rotation);
            go.GetComponentInChildren<HammerMenu>().SetColor(GetColor(color));
            spawnedObjects.Add(go);
            color++;
        }
    }

    private BaseColorMenu.ColorType GetColor(int i)
    {
        switch(i)
        {
            case 0: return BaseColorMenu.ColorType.YELLOW;
            case 1: return BaseColorMenu.ColorType.RED;
            case 2: return BaseColorMenu.ColorType.GREEN;
            case 3: return BaseColorMenu.ColorType.RED;
            case 4: return BaseColorMenu.ColorType.BLUE;
        }
        return BaseColorMenu.ColorType.RED;
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
