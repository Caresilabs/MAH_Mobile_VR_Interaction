using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectMenuScript : MonoBehaviour {

    [SerializeField]
    private GameObject OkButton;

    private Button selectedButton;

    [SerializeField]
    private GameObject canvas;

    public void Enter(Button button)
    {
        if (button == OkButton.GetComponent<Button>())
        {
            print("REMOVE");
            selectedButton = null;
            OkButton.SetActive(false);
            canvas.SetActive(false); //WHY YOU NO WORKING!=?!=!=="#=#!=#194yh
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
        canvas.gameObject.SetActive(true);
    }
	void Start () {
        OkButton.SetActive(false);
        canvas.gameObject.SetActive(false);
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
