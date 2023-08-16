using System;
using System.Collections.Generic;
using GoogleMobileAds.Ump.Api;
using LittleBitGames.Ads.Configs;
using UnityEngine;

public class UMPHandler
{
    private readonly UMPSettings _umpSettings;
    private readonly bool _isDebugMode;

    public event Action OnConsent;

    public UMPHandler(UMPSettings umpSettings, bool isDebugMode)
    {
        _umpSettings = umpSettings;
        _isDebugMode = isDebugMode;
    }
    public void Init()
    {
       
        ConsentRequestParameters request = new ConsentRequestParameters
        {
            TagForUnderAgeOfConsent = false,
        };
        
        if (_isDebugMode)
        {
            var idTestDevices = new List<string>();
            idTestDevices.AddRange(_umpSettings.IDTestDevices);
            idTestDevices.AddRange(new List<string>
            {
                "TEST-DEVICE-HASHED-ID",
                "TEST_EMULATOR"
            });
            
            request.ConsentDebugSettings = new ConsentDebugSettings
            {
                DebugGeography = DebugGeography.EEA,
                TestDeviceHashedIds = idTestDevices
            };
        }

        ConsentInformation.Update(request, OnConsentInfoUpdated);
    }

    private void OnConsentInfoUpdated(FormError error)
    {
        if (error != null)
        {
            Debug.LogError(error);
            return;
        }
            
        if (ConsentInformation.IsConsentFormAvailable())
        {
            LoadConsentForm();
        }
     
    }

    private void LoadConsentForm()
    {
        ConsentForm.Load(OnLoadConsentForm);
    }

    private void OnLoadConsentForm(ConsentForm consentForm, FormError error)
    {
        if (error != null)
        {
            Debug.LogError(error);
            return;
        }
        
        if(ConsentInformation.ConsentStatus == ConsentStatus.Required)
        {
            consentForm.Show(OnShowForm);
        }
        else if (ConsentInformation.ConsentStatus == ConsentStatus.Obtained)
        {
            OnConsent?.Invoke();
        }
        // You are now ready to show the form.
    }

    private void OnShowForm(FormError error)
    {
        if (error != null)
        {
            // Handle the error.
            UnityEngine.Debug.LogError(error);
            return;
        }

        // Handle dismissal by reloading form.
        LoadConsentForm();
    }

    public void Reset()
    {
        ConsentInformation.Reset();
    }
    

}
