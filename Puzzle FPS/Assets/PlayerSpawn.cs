using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.PlayerSpawnPos = gameObject;
    }
    private void Start()
    {
        GameManager.Instance.PlayerInstance.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f,
transform.position.z);
    }
}
