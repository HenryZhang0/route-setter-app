using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    AudioSource m_MyAudioSource;

    void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
        m_MyAudioSource.Play();
    }

    public void OnVolumeChange(float value)
    {
        m_MyAudioSource.volume = value;
    }
}
