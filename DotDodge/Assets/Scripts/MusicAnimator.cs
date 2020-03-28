using UnityEngine;

namespace Assets.Scripts
{
    public class MusicAnimator : MonoBehaviour
    {
        public AudioSource Music;
        public float PitchDrop;
        public float DropSeconds;

        private float initialVolume;

        void Start()
        {
            initialVolume = Music.volume;
        }

        public void DropPitch()
        {
            LeanTween.value(gameObject, f => Music.pitch = f, Music.pitch, PitchDrop, DropSeconds);
            LeanTween.value(gameObject, f => Music.volume = f, Music.volume, 0, DropSeconds).setOnComplete(() => Music.Stop());
        }

        public void ResetSound()
        {
            Music.volume = initialVolume;
            Music.pitch = 1;
            Music.Play();
        }
    }
}