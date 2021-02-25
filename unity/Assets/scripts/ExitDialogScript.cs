using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitDialogScript : MonoBehaviour
{
    public Button YesButton;
    public Button NoButton;
    public GameObject Panel;
    void Start()
    {
        Panel.SetActive(false);
        YesButton.onClick.AddListener(() =>
        {
            MainDirector.Instance.ExitGame();
        });
        NoButton.onClick.AddListener(() =>
        {
            Panel.SetActive(false);
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
