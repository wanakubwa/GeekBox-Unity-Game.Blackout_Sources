using UnityEngine;
using System.Collections;
using GeekBox.Ads;

public class ADManager : ManagerSingletonBase<ADManager>
{
    #region Fields

    [SerializeField]
    private int failsToShowInterstitial = 2;
    [SerializeField]
    private int winsToShowInterstitial = 3;

    #endregion

    #region Propeties

    public int FailsToShowInterstitial { 
        get => failsToShowInterstitial; 
    }
    public int WinsToShowInterstitial {
        get => winsToShowInterstitial;
    }

    // Variables
    private int CurrentFailsCounter { get; set; }
    private int CurrentWinsCounter { get; set; }
    private EasyMobileManager CachedMobileAdsManager { get; set; }
    private bool WasFirstAd { get; set; } = false;

    #endregion

    #region Methods

    protected override void Start()
    {
        base.Start();

        CachedMobileAdsManager = EasyMobileManager.Instance;
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        GameEventsManager.Instance.OnPlayerLooseScenario += OnPlayerLoose;
        GameEventsManager.Instance.OnPlayerWinScenario += OnPlayerWinn;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        if(GameEventsManager.Instance != null)
        {
            GameEventsManager.Instance.OnPlayerLooseScenario -= OnPlayerLoose;
            GameEventsManager.Instance.OnPlayerWinScenario -= OnPlayerWinn;
        }
    }

    private void OnPlayerLoose()
    {
        CurrentFailsCounter++;
        if(WasFirstAd == false || CurrentFailsCounter >= FailsToShowInterstitial)
        {
            CurrentFailsCounter = Constants.DEFAULT_VALUE;
            CachedMobileAdsManager.ShowInterstitialAD(() => { WasFirstAd = true; });
        }
    }

    private void OnPlayerWinn(float ms)
    {
        CurrentWinsCounter++;
        if (WasFirstAd == false || CurrentWinsCounter >= WinsToShowInterstitial)
        {
            CurrentWinsCounter = Constants.DEFAULT_VALUE;
            CachedMobileAdsManager.ShowInterstitialAD(()=> { WasFirstAd = true; });
        }
    }

    #endregion

    #region Enums



    #endregion
}
