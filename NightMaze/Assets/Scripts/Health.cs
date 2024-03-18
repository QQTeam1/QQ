using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    private CheckpointManager checkpoint;

    void Start()
    {
        currentHealth = maxHealth;
        checkpoint = GameObject.Find("Hero").GetComponent<CheckpointManager>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            // Oh no you'r dead

            checkpoint.RespawnNow();
            currentHealth = maxHealth;
        }
    }

    public float GetHealthProcent()
    {
        return (float)currentHealth / maxHealth;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}

