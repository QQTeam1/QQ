using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Vector3 respawnPoint;

    private void Start()
    {
        respawnPoint = transform.position;
    }

    public void SetCheckpoint(Vector3 position)
    {
        respawnPoint = position;
    }

    public void RespawnNow()
    {
        transform.position = respawnPoint;
    }
}
