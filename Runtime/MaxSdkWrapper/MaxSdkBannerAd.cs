using LittleBit.Modules.CoreModule;
using LittleBitGames.Ads.AdUnits;

namespace LittleBitGames.Ads.MediationNetworks.MaxSdk
{
    public sealed class MaxSdkBannerAd : AdUnitLogic
    {
        private readonly IAdUnitKey _key;

        public MaxSdkBannerAd(IAdUnitKey key, ICoroutineRunner coroutineRunner) : base(key,
            new MaxSdkBannerEvents(), coroutineRunner)

        {
            _key = key;
            global::MaxSdk.CreateBanner(_key.StringValue, MaxSdkBase.BannerPosition.BottomCenter);
            global::MaxSdk.SetBannerExtraParameter(_key.StringValue, "adaptive_banner", "true");
        }

        protected override bool IsAdReady() => true;

        protected override void ShowAd() => global::MaxSdk.ShowBanner(_key.StringValue);
        public override void Load() => global::MaxSdk.LoadBanner(_key.StringValue);
    }
}