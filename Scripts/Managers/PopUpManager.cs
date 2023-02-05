using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using TutorialSystem;
using UnityEngine;

public class PopUpManager : ManagerSingletonBase<PopUpManager>
{
    #region Fields

    [Space, Title("Editor Popups")]
    [SerializeField]
    private GameObject scenarioLoadEditorPopUp;
    [SerializeField]
    private GameObject parentSetupEditorPopUp;
    [SerializeField]
    private GameObject scenarioTimeEditorPopUp;
    [SerializeField]
    private GameObject aiEditorPopUp;

    [Space, Title("Game Popups")]
    [SerializeField]
    private GameObject selectScenarioPopUp;
    [SerializeField]
    private GameObject gameMenuPopUp;
    [SerializeField]
    private GameObject winPopUp;
    [SerializeField]
    private GameObject loosePopUp;
    [SerializeField]
    private GameObject settingsPopUp;
    [SerializeField]
    private GameObject okCancelPopUp;
    [SerializeField]
    private GameObject creditsPopUp;
    [SerializeField]
    private GameObject tutorialPopUp;
    [SerializeField]
    GameObject loadingPopUp;

    #endregion

    #region Propeties

    public event Action<PopUpController> OnPopUpClosed = delegate { };
    public event Action OnOverlapPopUpOpen = delegate { };
    public event Action OnOverlapPopUpClose = delegate { };

    public PopUpController CurrentPopUp {
        get;
        private set;
    }

    public PopUpController OverlapPopUp {
        get;
        private set;
    }

    public List<PopUpController> PopUpsQueue {
        get;
        private set;
    } = new List<PopUpController>();

    public GameObject ScenarioLoadEditorPopUp { get => scenarioLoadEditorPopUp; }
    public GameObject SelectScenarioPopUp { get => selectScenarioPopUp; }
    public GameObject GameMenuPopUp { get => gameMenuPopUp; }
    public GameObject WinPopUp { get => winPopUp; }
    public GameObject LoosePopUp { get => loosePopUp; }
    public GameObject ParentSetupEditorPopUp { get => parentSetupEditorPopUp; }
    public GameObject ScenarioTimeEditorPopUp { get => scenarioTimeEditorPopUp; }
    public GameObject AIEditorPopUp { get => aiEditorPopUp; }
    public GameObject SettingsPopUp { get => settingsPopUp; }
    public GameObject OkCancelPopUp { get => okCancelPopUp; }
    public GameObject CreditsPopUp { get => creditsPopUp; }
    public GameObject TutorialPopUp { get => tutorialPopUp; }
    public GameObject LoadingPopUp { get => loadingPopUp; }

    #endregion

    #region Methods

    public T ShowPopUp<T>(GameObject popUpPrefab, bool isQueued = true) where T : PopUpController
    {
        ClearReferences();

        bool isDisplayed = false;

        if(popUpPrefab == null)
        {
            return null;
        }

        T popUpController = RequestPopUp<T>(popUpPrefab);

        // TODO: Zrobic to normalnie.
        if(popUpController.Prority == PopUpPrority.OVERLAP)
        {
            popUpController.TogglePopUp();
            if(OverlapPopUp != null)
            {
                OverlapPopUp.ClosePopUp();
            }

            OverlapPopUp = popUpController;

            OnOverlapPopUpOpen();
            return popUpController;
        }

        if(CurrentPopUp == null)
        {
            CurrentPopUp = popUpController;
            popUpController.TogglePopUp();

            isDisplayed = true;
        }
        else
        {
            if(HasHigherOrEqualPriority(popUpController) == true)
            {
                SwapCurrentPopUp(popUpController);
                isDisplayed = true;
            }
        }

        if (isQueued == true)
        {
            AddPopUpToQueue(popUpController);
        }

        // Jezeli nie zostal wyswietlony ani nie jest w queue to mozna zniszczyc.
        if (isDisplayed == false && isQueued == false)
        {
            popUpController.ClosePopUp();
            popUpController = null;
        }

        return popUpController;
    }

    private void ClearReferences()
    {
        if(PopUpsQueue.IsNullOrEmpty() == true)
        {
            return;
        }

        for(int i=0; i < PopUpsQueue.Count; i++)
        {
            if (PopUpsQueue[i] == null)
            {
                PopUpsQueue.RemoveAt(i);
            }
        }
    }

