using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using System.Linq;
using System.Runtime.CompilerServices;

public class EnemyAI : MonoBehaviour, IDamage
{

    private Rigidbody[] _ragdollRigidBodies;
    EnemyAI enemyAI;

    [Header("-----Components-----")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;

    [Header("-----Stats-----")]
    [SerializeField] int hp;
    [SerializeField] int playerFaceSpeed;
    [SerializeField] float speed;

    [Header("-----Weapon-----")]
    [SerializeField]
    bool typeSniper;
    [SerializeField]
    bool typeMelee;
    [SerializeField] Transform headPos;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPos;
    [SerializeField] float shootRate;
    [SerializeField] float bulletSpeed;
    [SerializeField] int shootDist;
    [SerializeField] int viewAngle;

    float angleToPlayer;
    Vector3 playerDir;
    bool isShooting;
    bool playerInVisionRange;

    [Header("-----Animation-----")]
    public float DeathTimer;
    public float AttackTimer;
    public bool Alive;
    public bool inAttackRange;

    void Awake()
    {
        Alive = true;
        agent.speed = speed;
        _ragdollRigidBodies = GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
    }
    void Update()
    {
        if (Alive)
        {
            Debug.Log(inAttackRange);
            if (playerInVisionRange && CanSeePlayer())
            {
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    FacePlayer();
                }
                if (inAttackRange)
                {
                    if (!isShooting)
                    {
                        StartCoroutine(Shoot());
                    }
                }
            }
        }
        else
        {
            StartCoroutine(DestroyEnemy());
        }
        
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(DeathTimer);
        Destroy(gameObject);
    }

    bool CanSeePlayer()
    {
        playerDir = GameManager.Instance.PlayerController().transform.position - headPos.position;
        angleToPlayer = Vector3.Angle(new Vector3(playerDir.x, 0, playerDir.z), transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(headPos.position, playerDir, out hit))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= viewAngle)
            {
                agent.SetDestination(GameManager.Instance.PlayerController().transform.position);
                if (Mathf.Abs(playerDir.z) <= shootDist)
                {
                    inAttackRange = true;
                }
                else
                {
                    inAttackRange = false;
                }
                return true;
            }
        }
        inAttackRange = false;
        return false;
    }
    private void LateUpdate()
    {
        UpdateAnimator();
    }
    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        StartCoroutine(FlashDamage());
        if (hp <= 0)
        {
            Alive = false;
            EnableRagdoll();
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
        StartCoroutine(Attack());
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
    IEnumerator Attack()
    {
        agent.speed = 0;
        if (inAttackRange)
            animator.SetTrigger("attacking");
        else
            yield break;

        GameObject bulletClone = Instantiate(bullet, shootPos.position, bullet.transform.rotation);

        bulletClone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        yield return new WaitForSeconds(AttackTimer);

        agent.speed = speed;
    }
    private void UpdateAnimator()
    {
        animator.SetBool("playerInVisionRange", playerInVisionRange);
        animator.SetFloat("speed", agent.speed);
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

    private void DisableRagdoll()
    {
        foreach (var rigidbody in _ragdollRigidBodies)
        {
            rigidbody.isKinematic = true;
        }
        animator.enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;

    }

    private void EnableRagdoll()
    {
        foreach (var rigidbody in _ragdollRigidBodies)
        {
            rigidbody.isKinematic = false;
        }
        animator.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
    }
}
