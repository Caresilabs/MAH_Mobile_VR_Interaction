using UnityEngine;
using System.Collections;
using System;

public class HammerMotionMenu : BaseColorMenu {

    [SerializeField]
    private ColorType hammerColor;

    private GameObject camera;
    private bool hover;

    private float startTilt;
    private float TILT_AMOUNT = 0.2f;

    public new void Start() {
        camera = player.GetComponentInChildren<Camera>().gameObject;
        HideCanvas();
    }

    protected override void ColorPressed(BaseColorMenu.ColorType color) {
        //Don't use
    }

    public void Tilt(bool enter) {
        hover = enter;
        if (enter) {
            ShowCanvas();
            float z = camera.transform.rotation.eulerAngles.z;
            startTilt = ((z > 180) ? (z - 360f) : z) / 90f;
        } else {
            HideCanvas();
        }
    }

    public void Update() {
        if (hover && !player.HasHammer()) {
            float z = camera.transform.rotation.eulerAngles.z;
            float tilt = ((z > 180) ? (z - 360f) : z) / 90f;
            if(tilt >= startTilt + TILT_AMOUNT) {
                player.SetHammer(true);
                Destroy(transform.parent.gameObject);
            }
        }
    }

    public override void SetColor(ColorType type)
    {
    }

    protected override bool ShouldShow() {
        return !player.HasHammer();
    }
}
