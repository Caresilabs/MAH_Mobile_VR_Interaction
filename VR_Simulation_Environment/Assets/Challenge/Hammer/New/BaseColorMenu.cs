using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

abstract public class BaseColorMenu : MonoBehaviour
{
    public enum ColorType {
        RED, GREEN, BLUE, YELLOW
    }

    [SerializeField]
    private List<BaseColorObject> buttons = new List<BaseColorObject>();

    [SerializeField]
    protected Player player;

    [SerializeField]
    protected GameManager GameManager;

    [SerializeField]
    protected GameObject Canvas;

    protected void Start()
    {
        if (Canvas == null)
            return;
        Canvas.SetActive(false);
        foreach(BaseColorObject button in buttons) {
            button.callBackEvent.AddListener(ColorPressed);
        }
    }

    protected abstract void ColorPressed(ColorType color);

    protected abstract bool ShouldShow();

    protected void HideCanvas() {
        Canvas.SetActive(false);
    }

    public void ShowCanvas()
    {
        if (!ShouldShow())
            return;

        if (player.currentCanvas != null && player.currentCanvas != Canvas && player.currentCanvas.activeSelf)
            GameManager.Analytics.OnEvent("Canvas", "Show (Hide old)", (int)(GameManager.Timer * 1000), true);
        else if (player.currentCanvas != null && !player.currentCanvas.activeSelf)
            GameManager.Analytics.OnEvent("Canvas", "Show", (int)(GameManager.Timer * 1000), true);
        else if (player.currentCanvas == null)
            GameManager.Analytics.OnEvent("Canvas", "Show", (int)(GameManager.Timer * 1000), true);

        if (player.currentCanvas != null && player.currentCanvas != Canvas) {
            player.currentCanvas.SetActive(false);
        }

        player.currentCanvas = Canvas;

        Canvas.gameObject.SetActive(true);
        Quaternion rotation = Quaternion.LookRotation(transform.position - player.transform.position);
        Canvas.transform.rotation = rotation;

    }

    public abstract void SetColor(ColorType type);

    protected Color ColorTypeToColor(ColorType type)
    {
        switch(type)
        {
            case ColorType.BLUE: return Color.blue;
            case ColorType.GREEN: return Color.green;
            case ColorType.RED: return Color.red;
            case ColorType.YELLOW: return Color.yellow;
        }
        return Color.gray;
    }
}
