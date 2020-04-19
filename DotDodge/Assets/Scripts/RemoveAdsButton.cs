using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Button))]
    public class RemoveAdsButton : MonoBehaviour
    {
        private Button button;

        void Start()
        {
            button = GetComponent<Button>();
            IAPManager.instance.RemoveAdsButton = button;
            button.onClick.AddListener(IAPManager.instance.BuyRemoveAds);
            IAPManager.instance.SetupRemoveAdsButton();
        }
    }
}