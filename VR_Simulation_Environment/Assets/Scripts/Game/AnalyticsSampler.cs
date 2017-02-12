using UnityEngine;
using System.Collections;

public class AnalyticsSampler : MonoBehaviour {

    public static string COMPLETED = "Completed";

    [SerializeField]
    private GameManager game;

	// Use this for initialization
	void Start () {
        SmartAnalytics.SetTrackingID("UA-72246978-2");
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}

    public void OnChallengeComplete(GameManager.LevelState state, float time)
    {
        SmartAnalytics.SendTiming("name","cat", "lab", (int)(time * 1000));
    }

    public void OnEvent(string action, string label, float data)
    {
        SmartAnalytics.SendEvent(label, data, action, game.CurrentChallenge.GetType().Name + "-" + game.State.ToString());
    }
}
