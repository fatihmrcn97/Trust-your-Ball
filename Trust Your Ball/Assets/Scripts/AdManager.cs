using UnityEngine;
using GoogleMobileAds.Api;
using System;
public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    private BannerView bannerAd;
    private RewardedAd rewardedAd;

    private bool tryOne;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        tryOne = true;
    }
    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
        
    }


    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerAd = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        this.bannerAd.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerAd.LoadAd(request);

    }

    private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        if (tryOne)
        {
        this.RequestBanner();
            tryOne = false;
        }
    }


    public void RequestRewardedAd()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;

        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
        Debug.Log("Rewarded ad kapatýldý");
    }

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        LevelController.instance.WatchAdSettleMent();
    }

    private void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        RequestRewardedAd();
    }

    private void HandleRewardedAdLoaded(object sender, EventArgs e)
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }
}
