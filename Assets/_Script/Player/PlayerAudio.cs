using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip hit_sound;
    [SerializeField] private AudioClip jump_sound;
    [SerializeField] private AudioClip[] gameOver;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(gameOver[2]);
    }

    public void PlayJumSound()
    {
        audioSource.PlayOneShot(jump_sound);
    }

    public void PlayHitSound()
    {
        audioSource.PlayOneShot(hit_sound);
    }

    public void PlayGameOver()
    {
        int i = Random.Range(0, 2);
        audioSource.PlayOneShot(gameOver[i]);
    }
}
