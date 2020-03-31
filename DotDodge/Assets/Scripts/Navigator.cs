using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Button))]
    public class Navigator : MonoBehaviour
    {
        private Button button;
        public GameObject From;
        public GameObject To;

        void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(Clicked);
        }

        private void Clicked()
        {
            Navigate();
        }

        public void GoBack()
        {
            From.SetActive(true);
            NavigationStack.Stack.Remove(this);
            To.SetActive(false);
        }

        public void Navigate()
        {
            NavigationStack.Stack.Add(this);
            To.SetActive(true);
            From.SetActive(false);
        }
    }
}