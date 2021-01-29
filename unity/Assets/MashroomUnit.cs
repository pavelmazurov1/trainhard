using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashroomUnit : MonoBehaviour
{
    enum LifeStateEnum
    {
        Idle = 0,
        Warning,
        Die
    }

    public int state = 0;
    public Animator unitAnimator;
    public float warningStateSeconds = 1.5f;
    public Collider warningCollider;
    public ParticleSystem sporeParticle;

    private GameObject collidedObject = null;

    // Start is called before the first frame update
    void Start()
    {
        state = (int)LifeStateEnum.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        unitAnimator.SetInteger("state", state);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter");
        if(state != (int)LifeStateEnum.Die && state != (int)LifeStateEnum.Warning)
        {
            StartCoroutine(warningStateRoutine());
        }
        collidedObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("OnTriggerExit");
        if (collidedObject == other.gameObject)
        {
            collidedObject = null;
        }
    }

    IEnumerator warningStateRoutine()
    {
        state = (int)LifeStateEnum.Warning;
        unitAnimator.SetInteger("state", state);
        yield return new WaitForSeconds(warningStateSeconds);
        if(collidedObject != null)
        {
            StartCoroutine(deathStateRoutine());
        }
        else
        {
            state = (int)LifeStateEnum.Idle;
            unitAnimator.SetInteger("state", state);
        }
    }

    IEnumerator deathStateRoutine()
    {
        sporeParticle.Play(true);
        state = (int)LifeStateEnum.Die;
        unitAnimator.SetInteger("state", state);
        warningCollider.enabled = false;
        yield return null;
    }
}
