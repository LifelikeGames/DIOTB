using System.Collections;
using UnityEngine;

namespace VitaSoftware.Audio
{
    [CreateAssetMenu(fileName = "New Music Manager", menuName = "VitaSoftware/MusicManager", order = 0)]
    public class MusicManager : AudioManager
    {
        [SerializeField] private bool isMusicActive = true;
        [SerializeField] private AudioClip[] musicClips;

        public bool IsMusicActive => isMusicActive;
        
        private AudioSource audioSource;
        private int nextIndex;

        public override void Initialise(AudioSource source)
        {
            audioSource = source;
        }

        public override float PlayNextClip()
        {
            if (!isMusicActive) return float.MaxValue;
            
            audioSource.clip = musicClips[GetNextIndex()];
            audioSource.Play();

            return audioSource.clip.length;
        }

        public void DisableMusic()
        {
            audioSource.Pause();
            isMusicActive = false;
        }

        public void EnableMusic()
        {
            isMusicActive = true;
            if(audioSource.clip == null)
                PlayNextClip(); 
            audioSource.UnPause();
        }

        private int GetNextIndex()
        {
            nextIndex++;
            if (nextIndex >= musicClips.Length)
                nextIndex = 0;

            return nextIndex;
        }
    }
}