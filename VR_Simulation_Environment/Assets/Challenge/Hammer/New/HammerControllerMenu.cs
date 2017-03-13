using UnityEngine;
using System.Collections;
using System;

public class HammerControllerMenu : BaseColorMenu {

    [SerializeField]
    private ColorType hammerColor;
    private bool hover;

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
        } else {
            HideCanvas();
        }
    }

    public void Update() {
        if (hover) {
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
        if (hammerColor == type && !player.HasHammer()) {
            player.SetHammer(true);
            Destroy(transform.parent.gameObject);
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
