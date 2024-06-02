using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    [SerializeField] private AudioSource soundObject;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void PlaySound(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity, this.transform);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        audioSource.transform.name = audioClip.name;

        float clipLenght = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLenght);
    }
}
