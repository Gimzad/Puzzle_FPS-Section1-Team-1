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
    int enemiesSpawned;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.UpdateEnemyCount(spawnMaxNum);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && !isSpawning && enemiesSpawned < spawnMaxNum)
        {
            StartCoroutine(spawn());
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("SomethingEntered");
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    IEnumerator spawn()
    {
        Debug.Log("Spawnfunction");
        isSpawning = true;
        Instantiate(enemy, spawnPos[Random.Range(0, spawnPos.Length)].position, enemy.transform.rotation);
        enemiesSpawned++;
        yield return new WaitForSeconds(timer);
        isSpawning = false;
    }
}
