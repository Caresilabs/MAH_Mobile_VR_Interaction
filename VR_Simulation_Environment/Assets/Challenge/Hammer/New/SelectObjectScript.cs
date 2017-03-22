using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectObjectScript : BaseColorObject {

    private Image current;

    [SerializeField]
    private GameObject selectButton;

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

    public void Select() {
        selectButton.SetActive(false);
        if (current != null) {
            current.fillAmount = 0;
        }
        current = null;
        ButtonPressed();
    }

    void OnDisable() {
        if (current != null) {
            current.fillAmount = 0;
        }
    }
}
