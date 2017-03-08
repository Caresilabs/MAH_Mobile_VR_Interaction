using UnityEngine;
using System.Collections;
using System;

public class HammerMenu : BaseColorMenu {

    [SerializeField]
    private ColorType hammerColor;

    protected override void ColorPressed(BaseColorMenu.ColorType color) {
        if (hammerColor == color) {
            player.SetHammer(true);
            Destroy(transform.parent.gameObject);
        } else {
            HideCanvas();
        }
    }

    protected override bool ShouldShow() {
        return !player.HasHammer();
    }
}
