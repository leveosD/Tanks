using System;
using UnityEngine;

public class CharacterActions : MonoBehaviour, IControllable, ICanBeDamaged
{
    private BoxCollider2D box;
    private Rigidbody2D rb;
    [SerializeField] protected float speed;
    
    [SerializeField] private GameObject bulletPrefab;
    private GameObject bullet;
    
    [SerializeField] private GameObject burstPrefab;
    
    private SoundController soundController;
    
    public static event Action ScoreUp;

    delegate void FakeDestroy();

    private FakeDestroy fakeDestroy;

    private Spawner spawner;
    
    private void Awake()
    {
        spawner = this.GetComponentInParent<Spawner>();
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        soundController = GetComponent<SoundController>();
        
        if (this.TryGetComponent<EnemyController>(out EnemyController enemy))
        {
            fakeDestroy = enemy.FakeDestroy;
        }
        else if (TryGetComponent(out PlayerInput player))
        {
            fakeDestroy = player.FakeDestroy;
        }
        else
        {
            fakeDestroy = () => { };
        }
    }

    public void Move(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

    public void Aim(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void Shoot()
    {
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0).normalized;
        bullet = Instantiate(bulletPrefab, this.transform.position + offset, this.transform.rotation);
        bullet.GetComponent<Bullet>().SetDirection(offset);
        //spawner.CreateBullet(transform.position, offset, transform.rotation);
        soundController.Shoot();
    }
    
    public void TakeDamage()
    {
        ScoreUp?.Invoke();
        Instantiate(burstPrefab, transform.position, transform.rotation);
        //spawner.CreateBurst(this.transform);
        fakeDestroy();
    }
}
