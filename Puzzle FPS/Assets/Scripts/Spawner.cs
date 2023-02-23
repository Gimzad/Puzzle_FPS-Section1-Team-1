using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] int spawnMaxNum;
    [SerializeField] int timer;
    [SerializeField] Transform[] spawnPos;

    bool isSpawning;
    bool playerInRange;

    public bool AreaSpawner;

    bool locationSpawners;
    int enemiesSpawned;

    private void Awake()
    {
        enemiesSpawned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemies();
    }
    public void SpawnEnemies()
    {
        if (AreaSpawner)
        {
            if (playerInRange && !isSpawning && enemiesSpawned < spawnMaxNum)
            {
                StartCoroutine(spawn());
            }
        } else
        {
            if (!isSpawning && enemiesSpawned < spawnMaxNum)
            {
                StartCoroutine(spawn());
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    IEnumerator spawn()
    {
        isSpawning = true;
        Instantiate(enemy, spawnPos[0 + enemiesSpawned].position, enemy.transform.rotation);
        enemiesSpawned++;
        yield return new WaitForSeconds(timer);
        isSpawning = false;
    }
}
