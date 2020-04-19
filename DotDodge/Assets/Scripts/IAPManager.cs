using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class IAPManager : MonoBehaviour, IStoreListener
{
    [HideInInspector]
    public Button RemoveAdsButton;
    public static IAPManager instance;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    //Step 1 create your products
    private string removeAds = "plagueforce_remove_ads";

    //************************** Adjust these methods **************************************
    public void InitializePurchasing()
    {
        if (IsInitialized()) { return; }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Step 2 choose if your product is a consumable or non consumable
        builder.AddProduct(removeAds, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);

     
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    //Step 3 Create methods
    public void BuyRemoveAds()
    {
        BuyProductID(removeAds);
    }



    //Step 4 modify purchasing
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, removeAds, StringComparison.Ordinal))
        {
            HasRemovedAds = true;
            if (RemoveAdsButton)
            {
                RemoveAdsButton.interactable = false;
            }
            Popup.Instance.Alert("Congrats!","Ads every x number of games has been removed.  Thank you for your support!  You can still watch an ad to continue a game if you please.");
        }
        else
        {
            Popup.Instance.Alert("Oh no!","Purchase Failed");
            Debug.Log("Purchase Failed");
        }
        return PurchaseProcessingResult.Complete;
    }










    //**************************** Dont worry about these methods ***********************************
    private void Awake()
    {
        TestSingleton();
    }

    void Start()
    {
        if (m_StoreController == null) { InitializePurchasing(); }
    }

    private void TestSingleton()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log($"Purchasing product asychronously: '{product.definition.id}'");
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                var msg =
                    "BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase";
                Debug.Log(msg);
                Popup.Instance.Alert("Error", msg);
            }
        }
        else
        {
            var msg = "BuyProductID FAIL. Not initialized.";
            Debug.Log(msg);
            Popup.Instance.Alert("Error", msg);
        }
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) => {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public bool HasRemovedAds;
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;


        var ads = m_StoreController.products.WithID(removeAds);
        //if (ads != null && ads.hasReceipt)
        //{
        //    SettingsManager.instance.settingSystem.HasRemovedAds = true;
        //    SettingsManager.instance.settingSystem.Save();
        //}
        var hasBought = ads != null && ads.hasReceipt;

        if (RemoveAdsButton)
        {
            RemoveAdsButton.interactable = !hasBought;
        }
        HasRemovedAds = hasBought;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}