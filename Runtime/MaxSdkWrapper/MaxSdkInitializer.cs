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

        public bool IsInitialized { get; private set; } = false;

        public void Initialize()
        {
            if (_config.UmpSettings.IsEnable == false)
            {
                Init();
            }
            else
            {
                UMPHandler umpHandler = new UMPHandler(_config.UmpSettings, IsDebugMode);
                umpHandler.OnConsent += () =>
                {
                    global::MaxSdk.SetHasUserConsent(true);
                    Init();
                };
                umpHandler.Init();
            }
            
        }

        private void Init()
        {
            global::MaxSdk.SetSdkKey(_config.MaxSettings.MaxSdkKey);
            global::MaxSdk.SetUserId("USER_ID");
            global::MaxSdk.InitializeSdk();

            MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfig =>
            {
                IsInitialized = true;
                OnMediationInitialized?.Invoke();
                
                if (IsDebugMode) global::MaxSdk.ShowMediationDebugger();
            };
        }
    }
}