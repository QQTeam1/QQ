using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    //Projecttile to spawn
    [SerializeField] public GameObject projectile;

    //Where to spawn the ProjectTile
    [SerializeField] public Transform spawnLocation;

    // Rotation of Projecttole on Spawn
    [SerializeField] public Quaternion spawnRotation;
    [SerializeField] public DetectionZone detectionZone;
    [SerializeField] public float spawnTime = 0f;

    [SerializeField]  private float timeSinceSpawned = 0.5f;
   
    void Update()
    {
        if (detectionZone.detectedObjs.Count > 0)
        {
            timeSinceSpawned += Time.deltaTime;
            if (timeSinceSpawned >= spawnTime)
            {
                Instantiate(projectile, spawnLocation.position, spawnRotation);
                timeSinceSpawned = 0f;
            }
        }
        else
        {
            timeSinceSpawned = 0f;
        }
    }
}
