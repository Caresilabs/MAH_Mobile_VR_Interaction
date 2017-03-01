using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    class SelectMenuGeneral : MonoBehaviour
    {
        [SerializeField]
        private GameObject OkButton;

        [SerializeField]
        private GameObject ActionButton;

        public EventTrigger.TriggerEvent Callback;

        [SerializeField]
        private GameObject Canvas;

        private Button selectedButton;
        private Button hoverButton;
        private Camera cam;

        private float time;

        public void Start()
        {
            cam = FindObjectOfType<Camera>();
        }

        public void Update()
        {
            Quaternion rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
            Canvas.transform.rotation = rotation;

            time += Time.deltaTime;
            if (time > 0.9f && hoverButton == OkButton.GetComponent<Button>())
            {
                BaseEventData eventData = new BaseEventData(EventSystem.current);
                eventData.selectedObject = selectedButton.gameObject;
                Callback.Invoke(eventData);
                Canvas.gameObject.SetActive(false);
                OkButton.SetActive(false);

                setColor(Color.white, selectedButton);
                selectedButton = null;
            }
        }

        public void Enter(Button button)
        {
            time = 0;
            hoverButton = button;

            if (button == OkButton.GetComponent<Button>())
            {
                if (selectedButton == ActionButton.GetComponent<Button>())
                {
                    //BaseEventData eventData = new BaseEventData(EventSystem.current);
                    //eventData.selectedObject = selectedButton.gameObject;
                    //Callback.Invoke(eventData);
                    //Canvas.gameObject.SetActive(false);
                    //OkButton.SetActive(false);
                }
                else
                {
                  //  OkButton.SetActive(false);
                  //  Canvas.SetActive(false);

                   // setColor(Color.white, selectedButton);
                   // selectedButton = null;
                }
               
            }
            else
            {
                OkButton.SetActive(true);
                if (selectedButton != null)
                    setColor(Color.white, selectedButton);
                selectedButton = button;
                setColor(Color.red, selectedButton);
            }
        }

        public void Exit(Button button)
        {
            hoverButton = null;
        }

        public void Show()
        {
            if (selectedButton == null)
            {
                Canvas.gameObject.SetActive(true);
                OkButton.SetActive(false);
            }
        }

        void setColor(Color color, Button button)
        {
            ColorBlock block = button.colors;
            block.normalColor = color;
            block.highlightedColor = color;
            button.colors = block;
        }
    }
}
