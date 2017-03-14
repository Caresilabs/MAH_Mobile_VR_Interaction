using UnityEngine;
using System.Collections;
using UnityEngine.VR;
using Assets.Scripts;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private int ChallengeIndex;

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
        State = LevelState.PRACTICE;

        challenges[ChallengeIndex].SetActive(true);
        CurrentChallenge = challenges[ChallengeIndex].GetComponent<IChallenge>();
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

        UpdateSkipChallenge();
    }

    float t0 = 0f;
    private void UpdateSkipChallenge()
    {
        if (Input.GetMouseButtonDown(0))
            t0 = Time.time;

        if (Input.GetMouseButtonUp(0) && (Time.time - t0) > 0.9f)
        {
            StopTimer();
            NextPhase();
        }
    }

    public void NextPhase()
    {
        if (State == LevelState.PRACTICE)
        {
            CurrentChallenge.StopChallenge();
            challenges[ChallengeIndex].SetActive(false);

            ++ChallengeIndex;
            if (ChallengeIndex >= challenges.Count)
            {
                State = LevelState.COMPETITION;
                ChallengeIndex = 0;
            }
            
            challenges[ChallengeIndex].SetActive(true);
            CurrentChallenge = challenges[ChallengeIndex].GetComponent<IChallenge>();
            ChallengeState = UIState.PREPARE;
            CurrentChallenge.StartChallenge();
            
        }
        else
        {
            CurrentChallenge.StopChallenge();
            challenges[ChallengeIndex].SetActive(false);

            ++ChallengeIndex;
            if (ChallengeIndex >= challenges.Count)
            {
                // next scene
                int id = SceneManager.GetActiveScene().buildIndex;
                if (id < 3) {
                    SceneManager.LoadScene(id+1);
                } else {
                    print("end");
                }
            }
            else
            {
                challenges[ChallengeIndex].SetActive(true);
                CurrentChallenge = challenges[ChallengeIndex].GetComponent<IChallenge>();
                ChallengeState = UIState.PREPARE;
                CurrentChallenge.StartChallenge();
            }
        }
    }
}
