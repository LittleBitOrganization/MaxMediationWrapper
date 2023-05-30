using System;

namespace LittleBitGames.Ads.AdUnits
{
    public class MaxSdkRewardedEvents : IAdUnitEvents
    {
        public event Action<string, IAdInfo> OnAdRevenuePaid;
        public event Action<string, IAdInfo> OnAdLoaded;
        public event Action<string, IAdErrorInfo> OnAdLoadFailed;
        public event Action<string, IAdInfo> OnAdFinished;
        public event Action<string, IAdInfo> OnAdClicked;
        public event Action<string, IAdInfo> OnAdHidden;
        public event Action<string, IAdErrorInfo, IAdInfo> OnAdDisplayFailed;

        public MaxSdkRewardedEvents()
        {
            MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += (s, info) => OnAdRevenuePaid?.Invoke(s, new AdInfo(info));
            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += (s, info) => OnAdLoaded?.Invoke(s, new AdInfo(info));
            MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += (s, info) => OnAdLoadFailed?.Invoke(s, new AdErrorInfo(info));
            MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += (s, reward, info) => OnAdFinished?.Invoke(s, null);
            MaxSdkCallbacks.Rewarded.OnAdClickedEvent += (s, info) => OnAdClicked?.Invoke(s, new AdInfo(info));
            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += (s, info) => OnAdHidden?.Invoke(s, new AdInfo(info));
            MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += (s, error, info) => OnAdDisplayFailed?.Invoke(s, new AdErrorInfo(error), new AdInfo(info));
        }
    }
}