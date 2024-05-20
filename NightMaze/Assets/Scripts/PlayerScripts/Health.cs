using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{

    [SerializeField] private GameObject panel;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;
    [SerializeField] private GameObject heart4;
    [SerializeField] private GameObject heart5;
    [SerializeField] private GameObject heart6;
    [SerializeField] private GameObject heart7;
    [SerializeField] private GameObject heart8;

    private int previousHealth = 2;
    private int maxHealth = 8;
    public int currentHealth;
    public float animationDuration = 2f;
    public float blinkingDuration = 0.2f;
    private float changingAnimationDuration;
    private CheckpointManager checkpoint;
    private bool isInvulnerable = false;
    private GameObject[] hearts;
    void Start()
    {
        currentHealth = previousHealth;
        hearts = new GameObject[] { heart1, heart2, heart3, heart4, heart5, heart6, heart7, heart8};
        WriteAllHearts();
        checkpoint = GameObject.Find("Hero").GetComponent<CheckpointManager>();
        checkpoint.RespawnNow();
        panel.SetActive(false);
        changingAnimationDuration = animationDuration;
    }

    public void TakeDamage(int amount)
    {
        if (!isInvulnerable)
        {
            if (amount > currentHealth)
            {
                panel.SetActive(true);
                Time.timeScale = 0;
                currentHealth = 0;
            }
            else
            {
                currentHealth -= amount;
                StartCoroutine(InvulnerabilityCoroutine());
            }
        }
    }
    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        while (changingAnimationDuration > 0f)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkingDuration);
            changingAnimationDuration -= blinkingDuration;
        }
        spriteRenderer.enabled = true;
        changingAnimationDuration = animationDuration;
        isInvulnerable = false;
    }
    public void OnRestartButton()
    {
        panel.SetActive(false);
        checkpoint.RespawnNow();
        previousHealth = currentHealth;
        Time.timeScale = 1;
    }
    private void FixedUpdate()
    {
        WriteAllHearts();
    }
    private void WriteAllHearts()
    {
        for (int i = 0; i < currentHealth; i++)
        {
            hearts[i].SetActive(true);
        }
        for(int i = currentHealth; i < maxHealth; i++)
        {
            hearts[i].SetActive(false);
        }
    }
}