using UnityEngine;

public class GlobalUpdate : MonoBehaviour
{
    void Update()
    {
        for (int i = 0; i < MonoCache.allEnemies.Count; i++)
        {
            MonoCache.allEnemies[i].Tick();
        }
        
        for (int i = 0; i < MonoCache.allBursts.Count; i++)
        {
            MonoCache.allBursts[i].Tick();
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < MonoCache.allBullets.Count; i++)
        {
            if(MonoCache.allBullets[i].GetComponent<Rigidbody2D>())
                MonoCache.allBullets[i].FixedTick();
        }

        for (int i = 0; i < MonoCache.allEnemies.Count; i++)
        {
            MonoCache.allEnemies[i].FixedTick();
        }
    }
}
