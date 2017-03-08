﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProgressbarScript : MonoBehaviour {

    private float SELECT_TIME = 1.2f;

    private float currentTime;
    private bool hover;

    public EventTrigger.TriggerEvent Callback;

    [SerializeField]
    private Image loadingBar;

    public void Hover(bool hover)
    {
        this.hover = hover;
        if (!hover)
        {
            currentTime = 0;
            loadingBar.fillAmount = 0;
        }
    }
	
	void Update () {
	    if(hover)
        {
            currentTime += Time.deltaTime;
            loadingBar.fillAmount = currentTime / SELECT_TIME;
            if(currentTime >= SELECT_TIME)
            {
                Callback.Invoke(null);
                currentTime = 0;
                hover = false;
                loadingBar.fillAmount = 0;
            }
        }
	}
}