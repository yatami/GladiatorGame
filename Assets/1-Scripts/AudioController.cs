using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource[] audioSources;

    public static AudioController Instance;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.startGameEvent.AddListener(gameStarted);
    }

    private void gameStarted()
    {
        StartCoroutine(switchTracks());
    }

    IEnumerator switchTracks()
    {
        float time = 0;
        audioSources[2].Play();
        audioSources[2].volume = 0;

        while (time < 1f)
        {
            time += Time.deltaTime;
            audioSources[2].volume = time;
            audioSources[1].volume = 1- time;
        }
        audioSources[1].Stop();
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayArrowHitSound()
    {
        audioSources[0].Play();
    }

    public void PlayFleshSound()
    {
        audioSources[3].Play();
    }
}
