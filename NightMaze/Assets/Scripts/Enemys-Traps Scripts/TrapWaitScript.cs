using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapWaitScript : MonoBehaviour
{
    public Animator trapAnimator;
    public string activateAnimationName = "Active";

    public Collider2D trapCollider;

    [SerializeField] public int damage;
    public Health playerHealth;
    private void Update()
    {
        // Перевіряємо, чи відтворюється анімація, яка активує пастку
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
