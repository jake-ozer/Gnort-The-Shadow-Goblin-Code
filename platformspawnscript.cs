using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformspawnscript : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject spawnedPlatform;
    public float minWait;
    public float maxWait;
    private bool isSpawning;
    public bool spawningTurnedOn = false;

    void Awake()
    {
        isSpawning = false;
    }

    void Update()
    {
        if (spawningTurnedOn == true)
        {
            if (!isSpawning)
            {
                float timer = Random.Range(minWait, maxWait);
                Invoke("SpawnPlatform", timer);
                isSpawning = true;
            }
        }
    }

    void SpawnPlatform()
    {
        Instantiate(spawnedPlatform, spawnPoint.position, spawnPoint.rotation);
        isSpawning = false;
    }

    public void InitiateSpawn()
    {
        Debug.Log("the platforms are spawning");
        spawningTurnedOn = true;
    }
}
