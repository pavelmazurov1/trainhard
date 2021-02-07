using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticWeaponShootController : MonoBehaviour
{
    public ParticleSystem blustPrticles;
    public AudioClip shotSound;

    public float fireRatePerSecond;
    public bool isShooting = false;
    public float oneShotDamage = 50f;

    private AudioSource audioSource;
    private ParticleSystem.EmissionModule emissionModule;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        emissionModule = blustPrticles.emission;
        StartCoroutine(shotRoutine());
    }

    IEnumerator shotRoutine()
    {
        while (true)
        {
            if (isShooting)
            {
                audioSource.PlayOneShot(shotSound);

                //проверим куда стреляем
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit))
                {
                    //Debug.Log("!!!hit!!!");
                    var damagable = hit.collider.GetComponent<IDamagable>();
                    if(damagable != null)
                    {
                        //Debug.Log("!!!applyDamage!!!");
                        damagable.applyDamage(oneShotDamage);
                    }
                }

                yield return new WaitForSeconds(1f / fireRatePerSecond);
            }
            else
            {
                yield return null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        emissionModule.rate = fireRatePerSecond;
        
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
