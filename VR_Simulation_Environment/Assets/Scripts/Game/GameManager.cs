using UnityEngine;
using System.Collections;
using UnityEngine.VR;
using Assets.Scripts;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {

    public enum LevelState
    {
        PRACTICE, COMPETITION
    }

    public LevelState State { get; private set; }

    public IChallenge CurrentChallenge { get; private set; }

    public AnalyticsSampler Analytics { get; private set; }

    [SerializeField]
    private List<GameObject> vrChallenges;

    [SerializeField]
    private List<GameObject> controllerChallenges;

    private List<GameObject> challenges;

    private GvrReticlePointer pointer;

    private int challengeIndex;

    // Use this for initialization
    void Start () {
        this.Analytics = GetComponent<AnalyticsSampler>();

        this.pointer = FindObjectOfType<GvrReticlePointer>();

        this.challenges = GlobalManager.ControllerBuild ? controllerChallenges : vrChallenges;
        this.challengeIndex = 0;

        Begin();
	}

    private void Begin()
    {
        State = LevelState.COMPETITION;

        CurrentChallenge = challenges[0].GetComponent<IChallenge>();
        CurrentChallenge.StartChallenge();
    }

    // Update is called once per frame
    void Update () {

    }

    public void NextPhase()
    {
        if (State == LevelState.PRACTICE)
        {
            CurrentChallenge.StopChallenge();
            State = LevelState.COMPETITION;

            CurrentChallenge.StartChallenge();
        }
        else
        {
            CurrentChallenge.StopChallenge();

            ++challengeIndex;
            if (challengeIndex >= challenges.Count)
            {
                // next scene
            }
            else
            {
                CurrentChallenge = challenges[challengeIndex].GetComponent<IChallenge>();
                CurrentChallenge.StartChallenge();
            }
        }
    }
}
