using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMusicBox : MonoBehaviour
{
    private static ConstantMusicBox instance;

    Object[] songs;
    private AudioSource thisAudioSource;

    private int lastSongIndex = -1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            thisAudioSource = this.GetComponent<AudioSource>();
            songs = Resources.LoadAll("BackgroundMusic", typeof(AudioClip));
            SetRandomSong();
        } else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        thisAudioSource.Play();
    }

    private void Update()
    {
        if (!thisAudioSource.isPlaying)
        {
            SetRandomSong();
        }
    }

    private void SetRandomSong()
    {
        int newSongIndex = -1;
        while (newSongIndex < 0 || newSongIndex == lastSongIndex) {
            newSongIndex = Random.Range(0, songs.Length);
        }
        thisAudioSource.clip = songs[newSongIndex] as AudioClip;
        thisAudioSource.Play();
    }
}
