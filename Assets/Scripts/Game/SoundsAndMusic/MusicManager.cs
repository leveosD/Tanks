using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicManager : MonoBehaviour
{
    private AudioClip[] tracks;
    [SerializeField] private AudioClip[] winLose;
    private AudioSource audioSource;
    private AudioSource effectsSource;

    public int count;
    private float delay = 0;
    
    void Awake()
    {
        audioSource = GetComponents<AudioSource>()[0];
        effectsSource = GetComponents<AudioSource>()[1];
        int r = Random.Range(0, 3);
        tracks = Resources.LoadAll<AudioClip>("BattleMusic/" + r);
        count = tracks.Length;
    }

    private void OnEnable()
    {
        GameOverUIManager.GameOverSound += PlayEndGameMusic;
    }

    private void OnDestroy()
    {
        GameOverUIManager.GameOverSound -= PlayEndGameMusic;
    }
    
    private void PlayEndGameMusic(bool win)
    {
        
        audioSource.Stop();
        delay = 5.5f;
        if(win)
            effectsSource.PlayOneShot(winLose[0]);
        else
        {
            effectsSource.PlayOneShot(winLose[1]);
        }
    }

    void Update()
    {
        if (!audioSource.clip && delay <= 0)
        {
            audioSource.clip = tracks[Random.Range(0, count)];
            audioSource.Play();
        }
        else if (!audioSource.isPlaying)
            audioSource.clip = null;

        if (delay > 0)
            delay -= Time.deltaTime;
    }
}
