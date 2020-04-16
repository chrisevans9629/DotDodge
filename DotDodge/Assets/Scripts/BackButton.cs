using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Button))]
    public class BackButton : MonoBehaviour
    {
        private Button button;

        void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(Clicked);
        }

        private void Clicked()
        {
            NavigationStack.Stack.Peek().GoBack();
        }
    }
}