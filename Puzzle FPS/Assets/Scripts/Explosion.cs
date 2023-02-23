using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] int PushbackAmount;

    public int explosionDamage;
    GameObject affected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            affected = other.gameObject;
            EnemyAI enemyAI = affected.GetComponent<EnemyAI>();

            enemyAI.PushbackDir((enemyAI.transform.position - transform.position).normalized * PushbackAmount);
            enemyAI.TakeDamage(explosionDamage);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            affected = other.gameObject;
            EnemyAI enemyAI = affected.GetComponent<EnemyAI>();

            enemyAI.PushbackDir((enemyAI.transform.position - transform.position).normalized * PushbackAmount);
            enemyAI.TakeDamage(explosionDamage);
        }
    }
}
