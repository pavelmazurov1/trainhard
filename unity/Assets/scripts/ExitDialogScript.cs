using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ExitDialogScript : MonoBehaviour
{
    public Button YesButton;
    public Button NoButton;
    public GameObject Panel;
    public Text MessageLabel;
    void Start()
    {
        Panel.SetActive(false);
        NoButton.onClick.AddListener(() =>
        {
            Panel.SetActive(false);
        });
    }

    public void Open(string messageText, UnityAction call)
    {
        MessageLabel.text = messageText;
        YesButton.onClick.AddListener(call);
        Panel.SetActive(true);
    }

    public void Close()
    {
        Panel.SetActive(false);
    }

}
