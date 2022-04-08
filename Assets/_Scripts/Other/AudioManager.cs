using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip hoverSound;
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioSource audioSource;
    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void PlayHoverSound()
    {
        audioSource.PlayOneShot(hoverSound);
    }
}
