using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashroomUnit : MonoBehaviour
{
    enum LifeStateEnum
    {
        None = -1,
        Idle,
        Warning,
        Die
    }

    public int state
    {
        get { return _state; }
        set
        {
            if(_state != value)
            {
                _state = value;
                onStateChanged();
            }
        }
    }
    private int _state = (int)LifeStateEnum.None;

    public Animator unitAnimator;
    public float preBurstTimeoutSeconds = 1.0f;
    public float burstTimeoutSeconds = 1.0f;
    public float warningCooldownSeconds = 5.0f;

    public Collider warningCollider;
    public ParticleSystem sporeParticle;

    public AudioSource audioSource;
    public AudioClip idleSound;
    public AudioClip warningSound;
    public AudioClip burstSound;

    private GameObject targetObject = null;
    private Coroutine preBurstRoutine = null;

    // Start is called before the first frame update
    void Start()
    {
        state = (int)LifeStateEnum.Idle;
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void onStateChanged()
    {
        updateSoundByState();
        unitAnimator.SetInteger("state", state);
    }

    private void updateSoundByState()
    {
        switch ((LifeStateEnum)state)
        {
            case LifeStateEnum.Idle:
                audioSource.clip = idleSound;
                audioSource.loop = true;
                audioSource.Play();
                break;
            case LifeStateEnum.Warning:
                audioSource.clip = warningSound;
                audioSource.loop = true;
                audioSource.Play();
                break;
            case LifeStateEnum.Die:
                audioSource.Stop();
                audioSource.loop = false;
                audioSource.PlayOneShot(burstSound);
                break;
            default:
                audioSource.clip = idleSound;
                audioSource.loop = true;
                audioSource.Play();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (state != (int)LifeStateEnum.Die && targetObject == null)
        {
            if(state != (int)LifeStateEnum.Warning)
            {
                StartCoroutine(warningStateRoutine());
            }
            targetObject = other.gameObject;
            if (preBurstRoutine == null)
                preBurstRoutine = StartCoroutine(preBurstCoroutine());
        }
        
    }

     private void OnTriggerExit(Collider other)
    {
        if (state != (int)LifeStateEnum.Die)
        {
            if (targetObject == other.gameObject)
            {
                targetObject = null;
            }
            if (preBurstRoutine != null)
            {
                StopCoroutine(preBurstRoutine);
                preBurstRoutine = null;
            }
                
        }
    }

    IEnumerator preBurstCoroutine()
    {
        yield return new WaitForSeconds(preBurstTimeoutSeconds);
        if (targetObject != null)
        {
            StartCoroutine(burstCoroutine());
        }
    }

    IEnumerator burstCoroutine()
    {
        yield return new WaitForSeconds(burstTimeoutSeconds);
        StartCoroutine(deathStateRoutine());
    }

    IEnumerator warningStateRoutine()
    {
        state = (int)LifeStateEnum.Warning;
        yield return new WaitForSeconds(warningCooldownSeconds);

        if (targetObject != null)
        {
            StartCoroutine(warningStateRoutine());
        }
        else
        {
            state = (int)LifeStateEnum.Idle;
        }
    }

    IEnumerator deathStateRoutine()
    {
        state = (int)LifeStateEnum.Die;
        StopAllCoroutines();
        sporeParticle.Play(true);
        warningCollider.enabled = false;
        targetObject = null;
        yield return null;
    }
}
