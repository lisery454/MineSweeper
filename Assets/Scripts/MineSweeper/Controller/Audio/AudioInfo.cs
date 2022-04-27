using UnityEngine;

namespace MineSweeper {
    [CreateAssetMenu(fileName = "AudioInfo", menuName = "Custom/AudioInfo", order = 2)]
    public class AudioInfo : ScriptableObject {
        public string Name;
        public AudioClip AudioClip;
        public float Volume;
    }
}