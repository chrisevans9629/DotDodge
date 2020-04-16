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
            NavigationStack.Stack.Pop();
            To.SetActive(false);
            Debug.Log($"Went from {From} to {To}");
        }

        public void Navigate()
        {
            NavigationStack.Stack.Push(this);
            To.SetActive(true);
            From.SetActive(false);
            Debug.Log($"Went from {From} to {To}");
        }
    }
}