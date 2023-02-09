using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IDamage
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
    [SerializeField] int shootDist;



    Vector3 playerDir;
    bool isShooting;
    bool playerInVisionRange;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.UpdateEnemyCount(1);
    }

    // Update is called once per frame
    void Update()
    {
        playerDir = GameManager.Instance.PlayerController().transform.position - transform.position;

        if (playerInVisionRange)
        {
            agent.SetDestination(GameManager.Instance.PlayerController().transform.position);
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                FacePlayer();
            }

            if (!isShooting && agent.remainingDistance <= shootDist)
            {
                StartCoroutine(Shoot());
            }
        }
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
            playerInVisionRange = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInVisionRange = false;
        }
    }
}
