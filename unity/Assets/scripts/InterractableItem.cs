using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterractableItem : MonoBehaviour
{
    public event EventHandler OnInterractedEvent;

    public void DoInterract()
    {
        if(OnInterractedEvent != null)
        {
            OnInterractedEvent(this, EventArgs.Empty);
        }
    }
    
}
