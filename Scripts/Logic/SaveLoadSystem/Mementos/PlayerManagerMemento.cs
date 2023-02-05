using PlayerData;
using System;
using UnityEngine;

[Serializable]
public class PlayerManagerMemento : MementoBase
{
    #region Fields

    [SerializeField]
    private PlayerWallet walletSave;
    [SerializeField]
    private ParentValues playerParentValuesSave;

    #endregion

    #region Propeties

    public PlayerWallet WalletSave { 
        get => walletSave; 
        private set => walletSave = value; 
    }

    public ParentValues PlayerParentValuesSave { 
        get => playerParentValuesSave; 
        private set => playerParentValuesSave = value; 
    }

    #endregion

    #region Methods

    public override void CreateMemento(IManager sourceManager)
    {
        PlayerManager manager = sourceManager as PlayerManager;
        WalletSave = manager.Wallet;
        PlayerParentValuesSave = manager.PlayerParentValues;
    }

    #endregion

    #region Enums



    #endregion
}
