using System.Collections;
using UnityEngine;

public class Bullet : MonoCache
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    private Vector3 direction = Vector3.right;

    private void OnEnable()
    {
        speed = 15;
        rb = GetComponent<Rigidbody2D>();
        MonoCache.allBullets.Add(this);
    }

    private void OnDisable()
    {
        MonoCache.allBullets.Remove(this);
    }

    private void Start()
    {
        StartCoroutine(Death(2f));
    }

    protected override void OnFixedTick()
    {
        Move();
    }

    public void Move()
    {
        rb.velocity = direction * speed;
    }

    public void SetDirection(Vector3 d)
    {
        direction = d;
    }

    private IEnumerator Death(float delay)
    {
        yield return new WaitForSeconds(delay);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        //Spawner.unusedBullets.Add(this);
        //gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy") || coll.CompareTag("Player"))
        {
            coll.gameObject.GetComponent<CharacterActions>().TakeDamage();
            Destroy(this.gameObject);
        }
    }
}
