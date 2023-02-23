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
            SetPlayerPosition();
        }
    }
    public Vector3 SetPlayerPosition()
    {
        GameManager.Instance.PlayerInstance.transform.rotation = transform.rotation;
        return new Vector3(transform.position.x, transform.position.y + 1.5f,
transform.position.z);

    }
}
