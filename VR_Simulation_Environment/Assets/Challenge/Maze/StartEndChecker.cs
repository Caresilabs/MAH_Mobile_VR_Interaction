using UnityEngine;
using System.Collections;
using System;

public class StartEndChecker : MonoBehaviour {

    [SerializeField]
    private bool IsStart;

    private MazeChallenge Challenge;

    private bool isUsed;

	// Use this for initialization
	void Start () {
        this.isUsed = false;
	}

    void OnTriggerEnter(Collider other)
    {
        if (isUsed)
            return;

        if (other.GetComponent<Player>()  != null)
        {
            if (IsStart)
            {
                Challenge.OnStartEnter();
            }
            else
            {
                Challenge.OnGoalEnter();
            }
            isUsed = true;
        }
    }

    public void Reset(MazeChallenge mazeChallenge)
    {
        Challenge = mazeChallenge;
        isUsed = false;
    }
}
