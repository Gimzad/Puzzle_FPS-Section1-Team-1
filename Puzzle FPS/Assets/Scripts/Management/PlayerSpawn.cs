using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Instance)
            GameManager.Instance.PlayerSpawnPos = gameObject;
    }
    private void Start()
    {
        if (GameManager.Instance.PlayerInstance)
        {
            if (!GameManager.Instance.PlayerSpawnPos)
                GameManager.Instance.PlayerInstance.transform.position = GetPlayerPosition();
        }
    }
    public Vector3 GetPlayerPosition()
    {
        return new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);

    }
}
