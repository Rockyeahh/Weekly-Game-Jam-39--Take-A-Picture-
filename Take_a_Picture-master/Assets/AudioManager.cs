using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip[] audioClips;
    AudioSource audioSource;
    public static AudioManager Instance;

    void Start()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }


    public void Playclip(int index)
    {
        audioSource.PlayOneShot(audioClips[index]);
    }
}
