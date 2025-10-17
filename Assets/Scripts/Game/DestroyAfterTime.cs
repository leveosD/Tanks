using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private float LIFETIME;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        LIFETIME = lifeTime;
    }

    private void OnEnable()
    {
        //MonoCache.allBursts.Add(this);
        animator.Play("tank_explosion");
    }

    /*private void OnDisable()
    {
        lifeTime = LIFETIME;
        //MonoCache.allBursts.Remove(this);
        /*Spawner.unusedBursts.Add(this);
        Debug.Log(gameObject.name + " is added to Unused");
        for (int i = 0; i < Spawner.unusedBursts.Count; i++)
        {
            Debug.Log(i + ") " + Spawner.unusedBursts[i]);
        }#1#
    }*/

    protected void Update()
    {
        if (lifeTime > 0)
            lifeTime -= Time.deltaTime;
        else
        {
            Destroy(this.gameObject);
            //gameObject.SetActive(false);
        }
    }
}
