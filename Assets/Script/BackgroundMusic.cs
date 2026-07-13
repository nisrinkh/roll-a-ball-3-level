using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;

    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("GameVolume", 1f);
    }
    void Awake()
    {
        // Pastikan hanya satu musik aktif di seluruh scene
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Jangan hancurkan saat ganti scene
    }

    public void StopMusic()
    {
        GetComponent<AudioSource>().Stop();
    }

    public void PlayMusic()
    {
        GetComponent<AudioSource>().Play();
    }
}
