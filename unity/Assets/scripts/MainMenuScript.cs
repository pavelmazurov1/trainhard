using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera MenuCamera;
    public GameObject Panel;
    public SettingsMenuScript SettingsMenu;
    public ExitDialogScript ExitDialog;

    public Button NewGameButton;
    public Button SettingsButton;
    public Button ExitButton;
    void Start()
    {
        SettingsMenu.Close();
        ExitDialog.Close();
        Open();

        NewGameButton.onClick.AddListener(() => {
            MainDirector.Instance.StartNewGame();
            Close();
        });
        SettingsButton.onClick.AddListener(() => {
            SettingsMenu.Open();
        });
        ExitButton.onClick.AddListener(() => {
            ExitDialog.Open();
        });
    }

    private void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            Open();
        }
    }

    public void Open()
    {
        MenuCamera.enabled = true;
        Panel.SetActive(true);
        SettingsMenu.Close();
        ExitDialog.Close();
    }

    public void Close()
    {
        Panel.SetActive(false);
        MenuCamera.enabled = false;
    }

}
