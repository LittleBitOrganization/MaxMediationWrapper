using LittleBit.Modules.CoreModule;
using LittleBitGames.Ads.AdUnits;

namespace LittleBitGames.Ads.MediationNetworks.MaxSdk
{
    public sealed class MaxSdkBannerAd : AdUnitLogic
    {
        private readonly IAdUnitKey _key;
        private bool _isAdReady;


        public MaxSdkBannerAd(IAdUnitKey key, ICoroutineRunner coroutineRunner) : base(key,
            new MaxSdkBannerEvents(), coroutineRunner)

        {
            _key = key;
            if (global::MaxSdk.IsInitialized())
                Init(null);
            else
                global::MaxSdkCallbacks.OnSdkInitializedEvent += Init;
        }
        private void Init(MaxSdkBase.SdkConfiguration sdkConfiguration)
        {
            global::MaxSdkCallbacks.OnSdkInitializedEvent -= Init;
            global::MaxSdk.CreateBanner(_key.StringValue, MaxSdkBase.BannerPosition.BottomCenter);
            global::MaxSdk.SetBannerExtraParameter(_key.StringValue, "adaptive_banner", "true");
            global::MaxSdk.StartBannerAutoRefresh(_key.StringValue);
        }
        protected override bool IsAdReady() => true;
        protected override void ShowAd() => global::MaxSdk.ShowBanner(_key.StringValue);
        public override void Load()
        {
            
        }
    }
}