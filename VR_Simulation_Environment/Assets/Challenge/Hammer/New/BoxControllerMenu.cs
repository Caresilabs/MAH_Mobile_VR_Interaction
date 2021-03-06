﻿using UnityEngine;
using System.Collections;
using System;
using Assets.Challenge.Hammer.New;
using UnityEngine.UI;

public class BoxControllerMenu : BaseColorMenu, IBoxClosed {

    [SerializeField]
    private ColorType boxColor;

    [SerializeField]
    GameObject Top;

    [SerializeField]
    GameObject HammerInstance;

    [SerializeField]
    Image cross;

    Quaternion start;
    bool hover;

    bool closing;

    float timer;
    float DELAY = 0.8f;

    protected override void ColorPressed(ColorType color)
    {
    }

    public void Enter(bool enter) {
        hover = enter;
        if (enter) {
            GameManager.Analytics.OnEvent("Canvas", "Show!", (int)(GameManager.Timer * 1000), true);
            ShowCanvas();
            timer = 0.8f;
            cross.enabled = false;
        } else {
            GameManager.Analytics.OnEvent("Canvas", "Hide!", (int)(GameManager.Timer * 1000), true);
            HideCanvas();
        }
    }

    public void Update() {
        timer += Time.deltaTime;
        if (timer > DELAY)
            cross.enabled = false;
        if (hover && !closing && player.HasHammer()) {
            if (Input.GetButtonDown("Fire1")) { //A - GREEN
                Check(ColorType.GREEN);
            } else if (Input.GetButtonDown("Fire2")) { //B - RED
                Check(ColorType.RED);
            } else if (Input.GetButtonDown("Fire3")) { //X - BLUE
                Check(ColorType.BLUE);
            } else if (Input.GetButtonDown("Jump")) { //Y - YELLOW
                Check(ColorType.YELLOW);
            }
        }

        if (closing) {
            Top.transform.rotation = Quaternion.Slerp(Top.transform.rotation, start, 0.015f);
        }
    }

    public void Check(ColorType type) {
        if (timer > DELAY) {
            if (boxColor == type) {
                GameManager.Analytics.OnEvent("Canvas", "Correct!", (int)(GameManager.Timer * 1000), true);
                Close();
            } else {
                GameManager.Analytics.OnEvent("Canvas", "Wrong!", (int)(GameManager.Timer * 1000), true);
                timer = 0;
                cross.enabled = true;
            }
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
    }

    public void Close()
    {
        if (!closing)
        {
            HideCanvas();
            player.SetHammer(false);
            closing = true;
            GameObject go = (GameObject)Instantiate(HammerInstance, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            go.transform.SetParent(gameObject.transform);
        }
    }

    public override void SetColor(ColorType type)
    {
        boxColor = type;
        for (int i = 0; i < transform.childCount - 1; i++) {
            transform.GetChild(i).GetComponent<Renderer>().material.color = ColorTypeToColor(type);
        }
    }

    public bool IsBoxClosed() {
        return closing;
    }
}
