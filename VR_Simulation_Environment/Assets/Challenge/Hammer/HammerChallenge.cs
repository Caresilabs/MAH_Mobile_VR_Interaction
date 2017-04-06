using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;
using System.Collections.Generic;
using Assets.Scripts.UI;
using Assets.Challenge.Hammer.New;

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

    private static SelectMenuGeneral readyMenu;

    protected Player Player;

    private bool started;
    private bool isStopped;

    public void StartChallenge()
    {
        Player = GameManager.Player;
        Player.SetMovement(Player.MovementType.STILL);

        int color = 0;
        foreach(GameObject g in spawnBoxPosition)
        {
            GameObject go = (GameObject)Instantiate(boxObject, g.transform.position, g.transform.rotation);
            go.GetComponentInChildren<BaseColorMenu>().SetColor(GetColor(color));
            spawnedObjects.Add(go);
            color++;
        }

        color = 0;
        foreach (GameObject g in spawnHammerPosition)
        {
            GameObject go = (GameObject)Instantiate(hammerObject, g.transform.position, g.transform.rotation);
            go.GetComponentInChildren<BaseColorMenu>().SetColor(GetColor(color));
            spawnedObjects.Add(go);
            color++;
        }

        if (readyMenu == null)
            readyMenu = FindObjectOfType<SelectMenuGeneral>();

        readyMenu.Callback.RemoveAllListeners();
        readyMenu.gameObject.SetActive(false);

        isStopped = false;
        started = false;
    }

    public void OnHammerStart() {
        if (!started) {
            GameManager.StartTimer();
            started = true;
            print("TIMER START");
        }
    }

    public void OnHammerComplete() {
        if (!isStopped) {
            isStopped = true;

            GameManager.Analytics.OnEvent("Goal", "HammerTimeMS", (int)(GameManager.Timer * 1000));
            GameManager.StopTimer();
            print("TIMER STOPED");

            readyMenu.gameObject.SetActive(true);
            readyMenu.Callback.AddListener(delegate { OnUserReadyForNextClick(); });
        }
    }

    void OnUserReadyForNextClick() {
        GameManager.NextPhase();
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
        int closed = 0;
	    foreach(GameObject go in spawnedObjects) {
            if (!started && go == null)
                OnHammerStart();

            if(go != null) {
                IBoxClosed ibc = go.GetComponentInChildren<IBoxClosed>();
                if(ibc != null) {
                    closed += ibc.IsBoxClosed() ? 1 : 0;
                }
            }
        }
        if(closed == 5) {
            OnHammerComplete();
        }
	}
}
