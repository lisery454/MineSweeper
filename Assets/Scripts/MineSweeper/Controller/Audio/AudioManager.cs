using System.Collections.Generic;
using UnityEngine;

namespace MineSweeper {
    public class AudioManager : SingletonInOneScene<AudioManager> {
        [SerializeField] private List<AudioInfo> AudioInfos;
        private AudioSource AudioSource;

        protected override void Awake() {
            base.Awake();
            AudioSource = GetComponent<AudioSource>();
        }
        
        public void PlayAudio(string audioName) {
            var audioInfo = AudioInfos.Find(ai => ai.Name == audioName);
            AudioSource.PlayOneShot(audioInfo.AudioClip, audioInfo.Volume);
        }
    }
}