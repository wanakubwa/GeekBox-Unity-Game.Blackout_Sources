using System;
using UnityEngine;

[Serializable]
public class TimeManagerMemento : MementoBase
{
    #region Fields

    [SerializeField]
    private float currentMsCounterSave = Constants.DEFAULT_VALUE;

    #endregion

    #region Propeties

    public float CurrentMsCounterSave { 
        get => currentMsCounterSave; 
        private set => currentMsCounterSave = value; 
    }

    #endregion

    #region Methods

    public override void CreateMemento(IManager sourceManager)
    {
        TimeManager manager = sourceManager as TimeManager;
        CurrentMsCounterSave = manager.CurrentMilisecondsCounter;
    }

    #endregion

    #region Enums



    #endregion
}
