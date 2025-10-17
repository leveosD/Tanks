using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    private Vector3 minScale = new Vector3(0.9f, 0.9f, 1);
    private Vector3 maxScale = new Vector3(1.1f, 1.1f, 1);

    [SerializeField] private float defaultFrequancy;
    [SerializeField] private float frequancy;
    [SerializeField] private bool state;

    private void Start()
    {
        frequancy = defaultFrequancy;
    }

    void Update()
    {
        if (frequancy > 0)
            frequancy -= Time.deltaTime;
        else
        {
            state = !state;
            frequancy = defaultFrequancy;
        }
        if (state)
            transform.localScale = Vector3.Lerp(transform.localScale, maxScale, Time.deltaTime);
        else
            transform.localScale = Vector3.Lerp(transform.localScale, minScale, Time.deltaTime);
    }
}
