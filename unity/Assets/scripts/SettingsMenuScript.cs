using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour
{
    public Slider LookSensivitySlider;
    public Button BackButton;
    public GameObject Panel;
    void Start()
    {
        Panel.SetActive(false);
        BackButton.onClick.AddListener(() => {
            Panel.SetActive(false);
        });
        LookSensivitySlider.onValueChanged.AddListener((float value) =>
        {
            //todo get player settings and set new sensivity;
        });
    }

    public void Open()
    {
        Panel.SetActive(true);
    }

    public void Close()
    {
        Panel.SetActive(false);
    }

}
