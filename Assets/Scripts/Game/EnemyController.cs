using System;
using UnityEngine;

public class EnemyController : MonoCache
{
    [SerializeField] GameObject player;
    private CharacterActions actions;
    
    [SerializeField] private Vector2 direction;
    [SerializeField] private Vector2 rotation;

    [SerializeField] private float radius;
    private float distance;
    
    private LayerMask playerMask;
    private LayerMask ignoreMask;

    private float delay;

    private Collider2D playerCollider;
    private RaycastHit2D hit;
    
    private void Awake()
    {
        actions = GetComponent<CharacterActions>();
        playerMask = 1 << LayerMask.NameToLayer("Player");
        ignoreMask = ~(1 << LayerMask.NameToLayer("Enemy")) & ~(1 << LayerMask.NameToLayer("Projectile"));
    }

    private void OnEnable()
    {
        allEnemies.Add(this);
    }
    
    private void OnDisable()
    {
        allEnemies.Remove(this);
    }

    private void OnDestroy()
    {
        if (!isActiveAndEnabled)
        {
            Spawner.unusedEnemies.Remove(this);
        }
    }

    private void Start()
    {
        var playerInput = FindObjectOfType<PlayerInput>();
        if(playerInput)
            player = playerInput.gameObject;
        delay = 0;
    }

    protected override void OnFixedTick()
    {
        actions.Aim(rotation);
        if (distance <= radius)
            Shoot();
        actions.Move(direction);
    }

    protected override void OnTick()
    {
        if (delay > 0)
            delay -= Time.deltaTime;

        if (!player)
        {
            var playerInput = FindObjectOfType<PlayerInput>();
            if (playerInput)
            {
                player = playerInput.gameObject;
            }
            else
                return;
        }

        rotation = (player.transform.position - transform.position).normalized;
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > radius)
        {
            direction = rotation;
        }
        else
            direction = Vector2.zero;
        
        if (distance > 50)
        {
            FakeDestroy();
        }
    }

    private void Shoot()
    {
        if (delay <= 0)
        {
            float angle = Mathf.Atan2(Mathf.Sin(rotation.y), Mathf.Cos(rotation.x)) * Mathf.Rad2Deg;
            if(Mathf.Abs(transform.eulerAngles.z - angle) > 4)
            {
                actions.Shoot();
                delay = 2;
            }
        }
    }

    public void FakeDestroy()
    {
        Spawner.unusedEnemies.Add(this);
        this.gameObject.SetActive(false);
    }

    /*private void OnDestroy()
    {
        ScoreUp?.Invoke();
    }*/

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
        float rightAngle = (transform.eulerAngles.z - angle / 2) * Mathf.Deg2Rad;
        float leftAngle = (transform.eulerAngles.z + angle / 2) * Mathf.Deg2Rad;
        Vector3 rightLine = new Vector3(Mathf.Cos(rightAngle), Mathf.Sin(rightAngle), 0).normalized * radius;
        Vector3 leftLine = new Vector3(Mathf.Cos(leftAngle), Mathf.Sin(leftAngle), 0).normalized * radius;
        Gizmos.DrawLine(transform.position, (transform.position + rightLine));
        Gizmos.DrawLine(transform.position, (transform.position + leftLine));

        Gizmos.DrawLine(transform.position, (transform.position + (Vector3)rotation * radius));
    }*/
}
