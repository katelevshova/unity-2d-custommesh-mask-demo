using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorSound : MonoBehaviour
{
    public AudioSource audioSource;           //Reference to the narrator audio source component	
    private int _currentId = 0;
    
    void Awake()
    {
        if (audioSource == null)
        {
            throw new Exception("Initialize NarratorSound properties in the Editor, " +
                                "drag from Hierarchy window");
        }
    }

    public void PlayNextSound()
    {
        audioSource.clip = GetNextAudioClip();
        audioSource.Play();
    }
    
    private AudioClip GetNextAudioClip()
    {
        _currentId++;
        string audioClipPath = "Narrator/" + _currentId;
        Debug.Log("audioClipPath= " + audioClipPath);

        AudioClip audioClip = Resources.Load<AudioClip>(audioClipPath);

        if (audioClip == null)
        {
            _currentId = 0;
            audioClip = GetNextAudioClip();
        }
        
        return audioClip;
    }
}
