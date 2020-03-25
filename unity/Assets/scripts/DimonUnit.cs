using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimonUnit : MonoBehaviour
{
    public AudioClip[] randomFrases;

    public float maxFraseRatePerSecond = 0.1f;
    public float maxLifeTimeSeconds = 60;

    public GameObject playerUnit;
    
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(randomFraseRoutine());
        StartCoroutine(timeoutDeathRoutine());
        StartCoroutine(findPlayerUnit());
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

    IEnumerator timeoutDeathRoutine()
    {
        yield return new WaitForSeconds(maxLifeTimeSeconds);
        Destroy(gameObject);
    }

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
        if (playerUnit)
        {
            playerPos = playerUnit.transform.position;
            playerPos.y = 0;
            transform.LookAt(playerPos);
        }
    }
}
