using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUnitSpawner : MonoBehaviour
{
    public GameObject[] spawnUnitPrefabs;
    public float spawnRateInSecond = 0.1f;
    public GameObject playerUnit;
    public float maxSpawnRadius = 50;

    IEnumerator spawnUnitRoutine()
    {
        while (true)
        {
            if(spawnUnitPrefabs.Length > 0)
            {
                var prefab = spawnUnitPrefabs[Random.Range(0, spawnUnitPrefabs.Length)];
                var position = playerUnit.transform.position;
                var delta =  Random.insideUnitCircle * Random.Range(0, maxSpawnRadius);
                position.x += delta.x;
                position.z += delta.y;
                position.y = 0;
                GameObject.Instantiate(prefab, position, Quaternion.identity);
            }
            yield return new WaitForSeconds(1 / spawnRateInSecond);
        }
    }

    void Start()
    {
        StartCoroutine(spawnUnitRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
