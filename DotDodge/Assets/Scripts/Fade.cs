using UnityEngine;

namespace Assets.Scripts
{
    public class Fade : MonoBehaviour
    {
        public float Speed;
        public void FadeOut()
        {
            LeanTween.color(gameObject, Color.clear, Speed);
        }
    }
}