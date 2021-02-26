using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour
{
    public Slider LookSensivitySlider;
    public Button BackButton;
    public GameObject Panel;

    private GameData GameData;

    void Start()
    {
        GameData = GameData.Instance;

        LookSensivitySlider.minValue = 0;
        LookSensivitySlider.maxValue = 200;
        LookSensivitySlider.value = GameData.LookSensivity;

        Panel.SetActive(false);
        BackButton.onClick.AddListener(() => {
            Panel.SetActive(false);
        });
        LookSensivitySlider.onValueChanged.AddListener((float value) =>
        {
            GameData.LookSensivity = value;
        });
    }

    public void Open()
    {
        Panel.SetActive(true);
    }

    public void Close()
    {
        Panel.SetActive(false);
        if(GameData != null) GameData.Save();
    }

}
