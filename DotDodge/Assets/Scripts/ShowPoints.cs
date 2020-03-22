using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ShowPoints : MonoBehaviour
    {
        public Text TextPrefab;

        public float InDuration;
        public float OutDuration;
        public void OnPointScored(float value)
        {
            var text = Instantiate(TextPrefab, transform.position, Quaternion.identity, transform);

            text.text = $"+{value}";
            text.gameObject.transform.localScale = Vector3.zero;

            LeanTween.scale(text.gameObject, Vector3.one, InDuration).setOnComplete(() =>
            {
                LeanTween.scale(text.gameObject, Vector3.zero, OutDuration).setDestroyOnComplete(true);
            });
        }

    }
}