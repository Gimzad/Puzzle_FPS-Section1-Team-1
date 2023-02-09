using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour, IDamage
{
    [Header("-----Components-----")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;

    [Header("-----Stats-----")]
    [SerializeField] int hp;
    [SerializeField] int playerFaceSpeed;

    [Header("-----Weapon-----")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPos;
    [SerializeField] float shootRate;
    [SerializeField] float bulletSpeed;



    Vector3 playerDir;
    bool isShooting;
    bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.UpdateEnemyCount(1);
    }

    // Update is called once per frame
    void Update()
    {
        playerDir = GameManager.Instance.PlayerController().transform.position - transform.position;

        if (playerInRange)
        {
            if (!isShooting)
            {
                StartCoroutine(Shoot());
            }
        }
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            FacePlayer();
        }

        agent.SetDestination(GameManager.Instance.PlayerController().transform.position);
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        StartCoroutine(FlashDamage());
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator FlashDamage()
    {
        model.material.color = Color.red;

        yield return new WaitForSeconds(0.15f);

        model.material.color = Color.white;

    }

    void FacePlayer()
    {
        Quaternion rot = Quaternion.LookRotation(playerDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerFaceSpeed);
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        GameObject bulletClone = Instantiate(bullet, shootPos.position, bullet.transform.rotation);
        bulletClone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;

        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
