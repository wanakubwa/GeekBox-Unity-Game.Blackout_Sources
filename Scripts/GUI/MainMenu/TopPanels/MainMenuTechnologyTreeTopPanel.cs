using UnityEngine;
using TMPro;

public class MainMenuTechnologyTreeTopPanel : MainMenuTopPanelBase
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI currentKPointsText;

    #endregion

    #region Propeties

    public TextMeshProUGUI CurrentKPointsText {
        get => currentKPointsText;
    }

    #endregion

    #region Methods

    protected override void AttachEvents()
    {
        base.AttachEvents();
        if(PlayerManager.Instance != null)
        {
            PlayerManager.Instance.Wallet.OnKPointsChanged += RefreshCurrentKPointsText;
        }
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.Wallet.OnKPointsChanged -= RefreshCurrentKPointsText;
        }
    }

    protected override void RefreshPanel()
    {
        base.RefreshPanel();

        RefreshCurrentKPointsText(GetPlayerKPointsAmmount());
    }

    private void RefreshCurrentKPointsText(int ammount)
    {
        CurrentKPointsText.SetText(ammount.ToString());
    }

    private int GetPlayerKPointsAmmount()
    {
        return PlayerManager.Instance != null ? PlayerManager.Instance.Wallet.KPointsAmmount : Constants.DEFAULT_VALUE;
    }

    #endregion

    #region Enums



    #endregion
}
