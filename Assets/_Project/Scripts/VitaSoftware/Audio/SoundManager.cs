using System;
using System.Collections.Generic;
using UnityEngine;

namespace VitaSoftware.Audio
{
    [CreateAssetMenu(fileName = "New Sound Manager", menuName = "VitaSoftware/SoundManager", order = 0)]
    public class SoundManager : AudioManager
    {
        [SerializeField] private bool isSoundEnabled = true;
        [SerializeField] private NamedSounds[] namedSounds;
        
        private AudioClip nextRequestedSFX;
        private AudioSource audioSource;
        private Dictionary<string, AudioClip> soundDictionary;

        public bool IsSoundEnabled => isSoundEnabled;
        
        public override void Initialise(AudioSource source)
        {
            audioSource = source;

            soundDictionary = new Dictionary<string, AudioClip>();

            foreach (var namedSound in namedSounds)
            {
                soundDictionary.Add(namedSound.name, namedSound.clip);
            }
        }

        public void RequestSFX(AudioClip clip)
        {
            nextRequestedSFX = clip;
        }

        public void RequestSFX(string sfxName)
        {
            nextRequestedSFX = soundDictionary[sfxName];
        }
        

        public override float PlayNextClip()
        {
            if (!isSoundEnabled || nextRequestedSFX == null) return 0.1f;
            audioSource.clip = nextRequestedSFX;
            audioSource.Play();
            var length = nextRequestedSFX.length;
            nextRequestedSFX = null;
            return length;
        }

        public void ToggleSounds(bool value)
        {
            isSoundEnabled = value;
        }
    }

    [Serializable]
    public struct NamedSounds
    {
        public string name;
        public AudioClip clip;
    }
}