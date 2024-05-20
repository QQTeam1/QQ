using UnityEngine;

public class SpikesWhereStands_2 : MonoBehaviour
{

    [SerializeField] public Animator trapAnimator;
    [SerializeField] public string activateAnimationName = "ActivatedStand_2";
    [SerializeField] public int damage;
    [SerializeField] public Health playerHealth;
    [SerializeField] public Collider2D trapCollider;

    void Start()
    {
        playerHealth = GameObject.Find("Hero").GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            trapAnimator.SetBool("isActivatedAnimation_2", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            trapAnimator.SetBool("isActivatedAnimation_2", false);
        }
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
