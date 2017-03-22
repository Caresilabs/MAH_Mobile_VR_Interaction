using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CircleObjectScript : BaseColorObject {

    private float DELAY = 0.2f;
    float currentTime;
    bool hover;

    public void Hover(bool hover) {
        this.hover = hover;
        if (!hover) {
            currentTime = 0;
        }
    }

    void Update() {
        if (hover) {
            currentTime += Time.deltaTime;
            if (currentTime >= DELAY) {
                ButtonPressed();
                hover = false;
            }
        }
    }

}
