using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenuScript : MonoBehaviour
{
    public GameObject Panel;
    public SettingsMenuScript SettingsMenu;
    public ExitDialogScript ExitDialog;

    public Button BackToGameButton;
    public Button SettingsButton;
    public Button ToMainMenuButton;

    public bool IsOpened = false;

    public UnityEvent menuOpenChanged;
    public UnityEvent continueGame;
    //public UnityEvent exitLevel;

    public Image FadeScreen;

    public void Close()
    {
        if (IsOpened)
        {
            IsOpened = false;
            Panel.SetActive(false);
            SettingsMenu.Close();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            menuOpenChanged.Invoke();
        }
    }

    public void Open()
    {
        if (!IsOpened)
        {
            IsOpened = true;
            Panel.SetActive(true);
            SettingsMenu.Close();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            menuOpenChanged.Invoke();
            
        }
    }

    void Start()
    {
        BackToGameButton.onClick.AddListener(() => {
            Close();
            continueGame.Invoke();
        });
        SettingsButton.onClick.AddListener(() => {
            SettingsMenu.Open();
        });
        ToMainMenuButton.onClick.AddListener(() => {
            ExitDialog.Open("¬≈—‹ Õ≈—Œ’–¿Õ≈ÕÕ€… œ–Œ√–≈—— ¡”ƒ≈“ œŒ“≈–ﬂÕ.\n¬€…“» ¬ Ã≈Õﬁ ?", () => {
                Close();
                //todo save user data
                //exitLevel.Invoke();
                //
                SceneManager.LoadScene("„Î‡‚ÌÓÂ_ÏÂÌ˛");
            });
        });

        IsOpened = false;
        Panel.SetActive(false);
        SettingsMenu.Close();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private bool _MenuAvailable = false;
    public bool MenuAvailable
    {
        get { return _MenuAvailable; }
        set
        {
            if (_MenuAvailable != value)
            {
                _MenuAvailable = value;
                OnMenuAvailableChanged();
            }
        }
    }

    private Coroutine MenuOpenCloseCoroutine = null;
    private void OnMenuAvailableChanged()
    {
        if(MenuAvailable)
        {
            if(MenuOpenCloseCoroutine == null)
            {
                MenuOpenCloseCoroutine = StartCoroutine(MenuOpenClose());
            }
        }
        else
        {
            Close();
            if(MenuOpenCloseCoroutine != null)
            {
                StopCoroutine(MenuOpenCloseCoroutine);
                MenuOpenCloseCoroutine = null;
            }
        }
    }

    IEnumerator MenuOpenClose()
    {
        while (true)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                if (IsOpened)
                {
                    Close();
                    continueGame.Invoke();
                }
                else
                {
                    Open();
                }
            }
            yield return null;
        }
    }

    public IEnumerator Fade(float fadeTime)
    {
        FadeScreen.enabled = true;
        FadeScreen.color = new Color(0, 0, 0, 0);
        float time = 0;
        while (time <= fadeTime)
        {
            var color = FadeScreen.color;
            color.a = time / fadeTime;
            FadeScreen.color = color;
            time += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator UnFade(float fadeTime)
    {
        FadeScreen.color = new Color(0, 0, 0, 1);
        float time = 0;
        while (time <= fadeTime)
        {
            var color = FadeScreen.color;
            color.a = 1 - time / fadeTime;
            FadeScreen.color = color;
            time += Time.deltaTime;
            yield return null;
        }
        FadeScreen.enabled = false;
    }
}
