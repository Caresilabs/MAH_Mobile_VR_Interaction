using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class BaseColorObject : MonoBehaviour {

    public class IntEvent : UnityEvent<BaseColorMenu.ColorType> { }

    public BaseColorMenu.ColorType color;

    [HideInInspector]
    public IntEvent callBackEvent = new IntEvent();

    public void ButtonPressed() {
        callBackEvent.Invoke(color);
    }
}
