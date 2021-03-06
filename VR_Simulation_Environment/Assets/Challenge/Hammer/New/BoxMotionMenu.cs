﻿using UnityEngine;
using System.Collections;
using System;
using Assets.Challenge.Hammer.New;

public class BoxMotionMenu : BaseColorMenu, IBoxClosed {

    [SerializeField]
    private ColorType boxColor;

    [SerializeField]
    GameObject Top;

    [SerializeField]
    GameObject HammerInstance;

    Quaternion start;
    GameObject camera;
    bool hover;
    float startTilt;
    float TILT_AMOUNT = -0.2f;

    bool closing;

    protected override void ColorPressed(ColorType color)
    {
    }

    public void Tilt(bool enter) {
        hover = enter;
        if (enter) {
            ShowCanvas();
            float z = camera.transform.rotation.eulerAngles.z;
            startTilt = ((z > 180) ? (z - 360f) : z) / 90f;
        } else {
            GameManager.Analytics.OnEvent("Canvas", "Hide", (int)(GameManager.Timer * 1000), true);
            HideCanvas();
        }
    }

    public void Update() {
        if (hover && !closing && player.HasHammer()) {
            float z = camera.transform.rotation.eulerAngles.z;
            float tilt = ((z > 180) ? (z - 360f) : z) / 90f;
            if (tilt <= startTilt + TILT_AMOUNT) {
                Close();
            }
        }

        if (closing) {
            Top.transform.rotation = Quaternion.Slerp(Top.transform.rotation, start, 0.015f);
        }
    }

    protected override bool ShouldShow()
    {
        return player.HasHammer() && !closing;
    }

    new void Start()
    {
        base.Start();
        start = Top.transform.rotation * Quaternion.AngleAxis(90, new Vector3(0, 0, 1));
        camera = player.GetComponentInChildren<Camera>().gameObject;
    }

    public void Close()
    {
        if (!closing)
        {
            GameManager.Analytics.OnEvent("Canvas", "Correct", (int)(GameManager.Timer * 1000), true);
            HideCanvas();
            player.SetHammer(false);
            closing = true;
            GameObject go = (GameObject)Instantiate(HammerInstance, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            go.transform.SetParent(gameObject.transform);
        }
    }

    public override void SetColor(ColorType type)
    {
    }

    public bool IsBoxClosed() {
        return closing;
    }
}
