using UnityEngine;
using System.Collections;
using System;

public class BoxMenu : BaseColorMenu
{

    [SerializeField]
    private ColorType boxColor;

    [SerializeField]
    GameObject Top;

    [SerializeField]
    GameObject HammerInstance;

    Quaternion start;

    bool closing;

    protected override void ColorPressed(ColorType color)
    {
        if (boxColor == color)
        {
            player.SetHammer(false);
            GetComponent<Collider>().enabled = false;
            Close();
        }
        HideCanvas();
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

    void Update()
    {
        if (closing)
        {
            Top.transform.rotation = Quaternion.Slerp(Top.transform.rotation, start, 0.015f);
        }
    }

    public void Close()
    {
        if (!closing)
        {
            closing = true;
            Instantiate(HammerInstance, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        }
    }

    public override void SetColor(ColorType type)
    {
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            transform.GetChild(i).GetComponent<Renderer>().material.color = ColorTypeToColor(type);
            boxColor = type;
        }
    }
}
