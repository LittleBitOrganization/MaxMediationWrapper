using System;
using LittleBitGames.Ads.Configs;
using LittleBitGames.Environment.Ads;

namespace LittleBitGames.Ads.MediationNetworks.MaxSdk
{
    public class MaxSdkInitializer : IMediationNetworkInitializer
    {
        public event Action OnMediationInitialized;

        private readonly AdsConfig _config;
        public MaxSdkInitializer(AdsConfig config) => _config = config;
        private bool IsDebugMode => _config.Mode is ExecutionMode.Debug;

        public void Initialize()
        {
            global::MaxSdk.SetSdkKey(_config.MaxSettings.MaxSdkKey);
            global::MaxSdk.SetUserId("USER_ID");
            global::MaxSdk.InitializeSdk();

            MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfig =>
            {
                OnMediationInitialized?.Invoke();
                
                if (IsDebugMode) global::MaxSdk.ShowMediationDebugger();
            };
        }
    }
}