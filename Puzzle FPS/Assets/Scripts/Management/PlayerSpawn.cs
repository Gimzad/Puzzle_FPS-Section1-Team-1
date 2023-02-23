using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Vector3 GetPlayerPosition()
    {
        return new Vector3(transform.position.x, transform.position.y + 1.5f,
transform.position.z);

    }
}
