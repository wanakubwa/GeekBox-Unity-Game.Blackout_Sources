using AISystem;
using System;
using UnityEngine;

[Serializable]
public class AIManagerMemento : MementoBase
{
    #region Fields

    [SerializeField]
    private AISettings settingsSave = new AISettings();

    #endregion

    #region Propeties

    public AISettings SettingsSave { 
        get => settingsSave; 
        private set => settingsSave = value; 
    }

    #endregion

    #region Methods

    public override void CreateMemento(IManager sourceManager)
    {
        AIManager manager = sourceManager as AIManager;
        SettingsSave = new AISettings(manager.Settings);
    }

    #endregion

    #region Enums



    #endregion
}
