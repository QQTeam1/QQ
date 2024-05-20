using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Health;

public class Projectile : MonoBehaviour
{
    //Constant Speed of the projectile
    [SerializeField] float moveSpeed = 2f;

    //Time until projectile expires
    [SerializeField] float timeToLive = 5f;

    private float timeSinceSpawned = 0f;
    [SerializeField] public int damage;
    
    void Update()
    {
        transform.position += moveSpeed * transform.right * Time.deltaTime;
        timeSinceSpawned += Time.deltaTime;
        if (timeSinceSpawned > timeToLive)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.gameObject.tag;
        if (tag == "Player")
        {

            GameObject playerObj = GameObject.Find("Hero");
            Health playerHealth = playerObj.GetComponent<Health>();
            playerHealth.TakeDamage(damage);
           
            Destroy(gameObject);
        }
        if (tag == "Walls")
        {
            Destroy(gameObject); 
        }
    }

}
