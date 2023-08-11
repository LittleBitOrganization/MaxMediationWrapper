using System;
using System.Collections.Generic;
using GoogleMobileAds.Ump.Api;
using UnityEngine;

public class UMPHandler
{
    private readonly bool _isDebugMode;

    public event Action OnConsent;

    public UMPHandler(bool isDebugMode)
    {
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
            request.ConsentDebugSettings = new ConsentDebugSettings
            {
                DebugGeography = DebugGeography.EEA,
                TestDeviceHashedIds = new List<string>
                {
                    "TEST-DEVICE-HASHED-ID",
                    "TEST_EMULATOR"
                }
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
