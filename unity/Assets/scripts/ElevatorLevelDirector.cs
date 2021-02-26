using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorLevelDirector : MonoBehaviour
{
    public InGameMenuScript InGameMenu;
    // Start is called before the first frame update
    void Start()
    {
        InGameMenu.MenuAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
