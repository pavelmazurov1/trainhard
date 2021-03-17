using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderProxy : MonoBehaviour
{
    public event EventHandler<Collider> OnTriggerEnterEvent;
    public event EventHandler<Collider> OnTriggerExitEvent;

    private Collider Collider;

    private void Start()
    {
        if(Collider == null)
        {
            Collider = GetComponent<Collider>();
        }
    }

    public void DoActivate()
    {
        if(Collider != null)
            Collider.enabled = true;
    }

    public void DoDeactivate()
    {
        if (Collider != null)
            Collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (OnTriggerEnterEvent != null)
        {
            OnTriggerEnterEvent(this, other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (OnTriggerExitEvent != null)
        {
            OnTriggerExitEvent(this, other);
        }
    }
}
