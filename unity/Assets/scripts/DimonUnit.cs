using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimonUnit : MonoBehaviour, IDamagable
{
    public AudioClip[] randomFrases;
    

    public AudioClip closeFrase;
    public float closeFraseDistance = 3;
    

    public AudioClip incomingDamageFrase;
    private Coroutine incomingDamageFraseCoroutine = null;

    public float maxFraseRatePerSecond = 0.1f;
    public float maxLifeTimeSeconds = 60;

    public float moveSpeed = 1f;
    public float stopMoveDistance = 2f;

    public float health = 100f;
    public AudioClip deathFrase;

    public GameObject playerUnit;
    
    private AudioSource audioSource;

    

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        randomFraseCoroutine = StartCoroutine(randomFraseRoutine());
        timeoutDeathCoroutine = StartCoroutine(timeoutDeathRoutine());
        StartCoroutine(findPlayerUnit());
        closeFraseCoroutine = StartCoroutine(closeFraseRoutine());
        lookAtPlayerCoroutine = StartCoroutine(lookAtPlayerRoutine());
    }

    private Coroutine lookAtPlayerCoroutine;
    IEnumerator lookAtPlayerRoutine()
    {
        while(true)
        {
            if (playerUnit)
            {
                playerPos = playerUnit.transform.position;
                playerPos.y = 0;
                transform.LookAt(playerPos);

                if((transform.position - playerPos).magnitude > stopMoveDistance)
                {
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                }

            }
            yield return null;
        }
    }

    IEnumerator deathDropAnimationRoutine()
    {
        while(true)
        {
            transform.Rotate(-100 * Time.deltaTime, 0, 0);
            yield return null;
            if (transform.up.y <= 0.1) break;
        }
    }

    IEnumerator deathRoutine()
    {
        StopCoroutine(randomFraseCoroutine);
        StopCoroutine(timeoutDeathCoroutine);
        StopCoroutine(closeFraseCoroutine);
        StopCoroutine(lookAtPlayerCoroutine);

        audioSource.Stop();
        audioSource.PlayOneShot(deathFrase);
        StartCoroutine(deathDropAnimationRoutine());

        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    IEnumerator incomingDamageFraseRoutine()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(incomingDamageFrase);
        yield return new WaitForSeconds(1);
        incomingDamageFraseCoroutine = null;
    }

    private Coroutine closeFraseCoroutine = null;
    IEnumerator closeFraseRoutine()
    {
        while (true)
        {
            if ((playerPos - transform.position).magnitude <= closeFraseDistance)
            {
                audioSource.PlayOneShot(closeFrase);
                yield return new WaitForSeconds(3);
            }
            else
                yield return null;
        }
    }

    IEnumerator findPlayerUnit()
    {
        while(true)
        {
            var sceneDataObject = GameObject.FindGameObjectWithTag("SceneData");
            if (sceneDataObject)
            {
                playerUnit = sceneDataObject.GetComponent<PlayerData>().PlayerUnit;
                if (playerUnit)
                    break;
            }
            yield return null;
        }
    }

    private Coroutine timeoutDeathCoroutine;
    IEnumerator timeoutDeathRoutine()
    {
        yield return new WaitForSeconds(maxLifeTimeSeconds);
        Destroy(gameObject);
    }

    private Coroutine randomFraseCoroutine;
    IEnumerator randomFraseRoutine()
    {
        while (true)
        {
            if(randomFrases.Length > 0)
            {
                audioSource.PlayOneShot(randomFrases[Random.Range(0, randomFrases.Length)]);
            }
            yield return new WaitForSeconds(Random.Range(0, 1f / maxFraseRatePerSecond));
        }
    }

    private Vector3 playerPos;
    // Update is called once per frame
    void Update()
    {
        
    }

    void IDamagable.applyDamage(float value)
    {
        if(health > 0)
        {
            health -= value;
            if (health <= 0)
            {
                StartCoroutine(deathRoutine());
            }
            else if (incomingDamageFraseCoroutine == null)
            {
                incomingDamageFraseCoroutine = StartCoroutine(incomingDamageFraseRoutine());
            }
        }
    }
}
