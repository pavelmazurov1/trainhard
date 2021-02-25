using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainDirector : MonoBehaviour
{
    public static MainDirector Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("меню", LoadSceneMode.Single);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("меню", LoadSceneMode.Single);
        SceneManager.LoadScene("уровень_элеватор", LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("уровень_элеватор"));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