    public T RequestPopUp<T>(GameObject popUpPrefab) where T : PopUpController
    {
        T requestedPopUp;

        requestedPopUp = Instantiate(popUpPrefab).GetComponent<T>();
        requestedPopUp.Initialize();
        requestedPopUp.TogglePopUp();

        return requestedPopUp;
    }

    public T RequestShowPopUpForced<T>(GameObject popUpPrefab) where T : PopUpController
    {
        T requestedPopUp = null;

        if(CurrentPopUp != null)
        {
            CurrentPopUp.TogglePopUp();
        }

        requestedPopUp = Instantiate(popUpPrefab).GetComponent<T>();
        requestedPopUp.Initialize();

        CurrentPopUp = requestedPopUp;

        return requestedPopUp;
    }

    public void RequestClosePopUp(PopUpController popUp)
    {
        TryRemovePopUpFromQueue(popUp);

        if (popUp.Prority == PopUpPrority.OVERLAP)
        {
            OverlapPopUp = null;
            OnOverlapPopUpClose();
        }
        else
        {
            if (CurrentPopUp == popUp)
            {
                CurrentPopUp = null;
                CheckQueue();
            }
        }

        OnPopUpClosed(popUp);
    }

    public bool IsPopUpDisplayed<T>() where T : PopUpController
    {
        if (CurrentPopUp is T)
        {
            return true;
        }

        return false;
    }

    public T GetPopUpController<T>() where T : PopUpController
    {
        if(CurrentPopUp is T)
        {
            return (T)CurrentPopUp;
        }

        T queuePopup = TryGetPopUpFromQueue<T>();
        if(queuePopup != null)
        {
            return queuePopup;
        }

        return null;
    }

    public bool IsPopUpInQueue(PopUpController popUpController)
    {
        if(PopUpsQueue == null)
        {
            return false;
        }

        for(int i =0; i< PopUpsQueue.Count; i++)
        {
            if (ReferenceEquals(PopUpsQueue[i], popUpController) == true)
            {
                return true;
            }
        }

        return false;
    }

    public T TryGetPopUpFromQueue<T>() where T : PopUpController
    {
        if(PopUpsQueue != null)
        {
            for(int i =0; i < PopUpsQueue.Count; i++)
            {
                if(PopUpsQueue[i] is T)
                {
                    return (T)PopUpsQueue[i];
                }
            }
        }

        return null;
    }

    #region PopUps

    public LoadingPopUpController ShowLoadingPopUp(int maxValue)
    {
        LoadingPopUpController popUpController = ShowPopUp<LoadingPopUpController>(LoadingPopUp, false);
        if (popUpController != null)
        {
            popUpController.InitializePopUp(maxValue);
        }

        return popUpController;
    }

    public TutorialPopUpController ShowTutorialPopUp(TutorialType tutorialType, Action<TutorialType> onTutorialFinish)
    {
        TutorialPopUpController popUpController = ShowPopUp<TutorialPopUpController>(TutorialPopUp);
        if(popUpController != null)
        {
            popUpController.SetUp(tutorialType, onTutorialFinish);
        }

        return popUpController;
    }

    public CreditsPopUpController ShowCreditsPopUp()
    {
        CreditsPopUpController popUpController = ShowPopUp<CreditsPopUpController>(CreditsPopUp, false);
        return popUpController;
    }

    public InfoOKCancelPopUpController ShowOkCancelPopUp(string bodyInfoKey, Action onAcceptCallback, Action onCancelCallback = null)
    {
        InfoOKCancelPopUpController popUpController = ShowPopUp<InfoOKCancelPopUpController>(OkCancelPopUp, false);
        if(popUpController != null)
        {
            onCancelCallback = onCancelCallback == null ? delegate { } : onCancelCallback;
            popUpController.RefreshPopUp(bodyInfoKey, onAcceptCallback, onCancelCallback);
        }

        return popUpController;
    }

    public LoadScenarioEditorPopUpController ShowScenarioLoadEditorPopUp()
    {
        LoadScenarioEditorPopUpController popUpController = ShowPopUp<LoadScenarioEditorPopUpController>(ScenarioLoadEditorPopUp, false);
        return popUpController;
    }

    public SelectScenarioPopUpController ShowScenarioSelectPopUp()
    {
        SelectScenarioPopUpController popUpController = ShowPopUp<SelectScenarioPopUpController>(SelectScenarioPopUp, false);
        return popUpController;
    }

    public SettingsPopUpController ShowSettingsPopUp()
    {
        SettingsPopUpController popUpController = ShowPopUp<SettingsPopUpController>(SettingsPopUp, false);
        return popUpController;
    }

