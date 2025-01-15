using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip disparoPistola, disparoRifle, disparoEscopeta, recargar;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void DisparoPistola()
    {
        audioSource.PlayOneShot(disparoPistola);
    }
    public void DisparoRifle()
    {
        audioSource.PlayOneShot(disparoRifle);
    }
    public void DisparoEscopeta()
    {
        audioSource.PlayOneShot(disparoEscopeta);
    }
    public void Recargar()
    {
        audioSource.PlayOneShot(recargar);
    }
}
