using LittleBit.Modules.CoreModule;
using LittleBitGames.Ads.Configs;
using LittleBitGames.Ads.MediationNetworks.MaxSdk;
using LittleBitGames.Environment.Ads;
using UnityEngine;

namespace LittleBitGames.Ads
{
    public class MaxSdkAds
    {
        private readonly MaxSdkAdsServiceBuilder _builder;
        private readonly AdsConfig _adsConfig;
        private readonly ICreator _creator;
        private IAdsService _adsService;

        public MaxSdkAds(ICreator creator, ICoroutineRunner coroutineRunner)
        {
            _creator = creator;
            
            _adsConfig = Resources.Load<AdsConfig>(AdsConfig.PathInResources);
            
            _builder = creator.Instantiate<MaxSdkAdsServiceBuilder>(_adsConfig);
        }

        public IAdsService CreateAdsService()
        {
            var adsService = _builder.QuickBuild();

            _adsService = adsService;
            _adsService.Run();

            return _adsService;
        }

        public IMediationNetworkAnalytics CreateAnalytics() => _creator.Instantiate<MaxSdkAnalytics>(_adsService);
    }
}