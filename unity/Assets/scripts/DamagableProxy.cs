using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableProxy : MonoBehaviour, IDamagable
{
    public GameObject origin;
    private IDamagable originDamagable;

    void Start()
    {
        originDamagable = origin.GetComponent<IDamagable>();
    }

    void IDamagable.applyDamage(float value)
    {
        originDamagable.applyDamage(value);
    }
}
