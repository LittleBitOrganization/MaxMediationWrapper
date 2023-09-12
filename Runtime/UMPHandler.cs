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

    private void Log(string value)
    {
        Debug.Log(value);
    }

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
            
            Log("UMP inited debug mode");
        }

        
        Log("Update");
        ConsentInformation.Update(request, OnConsentInfoUpdated);
    }

    private void OnConsentInfoUpdated(FormError error)
    {
        if (error != null)
        {
            Log("OnConsentInfoUpdated Error" + error.ErrorCode + "  " + error.Message);
            Debug.LogError(error);
            return;
        }
            
     
        if (ConsentInformation.IsConsentFormAvailable())
        {
            Log("Consent Information is available");
            LoadConsentForm();
        }
        else
        {
            Log("Consent Information isn't available");
        }
     
    }

    private void LoadConsentForm()
    {
        Log("Load Consent Form");
        ConsentForm.Load(OnLoadConsentForm);
    }

    private void OnLoadConsentForm(ConsentForm consentForm, FormError error)
    {
      
        if (error != null)
        {
            Log("OnLoadConsentForm Error" + error.ErrorCode + "  " + error.Message);
            Debug.LogError(error);
            return;
        }
        Debug.Log("Consent form success loaded with status: " + ConsentInformation.ConsentStatus);
        
        if(ConsentInformation.ConsentStatus == ConsentStatus.Required)
        {
            consentForm.Show(OnShowForm);
        }
        else if (ConsentInformation.ConsentStatus == ConsentStatus.Obtained)
        {
            Log("User has given consent!");
            OnConsent?.Invoke();
        }
        // You are now ready to show the form.
    }

    private void OnShowForm(FormError error)
    {
        if (error != null)
        {
            Log("OnShowForm Error" + error.ErrorCode + "  " + error.Message);
            UnityEngine.Debug.LogError(error);
            return;
        }
        
        Log("Consent form success shown");

        // Handle dismissal by reloading form.
        LoadConsentForm();
    }

    public void Reset()
    {
        ConsentInformation.Reset();
        Log("Consent was withdrawn!");
    }
    

}
