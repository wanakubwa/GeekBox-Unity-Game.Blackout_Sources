using AISystem;
using System;
using UnityEngine;

[Serializable]
public class TutorialManagerMemento : MementoBase
{
    #region Fields

    [SerializeField]
    private bool isTechnologyTreeTutorialFinishedSave = false;
    [SerializeField]
    private bool isGameplayIntroTutorialFinishedSave = false;

    #endregion

    #region Propeties

    public bool IsTechnologyTreeTutorialFinishedSave { 
        get => isTechnologyTreeTutorialFinishedSave; 
        private set => isTechnologyTreeTutorialFinishedSave = value; 
    }
    public bool IsGameplayIntroTutorialFinishedSave {
        get => isGameplayIntroTutorialFinishedSave;
        private set => isGameplayIntroTutorialFinishedSave = value;
    }

    #endregion

    #region Methods

    public override void CreateMemento(IManager sourceManager)
    {
        TutorialManager manager = sourceManager as TutorialManager;
        IsTechnologyTreeTutorialFinishedSave = manager.IsTechnologyTreeTutorialFinished;
        IsGameplayIntroTutorialFinishedSave = manager.IsGameplayIntroTutorialFinished;
    }

    #endregion

    #region Enums



    #endregion
}
