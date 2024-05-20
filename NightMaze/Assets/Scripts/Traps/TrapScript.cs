using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] public int damage;
    public Health playerHealth;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Hero")
        {
            // todo: animation
            playerHealth.TakeDamage(this.damage);
        }
    }
}
