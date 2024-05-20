using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidEnemyy : MonoBehaviour
{
    public GameObject player;
    public float speed;

    void Start()
    {

    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if (transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    [SerializeField] public int damage;
    public Health playerHealth;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Hero")
        {
            playerHealth.TakeDamage(this.damage);
        }
    }
}
