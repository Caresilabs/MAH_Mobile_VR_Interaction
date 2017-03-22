using UnityEngine;
using System.Collections;
using System;
using System.IO;

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


    public void OnEvent(string action, string label, int data)
    {
        string cat = game.GetCurrentGameObjectChallenge().name + "-" + game.State.ToString();
        SaveToDisk(cat, action, label, data);
        SmartAnalytics.SendEvent(label, data, action, cat);
    }

    private void SaveToDisk(string cat, string action, string label, int data)
    {
        using (FileStream stream = new FileStream(Application.persistentDataPath + "/analytics.simme", FileMode.Append, FileAccess.Write))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine(string.Format("TOD: {4}, Category: {0}, Action: {1}, Label: {2}, Data: {3}"
                    , cat, action, label, data, DateTime.Now));
            }
        }
    }
}
