using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticWeaponShootController : MonoBehaviour
{
    public ParticleSystem blustPrticles;

    public bool isShooting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var firePressed = Input.GetButton("Fire1");
        if (isShooting && !firePressed)
        {
            blustPrticles.Stop();
            isShooting = false;
        }
        if (firePressed && !isShooting)
        {
            blustPrticles.Play(true);
            isShooting = true;
        }
    }
}