    public GameMenuPopUpController ShowGameMenuPopUp()
    {
        GameMenuPopUpController popUpController = ShowPopUp<GameMenuPopUpController>(GameMenuPopUp, false);
        return popUpController;
    }

    public WinPopUpController ShowWinPopUp(float lvlTimeMs)
    {
        WinPopUpController popUpController = ShowPopUp<WinPopUpController>(WinPopUp, false);
        if(popUpController != null)
        {
            popUpController.RefreshPopUp(lvlTimeMs);
        }

        return popUpController;
    }

    public LosePopUpController ShowLosePopUp(float lvlTimeMs)
    {
        LosePopUpController popUpController = ShowPopUp<LosePopUpController>(LoosePopUp, false);
        if (popUpController != null)
        {
            popUpController.RefreshPopUp(lvlTimeMs);
        }

        return popUpController;
    }

    public EditorParentSetupController ShowEditorParentSetupPopUp(NodeParent targetParent)
    {
        EditorParentSetupController popUpController = ShowPopUp<EditorParentSetupController>(ParentSetupEditorPopUp, false);
        if (popUpController != null)
        {
            popUpController.RefreshPopUp(targetParent);
        }

        return popUpController;
    }

    public EditorScenarioTimeSetupController ShowEditorScenarioTimeSetupPopUp()
    {
        EditorScenarioTimeSetupController popUpController = ShowPopUp<EditorScenarioTimeSetupController>(ScenarioTimeEditorPopUp, false);
        if (popUpController != null)
        {
            popUpController.RefreshPopUp(ScenarioDataManager.Instance);
        }

        return popUpController;
    }

    public EditorAISetupController ShowEditorAISetupPopUp(AISystem.AIParentSettings settings)
    {
        EditorAISetupController popUpController = ShowPopUp<EditorAISetupController>(AIEditorPopUp, false);
        if (popUpController != null)
        {
            popUpController.RefreshPopUp(settings);
        }

        return popUpController;
    }

    #endregion

    private void AddPopUpToQueue(PopUpController popUpController)
    {
        PopUpsQueue.Add(popUpController);
    }

    private bool HasHigherOrEqualPriority(PopUpController popUpController)
    {
        if (popUpController.Prority >= CurrentPopUp.Prority)
        {
            return true;
        }

        return false;
    }

    private void SwapCurrentPopUp(PopUpController popUpController)
    {
        PopUpController cachedPopUp = CurrentPopUp;

        if (cachedPopUp != null)
        {
            if (IsPopUpInQueue(cachedPopUp) == true)
            {
                cachedPopUp.TogglePopUp();
            }
            else
            {
                cachedPopUp.ClosePopUp();
            }
        }

        CurrentPopUp = popUpController;
        CurrentPopUp.TogglePopUp();
    }

    private void TryRemovePopUpFromQueue(PopUpController popUpController)
    {
        if (PopUpsQueue == null)
        {
            return;
        }

        PopUpsQueue.RemoveAll(x => x == popUpController);
    }

    private PopUpController TryGetHighestProrityPopUpFromQueue()
    {
        PopUpController hiPriority = null;
        if (PopUpsQueue != null && PopUpsQueue.Count > 0)
        {
            hiPriority = PopUpsQueue.First();

            for (int i = 0; i < PopUpsQueue.Count; i++)
            {
                if (PopUpsQueue[i].Prority > hiPriority.Prority)
                {
                    hiPriority = PopUpsQueue[i];
                }
            }
        }

        return hiPriority;
    }

    private void CheckQueue()
    {
        PopUpController nextPopUp = TryGetHighestProrityPopUpFromQueue();
        if(nextPopUp != null)
        {
            nextPopUp.TogglePopUp();
            CurrentPopUp = nextPopUp;
        }
    }

    private bool CheckDoublePopUp<T>() where T : PopUpController
    {
        if(OverlapPopUp is T)
        {
            return true;
        }

        if(CurrentPopUp is T)
        {
            return true;
        }

        for(int i = 0; i < PopUpsQueue.Count; i++)
        {
            if(PopUpsQueue[i] is T)
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    #region Handlers



    #endregion

    public enum PopUpPrority
    {
        MINIMUM = 0,
        NORMAL = 5,
        MEDIUM = 10,
        HIGH = 15,
        VERY_HIGH = 20,

        OVERLAP = -1
    }
}

