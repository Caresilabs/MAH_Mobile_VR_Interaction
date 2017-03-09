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

    public override void SetColor(ColorType type)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Renderer>().material.color = ColorTypeToColor(type);
            hammerColor = type;
        }
    }

    protected override bool ShouldShow() {
        return !player.HasHammer();
    }
}
