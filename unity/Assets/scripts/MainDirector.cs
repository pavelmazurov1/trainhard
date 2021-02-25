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
        SceneManager.LoadScene("����", LoadSceneMode.Single);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("����", LoadSceneMode.Single);
        SceneManager.LoadScene("�������_��������", LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("�������_��������"));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
