using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesAllTime3 : MonoBehaviour
{
    public Animator trapAnimator;
    public string activateAnimationName = "ActivatedAll_3";

    public Collider2D trapCollider;

    [SerializeField] public int damage;
    public Health playerHealth;
    void Start()
    {
        playerHealth = GameObject.Find("Hero").GetComponent<Health>();
    }

    private void Update()
    {

        bool isActivateAnimationPlaying = trapAnimator.GetCurrentAnimatorStateInfo(0).IsName(activateAnimationName);

        if (isActivateAnimationPlaying && IsPlayerUnderTrap())
        {
            playerHealth.TakeDamage(this.damage);
        }
    }
    private bool IsPlayerUnderTrap()
    {

        Collider2D[] colliders = Physics2D.OverlapBoxAll(trapCollider.bounds.center, trapCollider.bounds.size, 0f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.name == "Hero")
            {
                return true;
            }
        }

        return false;
    }
}
