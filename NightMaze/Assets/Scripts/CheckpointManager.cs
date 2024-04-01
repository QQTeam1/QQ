using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour, IDataPersistance
{
    [SerializeField] Vector3 respawnPoint;

    public Vector3 RespawnPoint { get; }

    public void SetCheckpoint(Vector3 position)
    {
        respawnPoint = position;
    }

    public void RespawnNow()
    {
        transform.position = respawnPoint;
    }

    public void LoadData(GameData data)
    {
        respawnPoint = data.respawnPos;
    }

    public void SaveData(ref GameData data)
    {
        data.respawnPos = respawnPoint;
    }
}
