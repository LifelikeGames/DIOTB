using System;
using System.Collections;
using UnityEngine;

namespace VitaSoftware.Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private AudioSource audioSource;

        private void Start()
        {
            audioManager.Initialise(audioSource);
            StartCoroutine(PlayClips());
        }

        private IEnumerator PlayClips()
        {
            yield return new WaitForSeconds(audioManager.PlayNextClip());
        }
    }
}