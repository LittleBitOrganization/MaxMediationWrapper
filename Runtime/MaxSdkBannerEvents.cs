using System;

namespace LittleBitGames.Ads.AdUnits
{
    public class MaxSdkBannerEvents : IAdUnitEvents
    {
        public event Action<string, IAdInfo> OnAdRevenuePaid;
        public event Action<string, IAdInfo> OnAdLoaded;
        public event Action<string, IAdErrorInfo> OnAdLoadFailed;
        public event Action<string, IAdInfo> OnAdFinished;
        public event Action<string, IAdInfo> OnAdClicked;
        public event Action<string, IAdInfo> OnAdHidden;
        public event Action<string, IAdErrorInfo, IAdInfo> OnAdDisplayFailed;

        public MaxSdkBannerEvents()
        {
            MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += (s, info) => OnAdRevenuePaid?.Invoke(s, new AdInfo(info));
            MaxSdkCallbacks.Banner.OnAdLoadedEvent += (s, info) => OnAdLoaded?.Invoke(s, new AdInfo(info));
            MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += (s, info) => OnAdLoadFailed?.Invoke(s, new AdErrorInfo(info));
            MaxSdkCallbacks.Banner.OnAdClickedEvent += (s, info) => OnAdClicked?.Invoke(s, new AdInfo(info));
            MaxSdkCallbacks.Banner.OnAdCollapsedEvent += (s, info) => OnAdHidden?.Invoke(s, new AdInfo(info));
        }
    }
}