using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectObjectScript : BaseColorObject {

    private Image current;

    [SerializeField]
    private GameObject selectButton;

    bool hover;
    float currentTime;
    float MAX_TIME = 0.15f;

    void Start() {
        selectButton.SetActive(false);
    }
    public void SetSelectedColor(Image image) {
        if(current != null) {
            current.fillAmount = 0;
        }

        current = image;
        current.fillAmount = 1f;
        selectButton.SetActive(true);
        color = current.GetComponent<SelectColorHolder>().color;
    }

    public void Select(bool enter) {
        print("hover " + enter);
        hover = enter;
        if (!hover) {
            currentTime = 0;
        }
    }

    void OnDisable() {
        if (current != null) {
            current.fillAmount = 0;
        }
    }

    void Update() {
        if (hover) {
            print("hover");
            currentTime += Time.deltaTime;
        }
        if (currentTime > MAX_TIME) {
            selectButton.SetActive(false);
            if (current != null) {
                current.fillAmount = 0;
            }
            current = null;
            ButtonPressed();
        }
    }
}
