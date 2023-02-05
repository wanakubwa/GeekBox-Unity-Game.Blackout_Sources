using PlayerData;
using UnityEngine;

public class PlayerManager : SingletonSaveableManager<PlayerManager, PlayerManagerMemento>
{
    #region Fields

    [SerializeField]
    private PlayerWallet wallet = new PlayerWallet();
    [SerializeField]
    private ParentValues playerParentValues = new ParentValues();

    #endregion

    #region Propeties

    public PlayerWallet Wallet { 
        get => wallet; 
        private set => wallet = value; 
    }

    public ParentValues PlayerParentValues { 
        get => playerParentValues; 
        private set => playerParentValues = value; 
    }

    #endregion

    #region Methods

    public override void LoadManager(PlayerManagerMemento memento)
    {
        Wallet = memento.WalletSave;
        PlayerParentValues = memento.PlayerParentValuesSave;

        // HOTFIX - 05.11
        if(Wallet.FinishedLvls == null)
        {
            Wallet.FinishedLvls = new System.Collections.Generic.HashSet<PlayerLvlInfo>();
        }

        if(Wallet.UnlockedModes == null)
        {
            wallet.UnlockedModes = new System.Collections.Generic.List<NodeModeType>();
        }
    }

    public override void ResetGameData()
    {
        base.ResetGameData();

        Wallet = new PlayerWallet();
        PlayerParentValues = new ParentValues();
    }

    #endregion

    #region Enums



    #endregion
}
