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
            if (IAPManager.instance == null)
            {
                Debug.LogWarning("IAP Manager is null");
                return;
            }
            button = GetComponent<Button>();
            button.interactable = false;
            IAPManager.instance.RemoveAdsButton = button;
            button.onClick.AddListener(IAPManager.instance.BuyRemoveAds);
            IAPManager.instance.SetupRemoveAdsButton();
        }
    }
}