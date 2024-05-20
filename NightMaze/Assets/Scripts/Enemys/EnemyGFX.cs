using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    [SerializeField] private AIPath aIPath;
    [SerializeField] private Animator animator;


    void Start()
    {

    }


    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(aIPath.desiredVelocity.x) + Mathf.Abs(aIPath.desiredVelocity.y));

        if (aIPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (aIPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
