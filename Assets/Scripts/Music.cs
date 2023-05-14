using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        GenerateMusic();
    }

    void GenerateMusic()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.volume = 0.5f;
        audioSource.Play();
        audioSource.loop = true;
    }
}
