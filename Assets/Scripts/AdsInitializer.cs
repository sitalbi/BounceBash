using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    string _gameId;
    [SerializeField] bool _testMode = true;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject homeButton;

    private void Awake()
    {
        if (Advertisement.isInitialized)
        {
            Debug.Log("Advertisement is Initialized");
            LoadBannerAd();
        }
        else
        {
            InitializeAds();
        }
    }
    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSGameId : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadBannerAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void LoadInterstitialAd()
    {
        string placementId = (Application.platform == RuntimePlatform.IPhonePlayer) ? "Interstitial_iOS" : "Interstitial_Android";
        Advertisement.Load(placementId, this);
    }

    public void LoadRewardedAd()
    {
        string placementId = (Application.platform == RuntimePlatform.IPhonePlayer) ? "Rewarded_iOS" : "Rewarded_Android";
        //homeButton.GetComponent<Button>().interactable = false;
        Advertisement.Load(placementId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("OnUnityAdsAdLoaded");
        /*if (placementId == "Rewarded_iOS" || placementId == "Rewarded_Android")
        {
            homeButton.GetComponent<Button>().interactable = false;
        }*/
        Advertisement.Show(placementId,this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("OnUnityAdsShowFailure");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("OnUnityAdsShowStart");
        Advertisement.Banner.Hide();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("OnUnityAdsShowClick");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("OnUnityAdsShowComplete "+showCompletionState);
        string rewarded = (Application.platform == RuntimePlatform.IPhonePlayer) ? "Rewarded_iOS" : "Rewarded_Android";
        if (placementId.Equals(rewarded) && UnityAdsShowCompletionState.COMPLETED.Equals(showCompletionState))
        {
            Debug.Log("rewarded Player");
            gameManager.Continue();
        }
        string interstitiate = (Application.platform == RuntimePlatform.IPhonePlayer) ? "Interstitial_iOS" : "Interstitial_Android";
        if (placementId.Equals(interstitiate) && UnityAdsShowCompletionState.COMPLETED.Equals(showCompletionState))
        {
            Debug.Log("interstitiate Player");
            SceneManager.LoadScene("Scenes/MainMenu");
        }
    }



    public void LoadBannerAd()
    {
        string placementId = (Application.platform == RuntimePlatform.IPhonePlayer) ? "Banner_iOS" : "Banner_Android";
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load(placementId,
            new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            }
            );
    }

    void OnBannerLoaded()
    {
        Advertisement.Banner.Show("Banner_Android");
    }

    void OnBannerError(string message)
    {
        Debug.Log("Could not show banner");
    }

}
