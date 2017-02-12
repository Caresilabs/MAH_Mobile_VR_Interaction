using UnityEngine;
using System.Collections;
using Assets.Scripts;

using System.Collections.Generic;
using UnityEngine.UI;

public class TargetChallenge : MonoBehaviour, IChallenge {

    private const float TARGET_TOTAL_TIME = 30;

    [SerializeField]
    private GameObject          Target;

    [SerializeField]
    protected GameManager       GameManager;

    [SerializeField]
    private Text                ScoreText; 

    protected Player            Player;

    private int                 currentChild;
    private GameObject          currentTarget;

    private int                 hits;

    public virtual void StartChallenge()
    {
        this.Player = GameManager.Player;

        currentChild = 0;
        hits = 0;
        spawnTarget();
        Player.SetMovement(Player.MovementType.STILL);
    }

    public virtual void StopChallenge()
    {
        Destroy(currentTarget);
    }

    private void spawnTarget()
    {
        if (currentChild == transform.childCount)
            currentChild = 0;

        BoxCollider bx = transform.GetChild(currentChild).GetComponent<BoxCollider>();
        Vector3 posSpawn = bx.transform.position + new Vector3(
            Random.Range(-bx.size.x / 2, bx.size.x / 2),
            Random.Range(-bx.size.y / 2, bx.size.y / 2),
            Random.Range(-bx.size.z / 2, bx.size.z / 2));

        currentTarget = (GameObject)Instantiate(Target, posSpawn, Quaternion.identity);

        currentTarget.transform.rotation = Quaternion.LookRotation(Player.transform.position - currentTarget.transform.position);
        currentTarget.transform.rotation *= Quaternion.Euler(90, 0, 0);

       currentChild++;
    }

	void Update () {
        if (GameManager.ChallengeState == GameManager.UIState.RUNNING)
        {

            float timeLeft = TARGET_TOTAL_TIME - GameManager.Timer;
            if (timeLeft <= 0)
            {
                ScoreText.text = "Hits: " + hits;
                ReportData();
                GameManager.StopTimer();
            }
            else
            {
                ScoreText.text = "Time: " + (int)timeLeft + "sec";
            }
        } 

        if (currentTarget != null)
        {
            TargetScript ts = currentTarget.GetComponent<TargetScript>();
            if (ts.Dead)
            {
                if (hits == 0)
                {
                    GameManager.StartTimer(false);
                }
                ++hits;

                Destroy(currentTarget);
                spawnTarget();
            }
        }
	   
	}

    private void ReportData()
    {
        GameManager.Analytics.OnEvent(AnalyticsSampler.COMPLETED, "Hits", hits);
    }
}
