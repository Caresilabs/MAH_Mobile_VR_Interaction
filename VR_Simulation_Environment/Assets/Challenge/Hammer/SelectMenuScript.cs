using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectMenuScript : MonoBehaviour {

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
                player.setHammer(true);
                Destroy(transform.parent.gameObject);
            }
            else
            {
                OkButton.SetActive(false);
                Canvas.SetActive(false);
            }
            setColor(Color.white, selectedButton);
            selectedButton = null;
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
        if (selectedButton == null)
        {
            print("Showing");
            Canvas.gameObject.SetActive(true);
            Quaternion rotation = Quaternion.LookRotation(transform.position - player.transform.position);
            Canvas.transform.rotation = rotation;
        }
    }
	void Start () {
        OkButton.SetActive(false);
        Canvas.gameObject.SetActive(false);
    }
	
	void Update () {
    }

    void setColor(Color color, Button button)
    {
        ColorBlock block = button.colors;
        block.normalColor = color;
        block.highlightedColor = color;
        button.colors = block;
    }
}
