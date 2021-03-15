using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorLevelDirector : MonoBehaviour
{
    public InGameMenuScript InGameMenu;

    private GameData GameData;
    // Start is called before the first frame update
    void Start()
    {
        InGameMenu.MenuAvailable = true;
        GameData = GameData.Instance;
        GameData.Scene = "уровень_элеватор";
        GameData.Save();

        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        yield return StartCoroutine(InGameMenu.UnFade(5));
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
