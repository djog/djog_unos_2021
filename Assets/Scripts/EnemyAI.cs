using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    public float nextWaypointDist = 1.2f;

    [Header("Combat")]
    public Weapon weapon;
    public GameObject BulletPrefab;
    public float spawnOffset = 1.0f;
    public float spawnZ = 10.0f;

    private float fireTimer;


    private Rigidbody2D rb;
    private Transform target;
    private Seeker seeker;
    private Path path;
    private bool reachedDestination;
    private int currentWaypoint;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        target = GameManager.GetTarget();

        // Reapeat on seperate thread to safe performance
        InvokeRepeating("UpdatePath", 0, 0.2f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && target != null)
        {
            seeker.StartPath(transform.position, target.position, FoundPath);
        }
    }

    public void FoundPath(Path p)
    {
        if (!target)
        {
            return;
        }
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float degrees = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            weapon.transform.rotation = Quaternion.Euler(0.0f, 0.0f, degrees);
            // Combat
            if (fireTimer > 0.0f)
            {
                fireTimer -= Time.deltaTime;
            }
            if (fireTimer <= 0.0)
            {
                Fire(direction);
            }
        }
    }

    void Fire(Vector2 direction)
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector3 spawnPos = currentPosition + direction * spawnOffset;

        RaycastHit2D hitInfo = Physics2D.Raycast(spawnPos, direction);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.transform == target.transform)
            {
                fireTimer = weapon.fireDelay;
                float degrees = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                var bullet = Instantiate(weapon.bulletPrefab, weapon.bulletSpawn.position, weapon.bulletSpawn.rotation);
            }
        }
    }

    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedDestination = true;
            return;
        }
        else
        {
            reachedDestination = false;
        }

        Vector2 waypointTarget = (Vector2)path.vectorPath[currentWaypoint];
        Vector2 direction = (waypointTarget - rb.position).normalized;
        Vector2 force = direction * moveSpeed * Time.fixedDeltaTime;
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDist)
        {
            currentWaypoint++;
        }
    }
}
