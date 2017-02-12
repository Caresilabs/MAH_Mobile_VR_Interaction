using UnityEngine;
using System.Collections;
using UnityEngine.VR;
using Assets.Scripts;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{

    public enum LevelState
    {
        PRACTICE, COMPETITION
    }

    public enum UIState
    {
        PREPARE, RUNNING, END
    }

    public LevelState State { get; private set; }

    public UIState ChallengeState { get; private set; }

    public IChallenge CurrentChallenge { get; private set; }

    public AnalyticsSampler Analytics { get; private set; }

    public Player Player { get; private set; }

    public float Timer { get; private set; }

    [SerializeField]
    private List<GameObject> vrChallenges;

    [SerializeField]
    private List<GameObject> controllerChallenges;

    private List<GameObject> challenges;

    private GvrReticlePointer pointer;

    private int challengeIndex;

    // Use this for initialization
    void Awake()
    {
        this.Analytics = GetComponent<AnalyticsSampler>();
        this.Player = GetComponentInChildren<Player>();

        this.pointer = FindObjectOfType<GvrReticlePointer>();

        this.challenges = GlobalManager.ControllerBuild ? controllerChallenges : vrChallenges;
        this.challengeIndex = 0;

        Begin();
    }

    private void Begin()
    {
        State = LevelState.PRACTICE;

        challenges[0].SetActive(true);
        CurrentChallenge = challenges[0].GetComponent<IChallenge>();
        CurrentChallenge.StartChallenge();
    }

    public void StartTimer(bool shownPrepareUI)
    {
        if (shownPrepareUI)
        {

        }
        else
        {
            ChallengeState = UIState.RUNNING;
        }
    }

    public void StopTimer()
    {
        ChallengeState = UIState.END;

        Timer = 0;
        NextPhase();
    }

    // Update is called once per frame
    void Update()
    {
        if (ChallengeState == UIState.RUNNING)
        {
            Timer += Time.deltaTime;
        }
    }

    private void NextPhase()
    {
        if (State == LevelState.PRACTICE)
        {
            CurrentChallenge.StopChallenge();
            State = LevelState.COMPETITION;
            ChallengeState = UIState.PREPARE;

            CurrentChallenge.StartChallenge();
        }
        else
        {
            CurrentChallenge.StopChallenge();
            challenges[challengeIndex].SetActive(false);

            ++challengeIndex;
            if (challengeIndex >= challenges.Count)
            {
                // next scene
            }
            else
            {
                challenges[challengeIndex].SetActive(true);
                CurrentChallenge = challenges[challengeIndex].GetComponent<IChallenge>();
                State = LevelState.PRACTICE;
                ChallengeState = UIState.PREPARE;
                CurrentChallenge.StartChallenge();
            }
        }
    }
}
