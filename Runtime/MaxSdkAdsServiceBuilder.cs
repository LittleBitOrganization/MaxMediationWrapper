using System;
using LittleBit.Modules.CoreModule;
using LittleBitGames.Ads.AdUnits;
using LittleBitGames.Ads.Configs;
using LittleBitGames.Ads.MediationNetworks.MaxSdk;
using LittleBitGames.Environment.Ads;
using UnityEngine.Scripting;

namespace LittleBitGames.Ads
{
    public class MaxSdkAdsServiceBuilder : IAdsServiceBuilder
    {
        private readonly MaxSdkAdUnitsFactory _adUnitsFactory;
        private readonly MaxSdkInitializer _initializer;

        private IAdUnit _inter, _rewarded,_banner;
        private AdsConfig _adsConfig;

        public IMediationNetworkInitializer Initializer => _initializer;

        [Preserve]
        public MaxSdkAdsServiceBuilder(AdsConfig adsConfig, ICoroutineRunner coroutineRunner)
        {
            _adsConfig = adsConfig;
            _adUnitsFactory = new MaxSdkAdUnitsFactory(coroutineRunner, adsConfig);
            _initializer = new MaxSdkInitializer(adsConfig);

            if (!ValidateMaxSdkKey())
                throw new Exception($"Max sdk key is invalid! Key: {_adsConfig.MaxSettings.MaxSdkKey}");
        }

        private bool ValidateMaxSdkKey() => !string.IsNullOrEmpty(_adsConfig.MaxSettings.MaxSdkKey);

        public IAdsService QuickBuild()
        {
            if (!string.IsNullOrEmpty(_adsConfig.MaxSettings.PlatformSettings.MaxInterAdUnitKey) && _adsConfig.IsInter) 
                BuildInterAdUnit();
            if (!string.IsNullOrEmpty(_adsConfig.MaxSettings.PlatformSettings.MaxRewardedAdUnitKey) && _adsConfig.IsRewarded) 
                BuildRewardedAdUnit();
            if (!string.IsNullOrEmpty(_adsConfig.MaxSettings.PlatformSettings.MaxBannerAdUnitKey) && _adsConfig.IsBanner) 
                BuildBannerAdUnit();

            return GetResult();
        }

        public void BuildInterAdUnit() =>
            _inter = _adUnitsFactory.CreateInterAdUnit();

        public void BuildRewardedAdUnit() =>
            _rewarded = _adUnitsFactory.CreateRewardedAdUnit();
        public void BuildBannerAdUnit() =>
            _banner = _adUnitsFactory.CreateBannerAdUnit();

        public IAdsService GetResult() => new AdsService(_initializer, _inter, _rewarded, _banner);
    }
}