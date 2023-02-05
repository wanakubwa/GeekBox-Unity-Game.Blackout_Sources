using TutorialSystem;

public class TutorialManager : SingletonSaveableManager<TutorialManager, TutorialManagerMemento>
{
    #region Fields



    #endregion

    #region Propeties

    public bool IsTechnologyTreeTutorialFinished {
        get;
        set;
    } = false;

    public bool IsGameplayIntroTutorialFinished
    {
        get;
        set;
    } = false;

    #endregion

    #region Methods

    public override void LoadManager(TutorialManagerMemento memento)
    {
        IsTechnologyTreeTutorialFinished = memento.IsTechnologyTreeTutorialFinishedSave;
        IsGameplayIntroTutorialFinished = memento.IsGameplayIntroTutorialFinishedSave;
    }

    public override void ResetGameData()
    {
        base.ResetGameData();

        IsTechnologyTreeTutorialFinished = false;
        IsGameplayIntroTutorialFinished = false;
    }

    public void TryShowTutorial(TutorialType tutorialType)
    {
        // Okropne, ale proste. :(
        switch (tutorialType)
        {
            case TutorialType.MENU_TECHNOLOGY_TREE:

                if(IsTechnologyTreeTutorialFinished == false)
                {
                    ShowTutorial(tutorialType);
                }

                break;
            case TutorialType.GAMEPLAY_INTRO:

                if (IsGameplayIntroTutorialFinished == false)
                {
                    ShowTutorial(tutorialType);
                }

                break;
            default:
                break;
        }
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        ScenariosManager.Instance.OnGameScenarioLoaded += OnScenarioLoadedHandler;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        if (ScenariosManager.Instance != null)
        {
            ScenariosManager.Instance.OnGameScenarioLoaded -= OnScenarioLoadedHandler;
        }
    }

    private void ShowTutorial(TutorialType tutorialType)
    {
        PopUpManager.Instance.ShowTutorialPopUp(tutorialType, HandleTutorialFinished);
    }

    private void HandleTutorialFinished(TutorialType tutorialType)
    {
        switch (tutorialType)
        {
            case TutorialType.MENU_TECHNOLOGY_TREE:

                IsTechnologyTreeTutorialFinished = true;
                break;

            case TutorialType.GAMEPLAY_INTRO:
                IsGameplayIntroTutorialFinished = true;
                break;

            default:
                break;
        }
    }

    // Handlers.
    private void OnScenarioLoadedHandler()
    {
        TryShowTutorial(TutorialType.GAMEPLAY_INTRO);
    }

    #endregion

    #region Enums



    #endregion
}
