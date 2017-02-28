using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Challenge.Hammer;
using System;

public class SelectMenuScript : MonoBehaviour, IMenuCanvas
{

    [SerializeField]
    private GameObject OkButton;

    [SerializeField]
    private GameObject PickUpButton;

    [SerializeField]
    private Player player;

    private Button selectedButton;

    [SerializeField]
    private GameObject Canvas;

    public void Enter(Button button)
    {
        if (button == OkButton.GetComponent<Button>())
        {
            print("REMOVE");
            if (selectedButton == PickUpButton.GetComponent<Button>())
            {
                print("REMOVE");
                player.SetHammer(true);
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Canvas.SetActive(false);
            }
            Exit();
        }
        else
        {
            OkButton.SetActive(true);
            if (selectedButton != null)
                setColor(Color.white, selectedButton);
            selectedButton = button;
            setColor(Color.red, selectedButton);
        }
    }

    public void Exit(Button button)
    {
    }

    public void Show()
    {
        if (player.HasHammer())
            return;

        if (player.currentCanvas != null && player.currentCanvas != Canvas)
            player.currentCanvas.SetActive(false);
        player.currentCanvas = Canvas;

        print("Showing");
        Canvas.gameObject.SetActive(true);
        Quaternion rotation = Quaternion.LookRotation(transform.position - player.transform.position);
        Canvas.transform.rotation = rotation;
    }
	void Start () {
        OkButton.SetActive(false);
        Canvas.gameObject.SetActive(false);
    }


    void setColor(Color color, Button button)
    {
        ColorBlock block = button.colors;
        block.normalColor = color;
        block.highlightedColor = color;
        button.colors = block;
    }

    public void Exit()
    {
        OkButton.SetActive(false);
        if (selectedButton != null)
            setColor(Color.white, selectedButton);
        selectedButton = null;
        print("DISABLED");
    }
}
