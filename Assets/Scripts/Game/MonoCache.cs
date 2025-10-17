using System.Collections.Generic;
using UnityEngine;

public class MonoCache : MonoBehaviour
{
    public static List<MonoCache> allBullets = new List<MonoCache>(16);
    public static List<MonoCache> allEnemies = new List<MonoCache>(16);
    public static List<MonoCache> allBursts = new List<MonoCache>(16);

    public void Tick() => OnTick();
    public void FixedTick() => OnFixedTick();
    
    protected virtual void OnTick() {}
    protected virtual void OnFixedTick() {}
}
