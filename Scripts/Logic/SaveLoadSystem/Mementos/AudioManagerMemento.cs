using System;
using UnityEngine;

[Serializable]
public class AudioManagerMemento : MementoBase
{
    #region Fields

    [SerializeField]
    private bool isAudioMuted;

    #endregion

    #region Propeties

    public bool IsAudioMutedSave { 
        get => isAudioMuted; 
        private set => isAudioMuted = value; 
    }

    #endregion

    #region Methods

    public override void CreateMemento(IManager sourceManager)
    {
        AudioManager manager = sourceManager as AudioManager;
        IsAudioMutedSave = manager.IsAudioMute;
    }

    #endregion

    #region Enums



    #endregion
}
