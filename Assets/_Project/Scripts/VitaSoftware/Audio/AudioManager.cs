using UnityEngine;

namespace VitaSoftware.Audio
{
    public abstract class AudioManager : ScriptableObject
    {
        public abstract void Initialise(AudioSource audioSource);

        public abstract float PlayNextClip();
        
    }
}