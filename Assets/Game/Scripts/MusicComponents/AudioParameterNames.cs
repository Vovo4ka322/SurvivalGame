using UnityEngine;

namespace Game.Scripts.MusicComponents
{
    [CreateAssetMenu(fileName = "AudioParameterNames", menuName = "Audio/ParameterNames")]
    public class AudioParameterNames : ScriptableObject
    {
        public string AllSoundVolume = "AllSoundVolume";
        public string MusicVolume = "MusicVolume";
        public string EffectsVolume = "EffectsVolume";
    }
}