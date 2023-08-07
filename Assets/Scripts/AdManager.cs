using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdManager : MonoBehaviour
{
    public static AdManager instance;
    [SerializeField] DestinationGate destinationGate;
    
    [SerializeField] string androidUnitID = "Rewarded_Android";
    [SerializeField] string gameID = "4958310";
    [SerializeField] bool testMode = true;
    [SerializeField] string androidUnitInterstitial = "MainMenuLevel";            
    private void Awake() {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //Advertisement.AddListener(this);
            Advertisement.Initialize(gameID, testMode);
        }   

    }
    public void OnUnityAdsDidError(string message)
    {
        Debug.LogWarning($"Error occured: {message}");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch(showResult)
        {
            case ShowResult.Skipped: 
                Debug.Log("skipped");
                break;
            case ShowResult.Failed:
                Debug.Log("Failed");
                break;
            case ShowResult.Finished:
                if(destinationGate != null)
                {
                    destinationGate.DoublePoints();
                }
                
                break;    
            default: 
                Debug.Log("default");
                break;
        }
    }
    public void ShowAd(DestinationGate gate)
    {
        this.destinationGate = gate;
        Advertisement.Show(androidUnitID);
    }
    public void ShowInterstitial()
    {
        Advertisement.Show(androidUnitInterstitial);
    }
    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Ad started");
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Ad is ready!");
    }
}
