using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class HammerControllerMenu : BaseColorMenu {

    [SerializeField]
    private ColorType hammerColor;
    private bool hover;

    float timer;
    float DELAY = 0.8f;

    [SerializeField]
    Image cross;

    public new void Start() {
        HideCanvas();
    }

    protected override void ColorPressed(BaseColorMenu.ColorType color) {
        //Don't use
    }

    public void Enter(bool enter) {
        hover = enter;
        if (enter) {
            ShowCanvas();
            timer = 0.8f;
            cross.enabled = false;
        } else {
            HideCanvas();
        }
    }

    public void Update() {
        timer += Time.deltaTime;
        if (timer > DELAY)
            cross.enabled = false;
        if (hover && !player.HasHammer()) {
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
    }

    public void Check(ColorType type) {
        if (timer > DELAY) {
            if (hammerColor == type) {
                player.SetHammer(true);
                Destroy(transform.parent.gameObject);
            } else {
                timer = 0;
                cross.enabled = true;
            }
        }
    }

    public override void SetColor(ColorType type)
    {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).GetComponent<Renderer>().material.color = ColorTypeToColor(type);
            hammerColor = type;
        }
    }

    protected override bool ShouldShow() {
        return !player.HasHammer();
    }
}
