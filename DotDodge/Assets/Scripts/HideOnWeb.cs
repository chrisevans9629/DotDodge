using UnityEngine;

namespace Assets.Scripts
{
    public class HideOnWeb : MonoBehaviour
    {
        void Start()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                gameObject.SetActive(false);
            }
        }
    }
}