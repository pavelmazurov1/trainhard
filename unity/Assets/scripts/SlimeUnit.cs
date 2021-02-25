using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeUnit : MonoBehaviour
{
    enum StateEnum
    {
        None = -1,
        Idle,
        Attack
    }

    public Animator animator = null;
    public AudioSource audioSource = null;

    public AudioClip attackSound;
    public AudioClip idleSound;
    public float attackCooldown = 3f;
    public float damagePerSecond = 0.1f;
    public int state
    {
        get { return _state; }
        set { 
            if(_state != value)
            {
                _state = value;
                onStateChanged();
            } 
        }
    }
    private int _state = (int)StateEnum.None;

    private HashSet<GameObject> targets;
    private Coroutine cooldownCoroutine = null;
    private Coroutine doDamageCoroutine = null;

    private void Start()
    {
        targets = new HashSet<GameObject>();
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        state = (int)StateEnum.Idle;
    }

    private void onStateChanged()
    {
        animator.SetInteger("state", state);

        if(state == (int)StateEnum.Attack)
        {
            audioSource.loop = true;
            audioSource.clip = attackSound;
            audioSource.Play();
        }
        else
        {
            audioSource.loop = true;
            audioSource.clip = idleSound;
            audioSource.Play();
        }
    }

    private void startCooldown()
    {
        if(cooldownCoroutine == null)
        {
            cooldownCoroutine = StartCoroutine(cooldown());
        }
    }

    private void stopCooldown()
    {
        if(cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
            cooldownCoroutine = null;
        }
    }

    private void startDoDamage()
    {
        if(doDamageCoroutine == null)
        {
            doDamageCoroutine = StartCoroutine(doDamage());
        }
    }

    private void stopDoDamage()
    {
        if (doDamageCoroutine != null)
        {
            StopCoroutine(doDamageCoroutine);
            doDamageCoroutine = null;
        }
    }

    private IEnumerator doDamage()
    {
        //Debug.Log("start do damage");
        state = (int)StateEnum.Attack;
        while (true)
        {
            if (targets.Count == 0)
            {
                startCooldown();
                break;
            }
            foreach (GameObject go in targets)
            {
                //Debug.Log("do damage!");
                var damagable = go.GetComponent<IDamagable>();
                if(damagable != null)
                {
                    damagable.applyDamage(damagePerSecond);
                }
            }
            yield return new WaitForSeconds(1f);
        }
        //Debug.Log("end do damage");
        doDamageCoroutine = null;
    }

    private IEnumerator cooldown()
    {
        //Debug.Log("start cooldown");
        state = (int)StateEnum.Attack;
        float spendTime = 0f;
        while (true)
        {
            yield return null;

            if(targets.Count > 0)
            {
                startDoDamage();
                break;
            }
            else
            {
                spendTime += Time.deltaTime;
                if (spendTime > attackCooldown)
                {
                    state = (int)StateEnum.Idle;
                    break;
                }
            }
        }
        //Debug.Log("end cooldown");
        cooldownCoroutine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        targets.Add(other.gameObject);
        startDoDamage();
    }

    private void OnTriggerExit(Collider other)
    {
        targets.Remove(other.gameObject);
    }

}
