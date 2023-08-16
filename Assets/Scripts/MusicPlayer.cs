using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicPlayer : MonoBehaviour {

    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetMasterVolume();
    }
	
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        if (SceneManager.GetActiveScene().buildIndex == 2 )
        {
            audioSource.volume = 0;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 )
        {
            audioSource.volume = 0;
        }
    }
}