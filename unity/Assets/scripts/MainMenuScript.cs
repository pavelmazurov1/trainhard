using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public SettingsMenuScript SettingsMenu;
    public ExitDialogScript ExitDialog;

    public Button NewGameButton;
    public Button SettingsButton;
    public Button ExitButton;

    void Start()
    {
        SettingsMenu.Close();
        ExitDialog.Close();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        NewGameButton.onClick.AddListener(() => {
            SceneManager.LoadScene("èíòðî");
        });
        SettingsButton.onClick.AddListener(() => {
            SettingsMenu.Open();
        });
        ExitButton.onClick.AddListener(() => {
            ExitDialog.Open("ÂÛÉÒÈ ÈÇ ÈÃÐÛ?", ()=> {
                GameData.Instance.Save();
                Application.Quit();
            });
        });
    }

}
