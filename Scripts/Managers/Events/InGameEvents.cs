using UnityEngine;
using System.Collections;
using System;

public class InGameEvents : ManagerSingletonBase<InGameEvents>
{
    #region Fields



    #endregion

    #region Propeties

    public event Action<bool> OnTimePaused = delegate { };

    /// <summary>
    /// int - id rodzica trigerujacego.
    /// </summary>
    public event Action<int> OnKamikazeEffectUsed = delegate { };

    #endregion

    #region Methods

    public void NotifyPauseTime(bool isPaused)
    {
        OnTimePaused(isPaused);
    }

    public void NotifyKamikazeEffectUse(int senderID)
    {
        OnKamikazeEffectUsed(senderID);
    }

    #endregion

    #region Enums



    #endregion
}
