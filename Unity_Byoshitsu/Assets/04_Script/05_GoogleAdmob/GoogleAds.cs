using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAds : MonoBehaviour
{

    private BannerView HeaderBannerView;
    private BannerView FooterBannerView;
    private BannerView squareBannerView;
    private RewardedAd rewardedAd;

    public HintManager HintClass;

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        //画面上バナー
        this.RequestHeaderBanner();
        //画面下バナー
        this.RequestFooterBanner();
        //リワード広告(動画)
        this.RequestReward();
    }

    //**********************************
    //**バナー広告(ヘッダー)
    //**********************************

    //画面上のバナーの表示
    private void RequestHeaderBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7464443980940177/5119592365";
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-7464443980940177/6145996902";
#else
        //テスト
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#endif

        AdSize adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        // Create a 320x50 banner at the top of the screen.
        HeaderBannerView = new BannerView(adUnitId, adSize, AdPosition.Top);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        HeaderBannerView.LoadAd(request);
    }

    //**********************************
    //**バナー広告(フッター)
    //**********************************

    //画面下のバナーの表示
    private void RequestFooterBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7464443980940177/9988775662";
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-7464443980940177/1276813608";
#else
        //テスト
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#endif

        //バナーサイズ
        AdSize adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        // Create a 320x50 banner at the top of the screen.
        FooterBannerView = new BannerView(adUnitId, adSize, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        FooterBannerView.LoadAd(request);
    }

    //**********************************
    //**バナー広告(Menu画面内の長方形)
    //**********************************
    //Menu画面内のバナーの表示
    public void RequestSquareBanner()
    {
#if UNITY_ANDROID
         string adUnitId = "";
#elif UNITY_IOS
        string adUnitId = "";
#else
        //テスト
         string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#endif

        // Create a banner at the top of the screen.
        this.squareBannerView = new BannerView(adUnitId, AdSize.MediumRectangle, AdPosition.Center);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        this.squareBannerView.LoadAd(request);
    }

    //Menu画面内のバナーの非表示
    public void unRequestSquareBanner()
    {
        squareBannerView.Destroy();
    }

    //**********************************
    //**リワード広告
    //**********************************

    /// <summary>
    /// 動画のロード
    /// </summary>
    private void RequestReward()
    {
        string adUnitId = "";
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-7464443980940177/7345482219";  //本番
#elif UNITY_IOS
        adUnitId = "ca-app-pub-7464443980940177/3041058086";  //本番
#else
        //テスト
        adUnitId = "ca-app-pub-3940256099942544/1712485313";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);
        //動画の視聴が完了したら「HandleUserEarnedReward」を呼ぶ
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // 広告が閉じたとき「HandleRewardedAdClosed」を呼ぶ
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        //動画ロードに失敗した場合
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;

        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }


    /// <summary>
    /// 動画の視聴開始
    /// </summary>
    public void ShowReawrd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }


    /// <summary>
    /// 動画視聴完了後の処理（途中で閉じられた場合は呼ばれない）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        //ヒントを表示
        HintClass.AfterWatch();
    }

    /// <summary>
    // 広告が閉じたときに呼び出されます 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public void HandleRewardedAdClosed(object sender, System.EventArgs args)
    {
        //リワード広告の再ロード
        this.RequestReward();
    }

    /// <summary>
    /// 動画ロードに失敗した場合　テスト動画を読み込む
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
        this.rewardedAd = new RewardedAd(adUnitId);

        //動画の視聴が完了したら「HandleUserEarnedReward」を呼ぶ
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // 広告が閉じたとき「HandleRewardedAdClosed」を呼ぶ
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }
}
