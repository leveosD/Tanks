using UnityEngine;

public interface IControllable
{
    void Move(Vector2 position);
    void Aim(Vector2 direction);
    void Shoot();
}
