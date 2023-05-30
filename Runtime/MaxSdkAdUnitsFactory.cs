using System;
using LittleBit.Modules.CoreModule;
using LittleBitGames.Ads.AdUnits;
using LittleBitGames.Ads.Configs;
using LittleBitGames.Ads.MediationNetworks.MaxSdk;

namespace LittleBitGames.Ads
{
    internal class MaxSdkAdUnitsFactory
    {
        private readonly ICoroutineRunner _coroutineRunner;

        private readonly AdsConfig _adsConfig;

        public MaxSdkAdUnitsFactory(ICoroutineRunner coroutineRunner, AdsConfig adsConfig)
        {
            _adsConfig = adsConfig;

            _coroutineRunner = coroutineRunner;
        }

        public IAdUnit CreateInterAdUnit() =>
            new MaxSdkInterAd(GetKey(_adsConfig.MaxSettings.PlatformSettings.MaxInterAdUnitKey), _coroutineRunner);

        public IAdUnit CreateRewardedAdUnit() =>
            new MaxSdkRewardedAd(GetKey(_adsConfig.MaxSettings.PlatformSettings.MaxRewardedAdUnitKey), _coroutineRunner);

        private IAdUnitKey GetKey(string s)
        {
            var key = new AdUnitKey(s);

            if (!key.Validate()) throw new Exception($"Max ad unit key is invalid! Key: {s}");

            return key;
        }
    }
}