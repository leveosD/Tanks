using UnityEngine;

public class Follower : MonoBehaviour
{
    private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;

    private void FixedUpdate()
    {
        if(target) transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.fixedDeltaTime * speed);
        else
        {
            var playerInput = FindObjectOfType<PlayerInput>();
            if(playerInput) target = playerInput.transform;
        }
    }
}
