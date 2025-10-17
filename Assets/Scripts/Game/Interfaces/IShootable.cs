using UnityEngine;

public interface IShootable
{
    void Move(Vector2 direction);
    void SetDirection(Vector3 direction);
}
