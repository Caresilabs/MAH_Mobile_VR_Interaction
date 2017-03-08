using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Challenge.Hammer;
using System;

public class DelayMenuScript : MonoBehaviour, IMenuCanvas
{

    [SerializeField]
    private GameObject exitButton;

    [SerializeField]
    private GameObject pickupButton;

    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject Canvas;

    void Start()
    {
        Canvas.gameObject.SetActive(false);
        exitButton.GetComponent<ProgressbarScript>().Callback.AddListener(delegate { ExitPress(); });
        pickupButton.GetComponent<ProgressbarScript>().Callback.AddListener(delegate { PickupPress(); });
    }

    public void Show()
    {
        if (player.HasHammer())
            return;

        if (player.currentCanvas != null && player.currentCanvas != Canvas)
            player.currentCanvas.SetActive(false);
        player.currentCanvas = Canvas;

        Canvas.gameObject.SetActive(true);
        Quaternion rotation = Quaternion.LookRotation(transform.position - player.transform.position);
        Canvas.transform.rotation = rotation;
    }

    void ExitPress()
    {
        Canvas.SetActive(false);
    }
    void PickupPress()
    {
        player.SetHammer(true);
        Destroy(transform.parent.gameObject);
    }

    public void Exit()
    {
       
    }
}
