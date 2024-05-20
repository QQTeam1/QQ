//using UnityEngine;

//public class SpikesAnimAllTime : MonoBehaviour
//{
//    [SerializeField] public Animator trapAnimator;
//    [SerializeField] public string activateAnimationName = "Activated";
//    [SerializeField] public int damage;
//    [SerializeField] public Health playerHealth;

//    private void OnTriggerStay2D(Collider2D other)
//    {
//        // Перевіряємо, чи відтворюється анімація, яка активує пастку
//        bool isActivateAnimationPlaying = trapAnimator.GetCurrentAnimatorStateInfo(0).IsName(activateAnimationName);

//        // Перевіряємо, чи гравець знаходиться під пасткою та чи відтворюється анімація активації пастки
//        if (isActivateAnimationPlaying && other.CompareTag("Player"))
//        {
//            if (playerHealth != null)
//            {
//                playerHealth.TakeDamage(damage);
//            }
//        }
//    }
//}
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapWaitScript : MonoBehaviour
{
    public Animator trapAnimator;
    public string activateAnimationName = "Activated";

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
