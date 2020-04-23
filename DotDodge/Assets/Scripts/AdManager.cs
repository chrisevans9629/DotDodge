using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    private string gameId = "3532261";

    public Action ContinueAction;
 

    public bool TestMode = true;
    private static string continueGameId = "ContinueGame";
    public static AdManager Instance;
    void Awake()
    {
        if(Instance != null)
            Destroy(this);
        DontDestroyOnLoad(this);
        Instance = this;
    }
    void Start()
    {
        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, TestMode);
    }

    public static void SetupButton(Button ContinueButton)
    {
        if(ContinueButton == null)
            return;
        // Set interactivity to be dependent on the Placement’s status:
        ContinueButton.interactable = Advertisement.IsReady(continueGameId);

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (ContinueButton)
        {
            ContinueButton.onClick.AddListener(ShowRewardedVideo);
        }
    }

    // Implement a function for showing a rewarded video ad:
    static void ShowRewardedVideo()
    {
        Advertisement.Show(continueGameId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == continueGameId)
        {
            //ContinueButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            if (placementId == continueGameId)
            {
                if (ContinueAction == null)
                {
                    throw new InvalidOperationException();
                }
                ContinueAction.Invoke();
                //RestartGame.Continue();
            }
            // Reward the user for watching the ad to completion.
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Popup.Instance.Alert("Uh oh!", "The ad did not finish due to an error. No Worries! Try again later.");
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}