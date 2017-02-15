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

        Begin();
    }

    private void Begin()
    {
        this.challengeIndex = 2;
        State = LevelState.PRACTICE;

        challenges[challengeIndex].SetActive(true);
        CurrentChallenge = challenges[challengeIndex].GetComponent<IChallenge>();
        CurrentChallenge.StartChallenge();
    }

    public void StartTimer()
    {
        ChallengeState = UIState.RUNNING;
    }

    public void StopTimer()
    {
        ChallengeState = UIState.END;

        Timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ChallengeState == UIState.RUNNING)
        {
            Timer += Time.deltaTime;
        }
    }

    public void NextPhase()
    {
        if (State == LevelState.PRACTICE)
        {
            // CurrentChallenge.StopChallenge();
            // State = LevelState.COMPETITION;
            //  ChallengeState = UIState.PREPARE;

            // CurrentChallenge.StartChallenge();

            CurrentChallenge.StopChallenge();
            challenges[challengeIndex].SetActive(false);

            ++challengeIndex;
            if (challengeIndex >= challenges.Count)
            {
                State = LevelState.COMPETITION;
                challengeIndex = 0;
            }
            
            challenges[challengeIndex].SetActive(true);
            CurrentChallenge = challenges[challengeIndex].GetComponent<IChallenge>();
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
                ChallengeState = UIState.PREPARE;
                CurrentChallenge.StartChallenge();
            }
        }
    }
}
