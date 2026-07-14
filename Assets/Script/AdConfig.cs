public class AdConfig
{
    public static string AppKey => GetAppKey();
    public static string BannerAdUnitId => GetBannerAdUnitId();
    public static string InterstitalAdUnitId => GetInterstitialAdUnitId();
    public static string RewardedVideoAdUnitId => GetRewardedVideoAdUnitId();
    static string GetAppKey()
    {
#if UNITY_ANDROID
        return "26960e7fd";
#elif UNITY_IPHONE
            return "8545d445";
#else
            return "unexpected_platform";
#endif
    }
    static string GetBannerAdUnitId()
    {
#if UNITY_ANDROID
        return "awc5p8ny2xshr5ta";
#elif UNITY_IPHONE
            return "iep3rxsyp9na3rw8";
#else
            return "unexpected_platform";
#endif
    }
    static string GetInterstitialAdUnitId()
    {
#if UNITY_ANDROID
        return "6tsx74a645ybvcgv";
#elif UNITY_IPHONE
            return "wmgt0712uuux8ju4";
#else
            return "unexpected_platform";
#endif
    }
    static string GetRewardedVideoAdUnitId()
    {
#if UNITY_ANDROID
        return "fggsm4d1l7mbcfjk";
#elif UNITY_IPHONE
            return "qwouvdrkuwivay5q";
#else
            return "unexpected_platform";
#endif
    }
}
