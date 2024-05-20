using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem careful;
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 200f;
    [SerializeField] private float nextWaypointDistance = 0.2f;
    [SerializeField] private Transform enemyGFX;
    [SerializeField] private Animator animator;

    [SerializeField] private Vector2 force = new Vector2(0,0);
    [SerializeField] private int currentWaypoint = 0;
    [SerializeField] private bool reachEndOfPath = false;

    [SerializeField] private Pathfinding.Path path;
    [SerializeField] private Seeker seeker;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ExtraCollision FindCollider;
    [SerializeField] private ExtraCollision LostCollider;
    [SerializeField] private ExtraCollision KillCollider;
    [SerializeField] private Vector2 StartPosition, LastPosition, directionOfCheckLine, patroolPoint;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private int state = 1;
    [SerializeField] private bool isPatrooling, isFolowing, isReturning, isWatching, isStartFolowing;
    [SerializeField] private float distanceOfPatrool = 5f;


    [SerializeField] private int damage;
    private Health playerHealth;
    
    void Start()
    {
        state = 1;
        StartPosition = transform.position;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Hero").GetComponent<Transform>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        playerHealth = GameObject.Find("Hero").GetComponent<Health>();
        damage = 1;

        FindCollider = transform.Find("Collider2DFind").GetComponent<ExtraCollision>();
        FindCollider.OnTriggerEnter2D_Action += FindCollider_OnTriggerEnter2D;
        FindCollider.OnTriggerStay2D_Action += FindCollider_OnTriggerStay2D;

        LostCollider = transform.Find("Collider2DLost").GetComponent<ExtraCollision>();
        LostCollider.OnTriggerExit2D_Action += LostCollider_OnTriggerExit2D;

        KillCollider = transform.Find("Collider2DKill").GetComponent<ExtraCollision>();
        KillCollider.OnTriggerEnter2D_Action += KillCollider_OnTriggerEnter2D;

        wallLayer = LayerMask.GetMask("Wall");
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && state == 1 && isFolowing)
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
        else if (seeker.IsDone() && state == 1 && isReturning)
        {
            seeker.StartPath(rb.position, StartPosition, OnPathComplete);
        }
        else if (seeker.IsDone() && state == 1 && isPatrooling)
        {
            seeker.StartPath(rb.position, patroolPoint, OnPathComplete);
        }
    }

    void OnPathComplete (Pathfinding.Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        if (state == 1 && isFolowing || state == 1 && isPatrooling || state == 1 && isReturning)
        {
            if (path == null)
            {
                return;
            }
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachEndOfPath = true;
                return;
            }
            else
            {
                reachEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            force = direction * speed * Time.deltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }
        if (isWatching)
        {
            CheckVector(rb.position, target.position);
        }
        if (state == 1 && !isFolowing && !isReturning && !isPatrooling)
        {
            float x = Random.Range(StartPosition.x - distanceOfPatrool, StartPosition.x + distanceOfPatrool);
            float y = Random.Range(StartPosition.y - distanceOfPatrool, StartPosition.y + distanceOfPatrool);
            patroolPoint = new Vector2(x, y);
            Vector2 box = new Vector2(0.6f, 0.6f);
            if (!Physics2D.OverlapBox(patroolPoint, box, 0, wallLayer))
            {
                isPatrooling = true;
            }
        }
        if (state == 1 && 0.15 > Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(patroolPoint.x)) && 0.15 > Mathf.Abs(Mathf.Abs(transform.position.y) - Mathf.Abs(patroolPoint.y)) && isPatrooling)
        {
            state = 0;
            isPatrooling = false;
            StartCoroutine(WaitAndGo(2, 8));
        }
        if (state == 1 && 0.15 > Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(StartPosition.x)) && 0.15 > Mathf.Abs(Mathf.Abs(transform.position.y) - Mathf.Abs(StartPosition.y)) && isReturning)
        {
            state = 0;
            isReturning = false;
            StartCoroutine(WaitAndGo(2, 8));
        }
        if (state == 1 && isStartFolowing)
        {
            state = 0;
            isStartFolowing = false;
            isFolowing = true;
            careful.Play();
            StartCoroutine(WaitAndGo(1));
        }

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y));
        if (rb.velocity.x >= 0.5f)
        {
            enemyGFX.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }
        else if (rb.velocity.x <= -0.5f)
        {
            enemyGFX.localScale = new Vector3(-0.75f, 0.75f, 0.75f);
        }
    }

    void CheckVector(Vector2 start, Vector2 end, float stepSize = 0.1f)
    {
        Vector2 direction = (end - start).normalized; 
        float distance = Vector2.Distance(start, end); 
        RaycastHit2D hit = Physics2D.Raycast(start, direction, distance, wallLayer);
        if (hit.collider == null)
        {
            Debug.DrawRay(start, direction * distance, Color.red);
            Debug.Log("See the Player and go to KILL him");
            isWatching = false;
            isPatrooling = false;
            isFolowing = false;
            //isFolowing = true;
            state = 1;
            isStartFolowing = true;
            return;
        }
        else
        {
            Debug.DrawRay(start, direction * distance, Color.yellow);
        }
    }
    IEnumerator WaitAndGo(float wait)
    {
        yield return new WaitForSeconds(wait);

        state = 1;
    }
    IEnumerator WaitAndGo(float wait, float wait2)
    {
        yield return new WaitForSeconds(Random.Range(wait, wait2));

        state = 1;
    }

    private void FindCollider_OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Hero")
        {
            print("FindCollider_OnTriggerEnter2D");
            //findPlayer = 1;
        }
    }
    private void FindCollider_OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Hero" && !isWatching && !isFolowing)
        {
            print("Watching for Player");

            isPatrooling = false;
            isWatching = true;
        }
    }

    private void LostCollider_OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Hero")
        {
            print("LostCollider_OnTriggerExit2D");

            state = 1;
            isWatching = false;
            isFolowing = false;
            isPatrooling = true;
            isReturning = false;
            LastPosition = target.position;
        }
    }
    private void KillCollider_OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Hero")
        {
            print("AAAAAAAAAA");

            isWatching = false;
            isFolowing = false;
            isPatrooling = false;
            isReturning = true;
            playerHealth.TakeDamage(damage);
            //state = 0;
            //StartCoroutine(WaitAndGo(3));
        }
    }
}
