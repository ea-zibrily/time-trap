using System;
using TimeTrap.DesignPattern.Singleton;
using TimeTrap.Enum;
using Tsukuyomi.Utilities;
using UnityEngine;

namespace TimeTrap.Managers
{
    [AddComponentMenu("TimeTrap/Managers/AudioManager")]
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;
        public static AudioManager Instance;

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(gameObject);
            
            foreach (var s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }

        #endregion

        #region Tsukuyomi Callbacks

        public void PlayAudio(SoundEnum soundName)
        {
            Sound sound = Array.Find(sounds, sound => sound.name == soundName.ToString());
            if (sound == null)
            {
                Debug.LogWarning($"Sound: {soundName} not found!");
                return;
            }
        
            sound.source.Play();
            Debug.Log($"Sound: {soundName} playing!");
        }
        
        public void StopAudio(SoundEnum soundName)
        {
            Sound sound = Array.Find(sounds, sound => sound.name == soundName.ToString());
        
            sound.source.Stop();
            Debug.Log($"Sound: {soundName} stops!");
        }
        
        public void PauseAudio(SoundEnum soundName)
        {
            Sound sound = Array.Find(sounds, sound => sound.name == soundName.ToString());
        
            sound.source.Pause();
        }
        
        public void SetVolume(SoundEnum soundName, float value)
        {
            Sound sound = Array.Find(sounds, sound => sound.name == soundName.ToString());
        
            sound.source.volume = value;
        }
        
        public float GetVolume(SoundEnum soundName)
        {
            Sound sound = Array.Find(sounds, sound => sound.name == soundName.ToString());
        
            return sound.volume;
        }

        #endregion
    }
}