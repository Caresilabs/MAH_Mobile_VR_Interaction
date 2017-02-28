using UnityEngine;
using System.Collections;
using Assets.Challenge.Hammer;

public class CanvasCaller : MonoBehaviour {

    public GameObject Canvas;

    void OnDisable()
    {
        Canvas.GetComponent<IMenuCanvas>().Exit();
	}
}
