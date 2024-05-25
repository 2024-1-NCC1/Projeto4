using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arvore : MonoBehaviour
{
    public int healing;
    public AudioClip healingAudioClip; // Adiciona uma referência ao clipe de áudio
    private AudioSource audioSource; // Adiciona uma referência ao AudioSource

    void Start()
    {
        // Obtenha a referência ao componente AudioSource
        audioSource = GetComponent<AudioSource>();

        // Certifique-se de que o áudio correto está definido no AudioSource
        if (healingAudioClip != null)
        {
            audioSource.clip = healingAudioClip;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        MayaVida mayaVida = other.GetComponent<MayaVida>();
        if (mayaVida != null)
        {
            mayaVida.ReceiveHealth(healing);

            // Toca o áudio de cura
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
