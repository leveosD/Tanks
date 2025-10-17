using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private List<AudioSource> audioSources;
    private Rigidbody2D rb;
    
    void Awake()
    {
        audioSources = GetComponents<AudioSource>().ToList();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb.velocity != Vector2.zero && !audioSources[0].isPlaying)
        {
            audioSources[0].pitch = Random.Range(0.9f, 1.1f);
            audioSources[0].Play();
        }
        else if(rb.velocity == Vector2.zero)
        {
            audioSources[0].Stop();
        }
    }

    public void Shoot()
    {
        audioSources[1].pitch = Random.Range(0.9f, 1.1f);
        audioSources[1].Play();
    }
}
