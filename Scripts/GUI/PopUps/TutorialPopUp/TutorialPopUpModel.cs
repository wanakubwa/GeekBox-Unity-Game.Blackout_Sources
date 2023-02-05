using System;
using TutorialSystem;

public class TutorialPopUpModel : PopUpModel
{
    #region Fields



    #endregion

    #region Propeties

    public TutorialType CurrentTutorialType {
        get;
        private set;
    }

    private Action<TutorialType> OnTutorialFinishCallback {
        get;
        set;
    } = delegate { };

    #endregion

    #region Methods

    public void SetUp(TutorialType currentType, Action<TutorialType> onTutorialFinish)
    {
        CurrentTutorialType = currentType;
        OnTutorialFinishCallback = onTutorialFinish;
    }

    public TutorialElement[] GetCurrentTutorials()
    {
        //todo;
        return TutorialSettings.Instance.GetTutorialDefinition(CurrentTutorialType)?.TutorialElements;
    }

    public void FinishTotorial()
    {
        OnTutorialFinishCallback(CurrentTutorialType);
    }

    #endregion

    #region Enums



    #endregion
}
