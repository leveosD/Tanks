using System;
using UnityEngine;

public class TilemapChecker : MonoBehaviour
{
    public static event Action TilemapExit;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TilemapExit?.Invoke();
        }
    }
}
