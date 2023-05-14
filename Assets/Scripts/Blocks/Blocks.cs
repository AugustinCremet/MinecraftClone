using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    new ParticleSystem particleSystem;
    ParticleSystem.MainModule setting;
    AudioSource audioSource;

    public float timeToDestroy;
    public Color particlesColor;
    public AudioClip destroySound;

    void Awake() 
    {
        particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
        particleSystem.transform.position = transform.position;
        setting = particleSystem.main;
        audioSource = GetComponent<AudioSource>();
    }

    public float GetTimeToDestroy()
    {
        return timeToDestroy;
    }

    public void StartParticles(Transform endPos)
    {
        setting.startColor = particlesColor;
        particleSystem.transform.eulerAngles = new Vector3(-endPos.eulerAngles.x, endPos.eulerAngles.y + 180, -endPos.eulerAngles.z);
        if(!particleSystem.isPlaying)
            particleSystem.Play();
    }

    public void StopParticles()
    {
        setting.loop = false;
        particleSystem.Stop();
    }

    public void PlayMiningSound()
    {
        if(!audioSource.isPlaying)
            audioSource.Play();
    }

    public void StopMiningSound()
    {
        audioSource.loop = false;
        audioSource.Stop();
    }

    public void DestroyBlock()
    {
        audioSource.PlayOneShot(destroySound);
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject, destroySound.length);
    }
}
