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

    [Header("Components")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;

    [Header("Stats")]
    [SerializeField] int hp;
    [SerializeField] int playerFaceSpeed;
    [SerializeField] float speed;
    [SerializeField] int viewAngle;

    [Header("Weapon")]
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
    [Header("Aiming")]
    [SerializeField] int shootAngle;

    [Header("Pathing")]

    [SerializeField] int waitTime;
    [SerializeField] int roamDist;


    [Header("-----Animation-----")]
    public float DeathTimer;
    public float AttackTimer;
    public bool Alive;
    public bool inAttackRange;

    Vector3 playerDir;
    bool playerInVisionRange;

    bool isShooting;
    float angleToPlayer;
    Vector3 startingPos;
    Vector3 pushback;
    bool destinationChosen;
    float stoppingDistOrig;



    void Awake()
    {
        Alive = true;
        agent.speed = speed;
        _ragdollRigidBodies = GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
    }
    void Start()
    {
        startingPos = transform.position;
        stoppingDistOrig = agent.stoppingDistance;
    }
    void Update()
    {
        if (Alive)
        {
            if (playerInVisionRange && CanSeePlayer())
            {
                if (!CanSeePlayer())
                {
                    StartCoroutine(Roam());
                }
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
            else if (agent.destination != GameManager.Instance.PlayerScript().transform.position)
            {
                StartCoroutine(Roam());
            }
        }
        else
        {
            StartCoroutine(DestroyEnemy());
        }
        
    }
    IEnumerator Roam()
    {
        if (!destinationChosen && agent.remainingDistance < 0.1f)
        {
            destinationChosen = true;
            agent.stoppingDistance = 0;
            yield return new WaitForSeconds(waitTime);
            destinationChosen = false;

            Vector3 randDir = Random.insideUnitSphere * roamDist;
            randDir += startingPos;

            NavMeshHit hit;
            NavMesh.SamplePosition(randDir, out hit, roamDist, NavMesh.AllAreas);

            agent.SetDestination(hit.position);
        }
    }
    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(DeathTimer);
        Destroy(gameObject);
    }

    bool CanSeePlayer()
    {
        playerDir = (GameManager.Instance.PlayerScript().transform.position - headPos.position).normalized;
        angleToPlayer = Vector3.Angle(new Vector3(playerDir.x, 0, playerDir.z), transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(headPos.position, playerDir, out hit))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= viewAngle)
            {
                agent.stoppingDistance = stoppingDistOrig;
                agent.SetDestination(GameManager.Instance.PlayerScript().transform.position);
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    FacePlayer();

                }
                if (!isShooting && angleToPlayer <= shootAngle)
                {
                    StartCoroutine(Shoot());
                }
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
        agent.stoppingDistance = 0;
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
        playerDir.y = 0;
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
        animator.SetTrigger("attacking");

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
            agent.stoppingDistance = 0;
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

    public void PushbackDir(Vector3 dir)
    {
        pushback += dir;
    }

    public void ExplosionForce()
    {
        Rigidbody thisBody = GetComponent<Rigidbody>();
        thisBody.AddForce(pushback);
    }
}
